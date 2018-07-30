//
//  PictureViewer2.FolderTreeView
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

    public partial class FolderTreeView : UserControl
    {

        // Initialize

        public FolderTreeView()
        {
            InitializeComponent();
            InitialFill();
        }

        // Fill

        private void InitialFill()
        {
            foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = driveInfo.Name;
                treeNode.Nodes.Add(new TreeNode()); // Add dummy node
                treeView.Nodes.Add(treeNode);
            }
        }

        // Selection & Expansion

        public event TreeViewEventHandler AfterSelect;
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (AfterSelect != null) AfterSelect(sender, e);
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.RemoveAt(0); // Remove dummy node
            try
            {
                foreach (DirectoryInfo directoryInfo in new DirectoryInfo(TreeNodeToPath(e.Node)).GetDirectories())
                {
                    if (!directoryInfo.Attributes.HasFlag(FileAttributes.Hidden))
                    {
                        TreeNode treeNode = new TreeNode();
                        treeNode.Text = directoryInfo.Name;
                        treeNode.Nodes.Add(new TreeNode()); // Add dummy node
                        e.Node.Nodes.Add(treeNode);
                    }
                }
            }
            catch (IOException ex) { MessageBox.Show("Error: " + ex.Message, Program.ApplicationName); }
            catch (UnauthorizedAccessException) { MessageBox.Show("You do not have permission to access this folder.", Program.ApplicationName); }
        }

        private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            // Clear child nodes
            e.Node.Nodes.Clear();
            // Add dummy node
            e.Node.Nodes.Add(new TreeNode()); 
        }

        // Get & Set Path

        public string GetPath()
        {
            return TreeNodeToPath(treeView.SelectedNode);
        }

        public void SetPath(string value)
        {
            // Condition
            if (!Directory.Exists(value)) throw new DirectoryNotFoundException();
            // Process Path Elements
            string[] pathElements = value.Split(Path.DirectorySeparatorChar);
            // Get Root Node
            TreeNode rootNode = (
                from TreeNode node in treeView.Nodes
                where node.Text == pathElements[0] + Path.DirectorySeparatorChar // TO DO: check if it needs an extra backslash
                select node
                ).FirstOrDefault();
            // Expand Root Node
            if (rootNode == null) throw new NodeNotFoundApplicationException("Root node not found");
            rootNode.Expand();
            // Expand Descendant Nodes
            // Assign root node as parent node 
            TreeNode parentNode = rootNode;
            // Go through all the path elements, but the first
            TreeNode n;
            foreach (string pathElement in pathElements.Skip(1))
            {
                // Get the child node for this path element
                n = (
                    from TreeNode node in parentNode.Nodes
                    where node.Text == pathElement
                    select node
                    ).FirstOrDefault();
                // Expand child node
                if (n == null) throw new NodeNotFoundApplicationException("Node not found");
                n.Expand();
                // Assign child node as parent node
                parentNode = n;
            }
        }

        private string TreeNodeToPath(TreeNode treeNode)
        {
            if (treeNode == null) return String.Empty;
            string path = treeNode.Text;
            while (treeNode.Parent != null)
            {
                treeNode = treeNode.Parent;
                path = Path.Combine(treeNode.Text, path);
            }
            return path;
        }

    }

}


// Old

    // Initial code for showing special folders too

        /*
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        treeView.Nodes.Add(desktopPath, "Desktop");
        TreeNode desktopTreeNode = treeView.Nodes[Environment.GetFolderPath(Environment.SpecialFolder.Desktop)];
            desktopTreeNode.Nodes.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Documents");
            desktopTreeNode.Nodes.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "My Computer");
            foreach (DirectoryInfo directoryInfo in new DirectoryInfo(desktopPath).GetDirectories())
            {
                desktopTreeNode.Nodes.Add(directoryInfo.FullName, directoryInfo.Name);
            }
        */
