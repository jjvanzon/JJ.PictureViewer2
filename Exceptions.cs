using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictureViewer2
{

    class NodeNotFoundApplicationException : ApplicationException
    {
        public NodeNotFoundApplicationException() : base("Node not found.") { }
        public NodeNotFoundApplicationException(string message) : base(message) { }
    }

    class BadPathFormatApplicationException : ApplicationException
    {
        public BadPathFormatApplicationException() : base("Bad path format.") { }
        public BadPathFormatApplicationException(string message) : base(message) { }
    }

}
