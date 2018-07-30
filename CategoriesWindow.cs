//
//  PictureViewer2.CategoriesWindow
//
//      Author: Jan-Joost van Zon
//      Date: 31-10-2010 - 06-11-2010
//
//  -----

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace PictureViewer2
{

    public partial class CategoriesWindow : Form
    {

        // Initialization

        public CategoriesWindow()
        {
            InitializeComponent();
            GetCurrentOrDefaultCategoryList();
            BindWithCheck();
            Draw();
        }

        // Data

        private CategoryList CategoryList;
        private CategoryListRepository CategoryListRepository = new CategoryListRepository();

        // Events

        private void categoryListsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCategoryList();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            New();
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            BeginAdd();
        }

        private void categoriesGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            EndAdd(e.Row.Index - 1); // e.Row.Index will return the wrong number. That's what the -1 is for.
        }

        private void categoriesGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            EndEdit(e.RowIndex, e.ColumnIndex);
        }

        private void categoriesGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            RemoveCategory();
        }

        private void categoriesGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            AfterRemoveCategory();
        }

        private void removeCategoryButton_Click(object sender, EventArgs e)
        {
            RemoveCategory();
            AfterRemoveCategory();
        }

        private void clearCategoriesButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void saveCategoryListButton_Click(object sender, EventArgs e)
        {
            Save();
        }
        
        private void renameButton_Click(object sender, EventArgs e)
        {
            Rename();
        }

        private void removeCategoryListButton_Click(object sender, EventArgs e)
        {
            RemoveCategoryList();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CategoriesWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = CheckedSaveChanges() == DialogResult.Cancel;
        }

        // Methods

        private bool Dirty;

        private bool IgnoreCategoryListsComboBoxSelectedIndexChanged;

        private void GetCurrentOrDefaultCategoryList()
        {
            CategoryList = CategoryListRepository.GetCurrentOrDefaultCategoryList();
            Dirty = false;
        }

        private void BindWithCheck()
        {
            if (CategoryList == null)
            {
                MessageBox.Show(
                    String.Format("Error loading category list '{0}'.", CategoryListRepository.CurrentCategoryListName),
                    Program.ApplicationName);
            }
            else
            {
                Bind();
            }
        }

        private void Bind()
        {
            BindCategoryListsComboBox();
            BindCategoriesGrid();
        }

        private void BindCategoryListsComboBox()
        {
            // Prevent SelectedIndexChanged from firing
            IgnoreCategoryListsComboBoxSelectedIndexChanged = true;
            // Call repository
            string[] names = CategoryListRepository.GetAllCategoryListNames();
            // Fill combo list
            categoryListsComboBox.Items.Clear();
            foreach (string name in names)
            {
                categoryListsComboBox.Items.Add(name);
            }
            // Select current category list
            categoryListsComboBox.Text = CategoryList.Name;
            // End prevent SelectedIndexChanged from firing
            IgnoreCategoryListsComboBoxSelectedIndexChanged = false;
        }

        private void BindCategoriesGrid()
        {
            // Remember selected cell
            DataGridViewRow row = GetSelectedRow();
            int rememberedRowIndex = (row != null) ? row.Index : -1;
            // Fill grid with categories
            categoriesGrid.Rows.Clear();
            if (CategoryList != null)
            {
                int i = 0;
                foreach (Category category in CategoryList)
                {
                    categoriesGrid.Rows.Add(
                        i++, 
                        category.SubFolderName, 
                        category.KeyboardKey);
                }
            }
            // Correct remembered row index
            if (rememberedRowIndex > categoriesGrid.Rows.Count - 1) { 
                rememberedRowIndex = categoriesGrid.Rows.Count - 1;
            };
            // Restore selected cell, if it is within rangethere are any rows at all, and any was row selected at all.
            if (rememberedRowIndex != -1) {
                categoriesGrid.CurrentCell = categoriesGrid.Rows[rememberedRowIndex].Cells[1];
            };
        }

        private void SelectCategoryList()
        {
            // Get out of here, if ignore flag on (meaning the selection change is not initiated by user, but caused by data binding)
            if (IgnoreCategoryListsComboBoxSelectedIndexChanged) return;
            // Checked Save changes
            if (CheckedSaveChanges() == DialogResult.Cancel)
            {
                BindCategoryListsComboBox();
                return;
            }
            // Get Category List
            CategoryList = CategoryListRepository.GetCategoryListByName(categoryListsComboBox.SelectedItem.ToString());
            // Assign current category list name
            CategoryListRepository.CurrentCategoryListName = CategoryList.Name;
            // Set not dirty
            Dirty = false;
            // Rebind grid
            BindCategoriesGrid();
        }

        private void New()
        {
            // Checked Save changes
            if (CheckedSaveChanges() == DialogResult.Cancel) return;
            // Enter new name
            string newName = InputBox.Show("Please specify a name.", "", Program.ApplicationName);
            // Get out of here if canceled
            if (newName == "") return;
            // Create new category list
            CategoryList = new CategoryList();
            CategoryList.Name = newName;
            // Assign current category list name
            CategoryListRepository.CurrentCategoryListName = newName;
            // Set not dirty
            Dirty = false;
            // Rebind
            Bind();
        }

        private void BeginAdd()
        {
            categoriesGrid.ClearSelection();
            DataGridViewRow row = categoriesGrid.Rows[categoriesGrid.NewRowIndex];
            categoriesGrid.CurrentCell = row.Cells[1];
            categoriesGrid.Focus();
        }

        private void EndAdd(int rowIndex)
        {
            CategoryList.Add(new Category());
            categoriesGrid.Rows[rowIndex].Cells["Index"].Value = CategoryList.Count - 1;
            Dirty = true;
        }

        private void EndEdit(int rowIndex, int columnIndex)
        {
            string columnName = categoriesGrid.Columns[columnIndex].Name;
            DataGridViewRow row = categoriesGrid.Rows[rowIndex];
            int index = Convert.ToInt32(row.Cells["Index"].Value);
            switch (columnName)
            {
                case "SubFolderName":
                    if (row.Cells[columnName].Value != null)
                    {
                        CategoryList[index].SubFolderName = Convert.ToString(row.Cells[columnName].Value);
                    }
                    break;
                case "KeyboardKey":
                    if (row.Cells[columnName].Value != null)
                    {
                        CategoryList[index].KeyboardKey = Convert.ToChar(row.Cells[columnName].Value);
                    }
                    break;
            }
            Dirty = true;
        }

        private void RemoveCategoryList()
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
                    Bind();
                }
            }
            else
            {
                MessageBox.Show(String.Format("You can not delete category list '{0}' because it does not exists.", CategoryList.Name));
            }
        }

        private void RemoveCategory()
        {
            // Get row of selected cell
            DataGridViewRow row = GetSelectedRow();
            // If selected row found
            if (row != null)
            {
                if (!row.IsNewRow)
                {
                    // Get index property
                    int index = Convert.ToInt32(row.Cells["Index"].Value);
                    // Remove Category
                    CategoryList.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// This is a separate method, because the removal must be done
        /// before the row in the grid is gone, and rebinding must
        /// be done after the row in the grid is automatically deleted
        /// by the grid control.
        /// </summary>
        private void AfterRemoveCategory()
        {
            // Rebind grid
            BindCategoriesGrid();
            categoriesGrid.Focus();
            // Set Dirty
            Dirty = true;
        }

        private void Clear()
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
                BindCategoriesGrid();
                // Set Dirty
                Dirty = true;
            }
        }

        private DialogResult CheckedSaveChanges()
        {
            if (Dirty)
            {
                switch (MessageBox.Show("Save changes first?", Program.ApplicationName, MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        switch (Save())
                        {
                            case true: 
                                return DialogResult.Yes;
                            case false: 
                                return DialogResult.Cancel;
                        }
                        break;
                    case DialogResult.Cancel:
                        return DialogResult.Cancel;
                    case DialogResult.No:
                        return DialogResult.No;
                }
            }
            return DialogResult.Yes;
        }

        /// <summary>
        /// Reused under save button and form closing.
        /// </summary>
        private bool Save()
        {
            // Store current or typed in category list name
            string name = CategoryList.Name;
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
            BindCategoryListsComboBox();
            // Return success
            return true;
        }
        
        private void Rename()
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
                BindCategoryListsComboBox();
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
