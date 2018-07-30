//
//  PictureViewer2.CategoryListRepository
//
//      Author: Jan-Joost van Zon
//      Date: 31-10-2010 - 31-10-2010
//
//  -----
//
//      Uses app.config and isolated storage for storage.
//
//  -----

using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Xml.Serialization;
using PictureViewer2.Properties;

namespace PictureViewer2
{

    class CategoryListRepository
    {

        // Data Store

        IsolatedStorageFile IsolatedStorageFile;

        // Initialization & Finalization

        public CategoryListRepository()
        {
            // Open Isolated Storage File
            // Get store
            IsolatedStorageFile = IsolatedStorageFile.GetMachineStoreForAssembly();
            // Ensure directory
            IsolatedStorageFile.CreateDirectory(DirectoryName); // Will succeed if it already exists.
        }

        ~CategoryListRepository()
        {
            // Close Isolated Storage File
            IsolatedStorageFile.Close();
        }

        // Repository Methods

        public string CurrentCategoryListName
        {
            get
            {
                return Settings.Default.CurrentCategoryListName;
            }
            set
            {
                Settings.Default.CurrentCategoryListName = value;
                Settings.Default.Save();
            }
        }

        public CategoryList GetDefaultCategoryList()
        {
            var categoryList = new CategoryList();
            categoryList.IsDefault = true;
            categoryList.Add(new Category() { SubFolderName = "Greatest", KeyboardKey = 'S' });
            categoryList.Add(new Category() { SubFolderName = "Great", KeyboardKey = 'G' });
            categoryList.Add(new Category() { SubFolderName = "Good", KeyboardKey = 'Y' });
            categoryList.Add(new Category() { SubFolderName = "Probably", KeyboardKey = 'P' });
            categoryList.Add(new Category() { SubFolderName = "Maybe", KeyboardKey = 'M' });
            categoryList.Add(new Category() { SubFolderName = "Don't Know", KeyboardKey = 'D' });
            categoryList.Add(new Category() { SubFolderName = "Not Great", KeyboardKey = 'N' });
            categoryList.Add(new Category() { SubFolderName = "Category", KeyboardKey = 'C' });
            return categoryList;
        }

        public CategoryList GetCurrentCategoryList()
        {
            return GetCategoryListByName(CurrentCategoryListName);
        }

        public CategoryList GetCurrentOrDefaultCategoryList()
        {
            if (!String.IsNullOrWhiteSpace(CurrentCategoryListName))
            {
                return GetCurrentCategoryList();
            }
            else
            {
                return GetDefaultCategoryList();
            }
        }

        public CategoryList GetCategoryListByName(string categoryListName)
        {
            // File Exists?
            if (CategoryListExists(categoryListName))
            {
                // Construct Path
                string path = NameToPath(categoryListName);
                // Load file
                using (var fs = new IsolatedStorageFileStream(path, FileMode.Open, FileAccess.Read, IsolatedStorageFile))
                {
                    // Create Serializer
                    var xmlSerializer = new XmlSerializer(typeof(CategoryList));
                    // Deserialize
                    CategoryList returnValue = (CategoryList)xmlSerializer.Deserialize(fs);
                    // Set name (name property didn't serialize)
                    returnValue.Name = categoryListName;
                    // Return
                    return returnValue;
                }
            }
            // Last resort
            return null;
        }

        public bool CategoryListExists(string categoryListName)
        {
            // Construct Path
            string path = NameToPath(categoryListName);
            // Check file exists
            bool returnValue = IsolatedStorageFile.FileExists(path);
            // Return
            return returnValue;
        }

        public void SaveCategoryList(CategoryList categoryList)
        {
            // Construct Path
            string path = NameToPath(categoryList.Name);
            // Save file (overwrite if exists)
            // Create Stream
            using (var fs = new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.Write, IsolatedStorageFile))
            {
                // Create Serializer
                var xmlSerializer = new XmlSerializer(typeof(CategoryList));
                // Serialize
                xmlSerializer.Serialize(fs, categoryList);
            }
        }

        public string GetFirstCategoryListName()
        {
            string fileName = IsolatedStorageFile.GetFileNames(Path.Combine(DirectoryName, "*.xml")).FirstOrDefault();
            return Path.GetFileNameWithoutExtension(fileName);
        }

        public string[] GetAllCategoryListNames()
        {
            // Get file names
            string[] fileNames = IsolatedStorageFile.GetFileNames(Path.Combine(DirectoryName, "*.xml"));
            // Get rid of extensions
            for (int i = 0; i < fileNames.Count(); i++)
            {
                fileNames[i] = Path.GetFileNameWithoutExtension(fileNames[i]);
            }
            // return
            return fileNames;
        }

        public void Rename(CategoryList categoryList, string newName)
        {
            // Delete old one
            DeleteCategoryList(categoryList.Name);
            // Change name property
            categoryList.Name = newName;
            // Save new one
            SaveCategoryList(categoryList);
        }

        public void DeleteCategoryList(string name)
        {
            string path = NameToPath(name);
            IsolatedStorageFile.DeleteFile(path);
        }
    
        // Helpers

        private string DirectoryName { get { return "Category Presets"; } }

        private string NameToPath(string name)
        {
            return Path.Combine(DirectoryName, name + ".xml");
        }

    }

}
