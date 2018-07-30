//
//  PictureViewer2.Category
//
//      Author: Jan-Joost van Zon
//      Date: 29-10-2010 - 30-10-2020
//
//  -----

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PictureViewer2
{
    
    [Serializable]
    public class Category
    {
        [XmlElement] public string SubFolderName;
        [XmlElement] public char KeyboardKey;
    }

}
