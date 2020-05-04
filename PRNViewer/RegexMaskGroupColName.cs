using System;
using System.Collections.Generic;
using System.Text;

namespace PRNViewer
{
    class RegexMaskGroupColName
    {
        public string Name { get; set; }
        public string Mask { get; set; }
        
        private string[] _headers = null;
        private string[][] _data = null;
        public string[] Groups { get; }


    }
}
