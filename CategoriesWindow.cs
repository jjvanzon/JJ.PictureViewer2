//
//  PictureViewer2.CategoriesWindow
//
//      Author: Jan-Joost van Zon
//      Date: 31-10-2010 - 31-10-2010
//
//  -----

using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PictureViewer2
{

    public partial class CategoriesWindow : Form
    {

        // Initialization & Finalization

        public CategoriesWindow()
        {
            InitializeComponent();
            GetCurrentOrDefaultCategoryList();
            if (CategoryList == null)
            {
                MessageBox.Show(
                    String.Format("Error loading category list '{0}'.", CategoryListRepository.CurrentCategoryListName),
                    Program.ApplicationName);
            }
            else
            {
                FillCategoryListsComboBox();
                FillCategoriesGrid();
            }
            Draw();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CategoriesWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Dirty)
            {
                switch (MessageBox.Show("Save changes?", Program.ApplicationName, MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        e.Cancel = !Save();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.No:
                        return;
                }
            }
        }

        // Data Objects

        private CategoryList CategoryList;
        private CategoryListRepository CategoryListRepository = new CategoryListRepository();

        // Processing Steps

        private bool Dirty;
        public bool Dirty
        {
            get
            {
                return Dirty;
            }
            set
            {
                // Changed?
                if (Dirty != value)
                {
                    // Store
                    Dirty = value;
                }
            }
        }

        private string CategoryListNameToDisplay
        {
            get
            {
                return
                    (CategoryList.IsDefault ? "(Default)" : CategoryList.Name) +
                    (Dirty ? " (modified)" : "");
            }
        }

        private void GetCurrentOrDefaultCategoryList()
        {
            CategoryList = CategoryListRepository.GetCurrentOrDefaultCategoryList();
            Dirty = false;
        }

        private void FillCategoryListsComboBox()
        {
            // Call repository
            string[] names = CategoryListRepository.GetAllCategoryListNames();
            // Fill combo list
            categoryListsComboBox.Items.Clear();
            foreach (string name in names)
            {
                categoryListsComboBox.Items.Add(name);
            }
            // Set combo text
            categoryListsComboBox.Text = CategoryListNameToDisplay;
        }

        private void categoryListsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get Category List
            CategoryList = CategoryListRepository.GetCategoryListByName(categoryListsComboBox.SelectedItem.ToString());
            // Assign current category list name
            CategoryListRepository.CurrentCategoryListName = categoryListsComboBox.SelectedItem.ToString();
            // Set not dirty
            Dirty = false;
            // Rebind grid
            FillCategoriesGrid();
        }

        private void FillCategoriesGrid()
        {
            // Remember selected cell
            DataGridViewRow row = GetSelectedRow();
            int currentRowIndex = (row != null) ? row.Index : -1;
            // Fill grid with categories
            categoriesGrid.Rows.Clear();
            if (CategoryList != null)
            {
                int i = 1;
                foreach (Category category in CategoryList)
                {
                    categoriesGrid.Rows.Add(
                        i++, 
                        category.SubFolderName, 
                        category.KeyboardKey);
                }
            }
            // Correct current row index
            if (currentRowIndex > categoriesGrid.Rows.Count - 1) { 
                currentRowIndex = categoriesGrid.Rows.Count - 1;
            };
            // Restore selected cell, if any.
            if (currentRowIndex != -1) {
                categoriesGrid.CurrentCell = categoriesGrid.Rows[currentRowIndex].Cells[1];
            };
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            if (Dirty)
            {
                switch (MessageBox.Show("Save changes first?", Program.ApplicationName, MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        // Save first
                        Save();
                        break;
                    case DialogResult.Cancel:
                        // Stop here
                        return;
                    case DialogResult.No:
                        // Keep going
                        break;
                }
            }
            // New
            string name = InputBox.Show("Please specify a name.", "", Program.ApplicationName);
            // Get Category List
            CategoryList = new CategoryList();
            CategoryList.Name = name;
            // Assign current category list name
            CategoryListRepository.CurrentCategoryListName = name;
            // Set not dirty
            Dirty = false;
            // Rebind
            FillCategoryListsComboBox();
            FillCategoriesGrid();
            return;
        }

        private void categoriesGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            CategoryList.Add(new Category());
            categoriesGrid.Rows[e.Row.Index - 1].Cells["Index"].Value = CategoryList.Count;
            Dirty = true;
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            categoriesGrid.ClearSelection();
            DataGridViewRow row = categoriesGrid.Rows[categoriesGrid.NewRowIndex];
            categoriesGrid.CurrentCell = row.Cells[1];
            categoriesGrid.Focus();
        }

        private void categoriesGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = categoriesGrid.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = categoriesGrid.Rows[e.RowIndex];
            int index = Convert.ToInt32(row.Cells["Index"].Value) - 1;
            switch (columnName)
            {
                case "SubFolderName":
                    CategoryList[index].SubFolderName = Convert.ToString(row.Cells[columnName].Value);
                    break;
                case "KeyboardKey":
                    CategoryList[index].KeyboardKey = Convert.ToChar(row.Cells[columnName].Value);
                    break;
            }
            Dirty = true;
        }

        private void categoriesGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!e.Row.IsNewRow)
            {
                int index = Convert.ToInt32(e.Row.Cells["Index"].Value) - 1;
                CategoryList.RemoveAt(index);
            }
        }

        private void categoriesGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            FillCategoriesGrid(); // After deleting refill the whole grid with categories
            Dirty = true;
        }

        private void removeCategoryButton_Click(object sender, EventArgs e)
        {
            // Get row of selected cell
            DataGridViewRow row = GetSelectedRow();
            // If selected row found
            if (row != null)
            {
                if (!row.IsNewRow)
                {
                    // Get index property
                    int index = Convert.ToInt32(row.Cells["Index"].Value) - 1;
                    // Remove Category
                    CategoryList.RemoveAt(index);
                    // Rebind grid
                    FillCategoriesGrid();
                    categoriesGrid.Focus();
                    // Set Dirty
                    Dirty = true;
                }
            }
        }

        private void clearCategoriesButton_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(
                    "Are you sure you wish to clear the category list?",
                    Program.ApplicationName,
                    MessageBoxButtons.YesNo
                )
                == DialogResult.Yes
            )
            {
                // Clear list in data
                CategoryList.Clear();
                // Rebind grid
                FillCategoriesGrid();
                // Set Dirty
                Dirty = true;
            }
        }

        private void saveCategoryListButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Reused under save button and form closing.
        /// </summary>
        private bool Save()
        {
            // Store current or typed in category list name
            string name = categoryListsComboBox.Text;
            // Prompt user for name
            name = InputBox.Show(
                "Please specify a name.",
                name,
                Program.ApplicationName);
            // If canceled, get out of here.
            if (String.IsNullOrWhiteSpace(name)) { return false; }
            // Check file already exists
            if (CategoryListRepository.CategoryListExists(name))
            {
                // Prompt user for overwrite
                if (
                    MessageBox.Show(
                        "Overwrite category list '" + name + "'?",
                        Program.ApplicationName,
                        MessageBoxButtons.YesNo
                    )
                    == DialogResult.No
                )
                {
                    // Get out of here if user cancelled
                    return false;
                }
            }
            // Set Category List Name
            CategoryList.Name = name;
            // Save Category List
            CategoryListRepository.SaveCategoryList(CategoryList);
            // Set current category list name
            CategoryListRepository.CurrentCategoryListName = name;
            // Set not dirty
            Dirty = false;
            // Set not default
            CategoryList.IsDefault = false;
            // Rebind ComboBox
            FillCategoryListsComboBox();
            // Return success
            return true;
        }
        
        private void renameButton_Click(object sender, EventArgs e)
        {
            if (Dirty)
            {
                MessageBox.Show("Please save before renaming the category list.", Program.ApplicationName);
                return;
            }
            string oldName = CategoryList.Name;
            string newName = InputBox.Show("Please specify a new name.", oldName, Program.ApplicationName);
            // Process only if not canceled
            if (!String.IsNullOrEmpty(newName))
            {
                // Rename
                CategoryListRepository.Rename(CategoryList, newName);
                // Set Current Category List Name Setting
                CategoryListRepository.CurrentCategoryListName = newName;
                // Rebind Combo
                FillCategoryListsComboBox();
            }
        }

        private void removeCategoryListButton_Click(object sender, EventArgs e)
        {
            // Condition: name specified
            if (String.IsNullOrWhiteSpace(CategoryList.Name))
            {
                MessageBox.Show("Please pick a category list to remove.");
                return;
            }
            // If exists
            if (CategoryListRepository.CategoryListExists(CategoryList.Name))
            {
                // Ask if sure
                if (MessageBox.Show(
                        String.Format("Are you sure you want to remove category list '{0}'?", CategoryList.Name),
                        Program.ApplicationName,
                        MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                {
                    // Delete
                    CategoryListRepository.DeleteCategoryList(CategoryList.Name);
                    // Move to first or default category list
                    CategoryListRepository.CurrentCategoryListName = CategoryListRepository.GetFirstCategoryListName();
                    GetCurrentOrDefaultCategoryList();
                    // Rebind
                    FillCategoryListsComboBox();
                    FillCategoriesGrid();

                }
            }
            else
            {
                MessageBox.Show(String.Format("You can not delete category list '{0}' because it does not exists.", CategoryList.Name));
            }
        }

        // Resize

        private const int Spacing = 5;
        private const int ButtonWidth = 55;
        private const int ButtonHeight = 21;

        private void CategoriesWindow_Resize(object sender, EventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            SuspendLayout();
            int previousLeft;
            List<Button> topButtons = new List<Button> {
                newButton, 
                removeCategoryListButton, 
                renameButton, 
                saveCategoryListButton };
            previousLeft = this.ClientRectangle.Right;
            foreach (Button topButton in topButtons)
            {
                topButton.Left = previousLeft - Spacing - ButtonWidth;
                topButton.Top = Spacing;
                topButton.Width = ButtonWidth;
                topButton.Height = ButtonHeight;
                previousLeft = topButton.Left;
            }
            categoryListsComboBox.Left = Spacing;
            categoryListsComboBox.Top = Spacing;
            categoryListsComboBox.Width = saveCategoryListButton.Left - Spacing * 2;
            categoryListsComboBox.Height = ButtonHeight;
            okButton.Left = Spacing;
            okButton.Top = this.ClientRectangle.Bottom - Spacing - ButtonHeight;
            okButton.Width = ButtonWidth;
            okButton.Height = ButtonHeight;
            List<Button> bottomButtons = new List<Button> {
                clearCategoriesButton,
                removeCategoryButton,
                addCategoryButton
            };
            previousLeft = this.ClientRectangle.Right;
            foreach (Button bottomButton in bottomButtons)
            {
                bottomButton.Left = previousLeft - Spacing - ButtonWidth;
                bottomButton.Top = this.ClientRectangle.Bottom - Spacing - ButtonHeight;
                bottomButton.Width = ButtonWidth;
                bottomButton.Height = ButtonHeight;
                previousLeft = bottomButton.Left;
            }
            categoriesGrid.Left = Spacing;
            categoriesGrid.Top = categoryListsComboBox.Top + categoryListsComboBox.Height + Spacing;
            categoriesGrid.Width = this.ClientSize.Width - Spacing * 2;
            categoriesGrid.Height = okButton.Top - Spacing - categoryListsComboBox.Bottom - Spacing;
            ResumeLayout();
        }

        // Helpers

        private DataGridViewRow GetSelectedRow()
        {
            DataGridViewRow row = null;
            // Get row of selected cell
            if (categoriesGrid.SelectedCells.Count == 1)
            {
                DataGridViewCell cell = categoriesGrid.SelectedCells[0];
                row = categoriesGrid.Rows[cell.RowIndex];
            }
            // Else get selected row
            else if (categoriesGrid.SelectedRows.Count == 1)
            {
                row = categoriesGrid.SelectedRows[0];
            }
            // Return
            return row;
        }

    }

}
