//
//  PictureViewer2.Program
//
//      Author: Jan-Joost van Zon
//      Date: 29-10-2010 - 30-10-2020
//
//  -----

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PictureViewer2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        public static string ApplicationName { get { return "Picture Viewer"; } }
    }
}
