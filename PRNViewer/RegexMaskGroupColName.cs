using System;
using System.Collections.Generic;
using System.Linq;
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
            for (var i =0;i<_headers.Length;i++)
            {
                var header = _headers[i];
                if (_maskRegex.IsMatch(header))
                    res.Add((header, i));
            }
            return res;
        }

        public List<(string, string[])> GetGroupsAsString()
        {
            var hGroups = this.Group();
            var res = new List<(string, string[])>();

            foreach (var hGroup in hGroups)
            {
                res.Add((hGroup.Item1, _data[hGroup.Item2]));
            }

            return res;
        }

        public List<(string,double[])> GetGroupsaAsDouble()
        {
            var res = new List<(string, double[])>();
            var groupsAsStiring = GetGroupsAsString();
            foreach(var groupAsStr in groupsAsStiring)
            {
                var datAsString = groupAsStr.Item2;
                var datAsDouble = datAsString.Select(d => Convert.ToDouble(d)).ToArray();
                res.Add((groupAsStr.Item1, datAsDouble));
            }

            return res;
        }
    }
}
