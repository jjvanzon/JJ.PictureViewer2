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

namespace PictureViewer2
{
    static class Categories
    {
        
        public static readonly List<Category> List = new List<Category>();

        public static void BuildDefaultCategoryList()
        {
            List.Clear();
            List.Add(new Category() { Name = "Greatest", Key = 'S' });
            List.Add(new Category() { Name = "Great", Key = 'G' });
            List.Add(new Category() { Name = "Good", Key = 'Y' });
            List.Add(new Category() { Name = "Probably", Key = 'P' });
            List.Add(new Category() { Name = "Maybe", Key = 'M' });
            List.Add(new Category() { Name = "Don't Know", Key = 'D' });
            List.Add(new Category() { Name = "Not Great", Key = 'N' });
            List.Add(new Category() { Name = "Category", Key = 'C' });
        }

        public static Category FindByKey(char key)
        {
            return (
                from category in List
                where category.Key.ToString().ToUpper() == key.ToString().ToUpper()
                select category
                ).FirstOrDefault();
        }

        public static Category FindByName(string name)
        {
            return (
                from category in List
                where category.Name == name
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
                    targetDirectory = new DirectoryInfo(Path.Combine(sourceDirectory.Parent.FullName, category.Name));
                }
                else
                {
                    // If already in category folder, target directory is child folder.
                    targetDirectory = new DirectoryInfo(Path.Combine(sourceDirectory.FullName, category.Name));
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

    }
}
