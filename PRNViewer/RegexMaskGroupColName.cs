using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PRNViewer
{
    class RegexMaskGroupColName
    {
        public string Name { get; set; }
        
        private string _maskString;
        private Regex _maskRegex = new Regex("", RegexOptions.Compiled);
        public string Mask 
        {
            get => _maskString;
            set
            {
                _maskString = value;
                _maskRegex = new Regex(value, RegexOptions.Compiled);
            }
        }
        
        private string[] _headers = null;
        private string[][] _data = null;
        
        public RegexMaskGroupColName(string[] Headers, string[][] Data)
        {
            _headers = Headers;
            _data = Data;
        }

        public List<(string, int)> Group()
        {
            var res = new List<(string, int)>();

            return res;
        }



    }
}
