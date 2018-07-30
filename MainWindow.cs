//
//  PictureViewer2.MainWindow
//
//      Author: Jan-Joost van Zon
//      Date: 29-10-2010 - 30-10-2020
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
using System.Configuration;
using PictureViewer2.Properties;

namespace PictureViewer2
{

    public partial class MainWindow : Form
    {

        // Initialization & Finalization

        public MainWindow()
        {
            InitializeComponent();
            var repository = new CategoryListRepository();
            CategoryList = repository.GetCurrentOrDefaultCategoryList();
            BindKeyPress();
            RequestDraw();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }

        // Category List

        private CategoryList CategoryList;

        // Selecting Items

        private void folderPathTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SelectFolder(folderPathTextBox.Text);
                fileListBox.Focus();
            }
        }

        private void folderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectFolder(folderTreeView.GetPath());
        }

        private void SelectFolder(string path)
        {
            try
            {
                if (folderPathTextBox.Text != path) folderPathTextBox.Text = path;
                if (folderTreeView.GetPath() != path) folderTreeView.SetPath(folderPathTextBox.Text);
                fileListBox.Fill(folderPathTextBox.Text, resetSelection: true);
            }
            catch (DirectoryNotFoundException) { MessageBox.Show("The specified path does not exist", Program.ApplicationName); }
            catch (NodeNotFoundApplicationException) { MessageBox.Show("Application error: an element of the path could not be opened in the folder explorer.", Program.ApplicationName); }
            catch (IOException ex) { MessageBox.Show("Error: " + ex.Message, Program.ApplicationName); }       
        }

        private void fileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPicture(Path.Combine(folderPathTextBox.Text, (string)fileListBox.SelectedItem));
        }

        private void SelectFile(int index)
        {
            if (fileListBox.SelectedIndex != index)
            {
                if (
                    index >= 0 &&
                    index < fileListBox.Items.Count
                )
                {
                    fileListBox.SelectedIndex = index;
                }
            }
        }

        private void ShowPicture(string path)
        {
            try
            {
                pictureBox.Load(path);
            }
            catch
            {
                // Do not make error handling more specific than this. 
                // Picture simply would not load. Wrong file type, wrong format, corrupt file, whatever.
                // Do clear the previous image, though.
                pictureBox.Image = null;
            }
        }

        // Keyboard Keys

        private void BindKeyPress()
        {
            this.KeyPress += KeyPressHandler;
            fileListBox.KeyPress += KeyPressHandler;
        }

        private void KeyPressHandler(object sender, KeyPressEventArgs e)
        {
            ProcessKey(e.KeyChar);
            e.Handled = true;
        }

        private void ProcessKey(char KeyChar)
        {
            // Process Full Screen Keys
            switch (KeyChar)
            {
                case (char)27: // Escape
                    FullScreen = false;
                    return;
            }
            Category category = CategoryList.FindCategoryByKey(KeyChar);
            if (category != null)
            {
                try
                {
                    CategoryList.MoveToCategoryFolder(
                        folderPathTextBox.Text,
                        (string)fileListBox.SelectedItem,
                        category);
                }
                catch (IOException) { MessageBox.Show("File access error. File may already exist.", Program.ApplicationName); }
                catch (BadPathFormatApplicationException) { MessageBox.Show("Bad folder format. Check the category configuration.", Program.ApplicationName); }
                fileListBox.Fill(folderPathTextBox.Text);
                // Trick for getting the list box to focus again, so that the arrow keys will work.
                fileListBox.Focus();
                if (!fileListBox.Focused) { fileListBox.Visible = true; fileListBox.Focus(); fileListBox.Visible = false; }
            }
        }

        // Get arrow-key functionality when clicking picture

        private void pictureBox_Click(object sender, EventArgs e)
        {
            fileListBox.Focus();
        }

        // Full Screen

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            FullScreen = !FullScreen;
        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FullScreen = !FullScreen;
        }

        private bool _fullscreen;
        private bool FullScreen
        {
            get
            {
                return _fullscreen;
            }
            set
            {
                if (_fullscreen != value)
                {
                    // Store
                    _fullscreen = value;
                    // Apply
                    FormBorderStyle = _fullscreen ? FormBorderStyle.None : FormBorderStyle.Sizable;
                    WindowState = _fullscreen ? FormWindowState.Maximized : FormWindowState.Normal;
                    // Draw
                    RequestDraw();
                }
            }
        }

        // Resize

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            RequestDraw();
        }

        private void RequestDraw()
        {
            drawTimer.Enabled = true;
        }

        private void drawTimer_Tick(object sender, EventArgs e)
        {
            Draw();
        }

        private const int LeftPanelWidth = 300;

        private void Draw()
        {
            drawTimer.Enabled = false;
            SuspendLayout();
            // Visibility
            folderPathTextBox.Visible = !FullScreen;
            folderTreeView.Visible = !FullScreen;
            // Do this last, so that fileListBox is the last control that was visible,
            // so that it gets focus, even though it is invisible, so that the arrow keys
            // will result in next-previous behavior.
            fileListBox.Visible = !FullScreen; 
            menuStrip.Visible = !FullScreen;
            // Positioning
            if (FullScreen)
            {
                pictureBox.Left = 0;
                pictureBox.Top = 0;
                pictureBox.Width = this.ClientSize.Width;
                pictureBox.Height = this.ClientSize.Height;
            }
            else
            {
                folderPathTextBox.Left = 0;
                folderPathTextBox.Top = menuStrip.Bottom;
                folderPathTextBox.Width = LeftPanelWidth;
                folderTreeView.Left = 0;
                folderTreeView.Top = folderPathTextBox.Bottom;
                folderTreeView.Width = LeftPanelWidth / 2;
                folderTreeView.Height = this.ClientSize.Height - folderPathTextBox.Bottom;
                fileListBox.Left = LeftPanelWidth / 2;
                fileListBox.Top = folderPathTextBox.Bottom;
                fileListBox.Width = LeftPanelWidth / 2;
                fileListBox.Height = this.ClientSize.Height - fileListBox.Top;
                pictureBox.Left = LeftPanelWidth;
                pictureBox.Top = menuStrip.Bottom;
                pictureBox.Width = this.ClientSize.Width - LeftPanelWidth;
                pictureBox.Height = this.ClientSize.Height - menuStrip.Bottom;
            }
            ResumeLayout();
        }

        // Settings

        private void ApplySettings()
        {
            // Folder
            string folderPath = Settings.Default.FolderPath;
            if (!String.IsNullOrWhiteSpace(folderPath))
            {
                SelectFolder(folderPath);
            }
            // File
            SelectFile(Settings.Default.SelectedFileIndex);
            // Window Positions
            int width = Settings.Default.WindowWidth;
            int height = Settings.Default.WindowHeight;
            if (width > 0 && height > 0)
            {
                Left = Settings.Default.WindowLeft;
                Top = Settings.Default.WindowTop;
                Width = width;
                Height = height;
            }
        }

        private void SaveSettings()
        {
            Settings.Default.FolderPath = folderPathTextBox.Text;
            Settings.Default.SelectedFileIndex = fileListBox.SelectedIndex;
            Settings.Default.WindowLeft = Left;
            Settings.Default.WindowTop = Top;
            Settings.Default.WindowWidth = Width;
            Settings.Default.WindowHeight = Height;
            Settings.Default.Save();
        }

        // Categories

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var categoriesWindow = new CategoriesWindow();
            categoriesWindow.ShowDialog();
        }

    }
}
