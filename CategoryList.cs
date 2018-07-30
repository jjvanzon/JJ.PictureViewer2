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

    [Serializable]
    public class CategoryList : List<Category>
    {

        public string Name;
        public bool IsDefault;

        public Category FindCategoryByKey(char keyboardKey)
        {
            return (
                from category in this
                where category.KeyboardKey.ToString().ToUpper() == keyboardKey.ToString().ToUpper()
                select category
                ).FirstOrDefault();
        }

        public Category FindCategoryBySubFolderName(string subFolderName)
        {
            return (
                from category in this
                where category.SubFolderName == subFolderName
                select category
                ).FirstOrDefault();
        }

        public void MoveToCategoryFolder(string folderPath, string fileName, Category category)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(folderPath);
            // Determine Target Directory
            DirectoryInfo targetDirectory = null;
            try
            {
                if (FindCategoryBySubFolderName(sourceDirectory.Name) != null)
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
            catch (ArgumentException) { throw new BadPathFormatApplicationException(); }
            // Create target directory when not exists
            if (!targetDirectory.Exists) { targetDirectory.Create(); }
            // Move the file
            File.Move(
                Path.Combine(sourceDirectory.FullName, fileName), 
                Path.Combine(targetDirectory.FullName, fileName));
        }

    }
}
