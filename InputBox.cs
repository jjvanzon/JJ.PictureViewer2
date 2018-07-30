using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictureViewer2
{
    public static class InputBox
    {

        public static string Show(string prompt, string value, string title = "")
        {
            var w = new InputBoxWindow();
            w.promptLabel.Text = prompt;
            w.valueTextBox.Text = value;
            w.Text = title;
            w.ShowDialog();
            return w.valueTextBox.Text;
        }

    }
}
