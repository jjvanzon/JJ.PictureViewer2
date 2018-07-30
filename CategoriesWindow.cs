//
//  PictureViewer2.CategoriesWindow
//
//      Author: Jan-Joost van Zon
//      Date: 31-10-2010 - 31-10-2010
//
//  -----

using System;
using System.Windows.Forms;

namespace PictureViewer2
{
    public partial class CategoriesWindow : Form
    {

        // Initialization

        public CategoriesWindow()
        {
            InitializeComponent();
            FillCategoryListsComboBox();
            GetCurrentOrDefaultCategoryList();
            FillCategoriesGrid();
        }

        // Entity Object

        private CategoryList CategoryList;

        // Steps

        private void GetCurrentOrDefaultCategoryList()
        {
            var repository = new CategoryListRepository();
            CategoryList = repository.GetCurrentOrDefaultCategoryList();
        }

        private void FillCategoryListsComboBox()
        {
            // Call repository
            var repository = new CategoryListRepository();
            string[] names = repository.GetAllCategoryListNames();
            // Fill combo list
            categoryListsComboBox.Items.Clear();
            foreach (string name in names)
            {
                categoryListsComboBox.Items.Add(name);
            }
            // Set combo text
            categoryListsComboBox.Text = repository.CurrentCategoryListName;
        }

        private void categoryListsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get repository
            var repository = new CategoryListRepository();
            // Get Category List
            CategoryList = repository.GetCategoryListByName(categoryListsComboBox.SelectedItem.ToString());
            // Assign current category list name
            repository.CurrentCategoryListName = categoryListsComboBox.SelectedItem.ToString();
            // Rebind grid
            FillCategoriesGrid();
        }

        private void FillCategoriesGrid()
        {
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
        }

        private void categoriesGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            CategoryList.Add(new Category());
            categoriesGrid.Rows[e.Row.Index - 1].Cells["Index"].Value = CategoryList.Count;
        }

        private void categoriesGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int index = Convert.ToInt32(e.Row.Cells["Index"].Value) - 1;
            CategoryList.RemoveAt(index);
        }

        private void categoriesGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            FillCategoriesGrid(); // After deleting refill the whole grid with categories
        }

        private void saveCategoryListButton_Click(object sender, EventArgs e)
        {
            // Get repository
            var repository = new CategoryListRepository();
            // Store current category list name
            string name = categoryListsComboBox.Text;
            // Prompt user for name
            name = InputBox.Show(
                "Please specify a name.", 
                name, 
                Program.ApplicationName);
            // If canceled, get out of here.
            if (String.IsNullOrWhiteSpace(name)) { return; }
            // Check file already exists
            if (repository.CategoryListExists(name))
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
                    return;
                }
            }
            // Set Category List Name
            CategoryList.Name = name;
            // Save Category List
            repository.SaveCategoryList(CategoryList);
            // Set current category list name
            repository.CurrentCategoryListName = name;
            // Rebind ComboBox
            FillCategoryListsComboBox();
        }

    }
}
