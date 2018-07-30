//
//  PictureViewer2.CategoriesWindow
//
//      Author: Jan-Joost van Zon
//      Date: 31-10-2010 - 31-10-2020
//
//  -----

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using PictureViewer2.Properties;

namespace PictureViewer2
{
    public partial class CategoriesWindow : Form
    {

        // Initialization

        public CategoriesWindow()
        {
            InitializeComponent();
            FillPresetComboBox();
        }

        // Data Object

        private List<Category> _categorylist;
        public List<Category> CategoryList
        {
            get
            {
                return _categorylist;
            }
            set
            {
                if (_categorylist != value)
                {
                    _categorylist = value;
                    FillCategoryGrid();
                }
            }
        }

        // Data Binding

        private void FillCategoryGrid()
        {
            categoryGrid.Rows.Clear();
            if (CategoryList != null)
            {
                int i = 1;
                foreach (Category category in CategoryList)
                {
                    categoryGrid.Rows.Add(i++, category.SubFolderName, category.KeyboardKey);
                }
            }
            categoryGrid.Refresh();
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = categoryGrid.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = categoryGrid.Rows[e.RowIndex];
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

        private void dataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            CategoryList.Add(new Category());
            categoryGrid.Rows[e.Row.Index - 1].Cells["Index"].Value = CategoryList.Count;
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int index = Convert.ToInt32(e.Row.Cells["Index"].Value) - 1;
            CategoryList.RemoveAt(index);
        }

        private void dataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            FillCategoryGrid(); // After deleting refill the whole grid
        }

        // Storage

        private const string DirectoryName = "Category Presets";

        private void FillPresetComboBox()
        {
            // Open store
            IsolatedStorageFile isf = IsolatedStorageFile.GetMachineStoreForAssembly();
            // Make sure the directory exists
            isf.CreateDirectory(DirectoryName); // Will succeed if it already exists.
            // Get file names
            string[] fileNames = isf.GetFileNames(Path.Combine(DirectoryName, "*.xml"));
            // Fill combo
            presetComboBox.Items.Clear();
            foreach (string fileName in fileNames)
            {
                presetComboBox.Items.Add(Path.GetFileNameWithoutExtension(fileName));
            }
            presetComboBox.Text = Settings.Default.CurrentCategoriesPresetName;
        }

        private void presetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPreset(presetComboBox.SelectedItem.ToString());
        }

        public void LoadPreset(string name)
        {
            // Open store
            IsolatedStorageFile isf = IsolatedStorageFile.GetMachineStoreForAssembly();
            // Construct path
            string path = Path.Combine(DirectoryName, name + ".xml");
            // File Exists?
            if (isf.FileExists(path))
            {
                // Load file
                using (var fs = new IsolatedStorageFileStream(path, FileMode.Open, FileAccess.Read, isf))
                {
                    // Create Serializer
                    var xmlSerializer = new XmlSerializer(typeof(List<Category>));
                    // Serialize
                    CategoryList = (List<Category>)xmlSerializer.Deserialize(fs);
                }
            }
            // Assign current preset name
            Settings.Default.CurrentCategoriesPresetName = name;
            Settings.Default.Save();
            // Rebind data
            FillCategoryGrid();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Settings.Default.CurrentCategoriesPresetName = presetComboBox.Text;
            Settings.Default.Save();
            SavePreset();
        }

        private void SavePreset()
        {
            // Prompt user for name
            Settings.Default.CurrentCategoriesPresetName = InputBox.Show("Please specify a name.", Settings.Default.CurrentCategoriesPresetName, Program.ApplicationName);
            // Get out of here if user cancelled
            if (String.IsNullOrWhiteSpace(Settings.Default.CurrentCategoriesPresetName)) { return; }
            // Save in isolated storage
            // Get store
            IsolatedStorageFile isf = IsolatedStorageFile.GetMachineStoreForAssembly();
            // Create directory
            isf.CreateDirectory(DirectoryName); // Will succeed if it already exists.
            // Construct file path
            string path = Path.Combine(DirectoryName, Settings.Default.CurrentCategoriesPresetName + ".xml");
            // Check file exists
            if (isf.FileExists(path))
            {
                // Prompt user for overwrite
                if (
                    MessageBox.Show(
                        "Overwrite preset '" + Settings.Default.CurrentCategoriesPresetName + "'?",
                        Program.ApplicationName,
                        MessageBoxButtons.OKCancel
                    )
                    == DialogResult.Cancel
                )
                {
                    // Get out of here if user cancelled
                    return;
                }
            }
            // Save file (overwrite if exists)
            // Create Stream
            using (var fs = new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.Write, isf))
            {
                // Create Serializer
                var xmlSerializer = new XmlSerializer(typeof(List<Category>));
                // Serialize
                xmlSerializer.Serialize(fs, CategoryList);
            }
            // Rebind ComboBox
            FillPresetComboBox();
        }

    }
}
