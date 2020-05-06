using Steema.TeeChart.Styles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
#nullable enable

namespace PRNViewer
{
    class PrnReader : CsvReader
    {
        public PrnReader()
        {
            this.Name = "PRN Reader";
            this.SkippedRows = 1;
            this.DelimiterChars = new[] {' '};
            this.DecimalSeparator = ".";
            this.FileContainsHeaders = true;
        }
        
        /// <summary>
        /// Get piket name from file
        /// </summary>
        /// <param name="theException"></param>
        /// <returns>delimeitted piket name</returns>
        public string[] GetPkName(Exception? theException)
        {
            try
            {
                if (_readedFileLines != null)
                {
                    theException = null;
                    return _readedFileLines[0].Split("_");
                    
                }
                else
                    throw new IndexOutOfRangeException("There no readed lines");
            }
            catch (Exception e)
            {
                theException = e;
                return new string[0];
            }
        }
    }
}