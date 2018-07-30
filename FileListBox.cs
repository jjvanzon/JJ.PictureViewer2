//
//  PictureViewer2.FileListBox
//
//      Author: Jan-Joost van Zon
//      Date: 29-10-2010 - 30-10-2020
//
//  -----

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PictureViewer2
{

    public partial class FileListBox : ListBox
    {

        public FileListBox()
        {
            InitializeComponent();
        }

        public void Fill(string folderPath, bool resetSelection = false)
        {
            // Remember list index
            int selectedIndex = -1;
            if (!resetSelection) 
            { 
                selectedIndex = SelectedIndex; 
            }
            // Refresh list
            Items.Clear();
            foreach (FileInfo fi in new DirectoryInfo(folderPath).GetFiles())
            {
                Items.Add(fi.Name);
            }
            // Restore list index
            if (!resetSelection)
            {
                // Correct when list index too large
                if (selectedIndex > Items.Count - 1)
                {
                    selectedIndex = Items.Count - 1;
                }
            }
            else
            {
                selectedIndex = 0;
            }
            // Assign list index when any items at all
            if (Items.Count > 0)
            {
                SelectedIndex = selectedIndex;
            }
        }

    }

}
