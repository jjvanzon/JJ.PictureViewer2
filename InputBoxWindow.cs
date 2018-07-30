//
//  PictureViewer2.InputBoxWindow
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

namespace PictureViewer2
{

    internal partial class InputBoxWindow : Form
    {

        public InputBoxWindow()
        {
            InitializeComponent();
        }

        private void InputBoxWindow_Shown(object sender, EventArgs e)
        {
            valueTextBox.Focus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            valueTextBox.Text = String.Empty;
            Close();
        }

    }
}
