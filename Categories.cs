//
//  PictureViewer2.Categories
//
//      Author: Jan-Joost van Zon
//      Date: 29-10-2010 - 30-10-2020
//
//  -----

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;

namespace PictureViewer2
{

    static class Categories
    {
        
        public static List<Category> List = new List<Category>(); // Not read only.

        public static void BuildDefaultCategoryList()
        {
            List.Clear();
            List.Add(new Category() { SubFolderName = "Greatest", KeyboardKey = 'S' });
            List.Add(new Category() { SubFolderName = "Great", KeyboardKey = 'G' });
            List.Add(new Category() { SubFolderName = "Good", KeyboardKey = 'Y' });
            List.Add(new Category() { SubFolderName = "Probably", KeyboardKey = 'P' });
            List.Add(new Category() { SubFolderName = "Maybe", KeyboardKey = 'M' });
            List.Add(new Category() { SubFolderName = "Don't Know", KeyboardKey = 'D' });
            List.Add(new Category() { SubFolderName = "Not Great", KeyboardKey = 'N' });
            List.Add(new Category() { SubFolderName = "Category", KeyboardKey = 'C' });
        }

        public static Category FindByKey(char key)
        {
            return (
                from category in List
                where category.KeyboardKey.ToString().ToUpper() == key.ToString().ToUpper()
                select category
                ).FirstOrDefault();
        }

        public static Category FindByName(string name)
        {
            return (
                from category in List
                where category.SubFolderName == name
                select category
                ).FirstOrDefault();
        }

        public static void MoveToCategoryFolder(string folderPath, string fileName, Category category)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(folderPath);
            // Determine Target Directory
            DirectoryInfo targetDirectory = null;
            try
            {
                if (Categories.FindByName(sourceDirectory.Name) != null)
                {
                    // If already in category folder, target directory is sibbling folder.
                    targetDirectory = new DirectoryInfo(Path.Combine(sourceDirectory.Parent.FullName, category.SubFolderName));
                }
                else
                {
                    // If already in category folder, target directory is child folder.
                    targetDirectory = new DirectoryInfo(Path.Combine(sourceDirectory.FullName, category.SubFolderName));
                }
            }
            catch (ArgumentException)
            {
                throw new BadPathFormatApplicationException();
            }
            // Create target directory when not exists
            if (!targetDirectory.Exists) { targetDirectory.Create(); }
            // Move the file
            File.Move(
                Path.Combine(sourceDirectory.FullName, fileName), 
                Path.Combine(targetDirectory.FullName, fileName));
        }

        private const string DirectoryName = "Category Presets";

        public static void LoadPreset(string name)
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
                    List = (List<Category>)xmlSerializer.Deserialize(fs);
                }
            }
        }


    }
}
