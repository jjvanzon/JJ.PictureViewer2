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
                // Call isolated storage helper
                IsolatedStorageFile isf;
                string path;
                IsolatedStorageHelper(
                    categoryListName,
                    out isf,
                    out path);
                using (isf)
                {
                    // Load file
                    using (var fs = new IsolatedStorageFileStream(path, FileMode.Open, FileAccess.Read, isf))
                    {
                        // Create Serializer
                        var xmlSerializer = new XmlSerializer(typeof(CategoryList));
                        // Serialize
                        return (CategoryList)xmlSerializer.Deserialize(fs);
                    }
                }
            }
            // Last resort
            return null;
        }

        public bool CategoryListExists(string categoryListName)
        {
            // Call isolated storage helper
            IsolatedStorageFile isf;
            string path;
            IsolatedStorageHelper(
                categoryListName,
                out isf,
                out path);
            using (isf)
            {
                // Check file exists
                bool returnValue = isf.FileExists(path);
                // Close store
                isf.Close();
                // Return
                return returnValue;
            }
        }

        public void SaveCategoryList(CategoryList categoryList)
        {
            // Call isolated storage helper
            IsolatedStorageFile isf;
            string path;
            IsolatedStorageHelper(
                categoryList.Name,
                out isf,
                out path);
            using (isf)
            {
                // Save file (overwrite if exists)
                // Create Stream
                using (var fs = new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.Write, isf))
                {
                    // Create Serializer
                    var xmlSerializer = new XmlSerializer(typeof(CategoryList));
                    // Serialize
                    xmlSerializer.Serialize(fs, categoryList);
                }
            }
        }

        public string[] GetAllCategoryListNames()
        {
            // Call isolated storage helper
            IsolatedStorageFile isf;
            string path; // Dummy
            IsolatedStorageHelper(
                null,
                out isf,
                out path);
            using (isf)
            {
                // Get file names
                string[] fileNames = isf.GetFileNames(Path.Combine(DirectoryName, "*.xml"));
                // Get rid of extensions
                for (int i = 0; i < fileNames.Count(); i++)
                {
                    fileNames[i] = Path.GetFileNameWithoutExtension(fileNames[i]);
                }
                // Close store
                isf.Close();
                // return
                return fileNames;
            }
        }

        // Isolated Storage Helpers

        private string DirectoryName { get { return "Category Presets"; } }

        public void IsolatedStorageHelper(
            string categoryListName,
            out IsolatedStorageFile isf,
            out string path)
        {
            // Get store
            isf = IsolatedStorageFile.GetMachineStoreForAssembly();
            // Ensure directory
            isf.CreateDirectory(DirectoryName); // Will succeed if it already exists.
            // Set file path
            if (!String.IsNullOrWhiteSpace(categoryListName))
            {
                path = Path.Combine(DirectoryName, categoryListName + ".xml");
            }
            else
            {
                path = ""; 
            }
        }

    }

}
