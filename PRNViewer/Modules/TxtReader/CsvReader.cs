#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PRNViewer
{
    class CsvReader
    {
        /// <summary>
        /// Name of this reader
        /// </summary>
        public string Name { get; set; } = "CSV Reader";

        /// <summary>
        /// How much rows in file to skip until header
        /// </summary>
        public int SkippedRows { get; set; } = 0;

        /// <summary>
        /// Delimited cracters in the file
        /// </summary>
        public char[] DelimiterChars { get; set; } = { ',', ' ' };

        /// <summary>
        /// Decimal value separator in the file
        /// </summary>
        public string DecimalSeparator { get; set; } = ".";

        /// <summary>
        /// Are this file contsins headers?
        /// </summary>
        public bool FileContainsHeaders { get; set; } = true;

        /// <summary>
        /// Raw readed file lines
        /// </summary>
        protected string[]? _readedFileLines;

        /// <summary>
        /// Reading file 
        /// normal use: ReadFile(aPath, out _), for details see:
        /// https://devblogs.microsoft.com/dotnet/new-features-in-c-7-0/
        /// </summary>
        /// <param name="aPath">path to read file</param>
        /// <param name="theException">exception in case of the problems</param>
        /// <returns></returns>
        public string[]? ReadFile(string aPath, out Exception? theException)
        {
            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-from-a-text-file            
            try
            {
                _readedFileLines = System.IO.File.ReadAllLines(aPath);
                theException = null;
                return _readedFileLines;
            }
            catch (Exception e)
            {
                _readedFileLines = null;
                theException = e;
                return (null);
            }
        }

        /// <summary>
        /// Get headers from readed file
        /// normal use: GetHeaders(out _), for details see:
        /// https://devblogs.microsoft.com/dotnet/new-features-in-c-7-0/
        /// </summary>
        /// <param name="theException">exception in case of the problems</param>
        /// <returns>headers array</returns>
        public string[]? GetHeaders(out Exception? theException)
        {
            try
            {
                if (FileContainsHeaders)
                {
                    if (_readedFileLines != null)
                    {
                        theException = null;
                        return _readedFileLines[0 + SkippedRows].Split(DelimiterChars);
                    }
                    else
                        throw new IndexOutOfRangeException("There no readed lines");
                }
                else
                {
                    if (_readedFileLines != null)
                    {

                        // count cols
                        var consCount = _readedFileLines[0 + SkippedRows].Split(DelimiterChars).Length;
                        var res = Enumerable.Range(1, _readedFileLines[0 + SkippedRows].Split(DelimiterChars).Length)
                            .Select(i => $"Field_{i}").ToArray();
                        theException = null;
                        return res;
                    }
                    else
                        throw new IndexOutOfRangeException("There no readed lines");
                }
            }
            catch (Exception e)
            {
                theException = e;
                return null;
            }
        }

        /// <summary>
        /// To get data from file
        /// </summary>
        /// <returns>[*][] Cols, [][*] Rows</returns>
        public dynamic[,]? GetData(Exception? theException)
        {
            try
            {
                if (_readedFileLines != null)
                {
                    var rows = new List<dynamic[]>();

                    for (var iRow = SkippedRows + (FileContainsHeaders ? 1 : 0); iRow < _readedFileLines.Length; iRow++)
                    {
                        var row = _readedFileLines[iRow].Split(DelimiterChars);
                        var cells = new dynamic[row.Length];
                        for (var iCol = 0; iCol < row.Length; iCol++)
                        {
                            var provider = new NumberFormatInfo() { NumberDecimalSeparator = DecimalSeparator };
                            dynamic cell;
                            try
                            {
                                cell = Convert.ToDouble(row[iCol], provider);
                            }
                            catch
                            {
                                cell = row[iCol];
                            }
                            cells[iCol] = cell;
                        }
                        rows.Add(cells);
                    }

                    var maxColLen = rows.Select(r => r.Length).Max();
                    var res = new dynamic[maxColLen, rows.Count];
                    for (var iRow = 0; iRow < rows.Count; iRow++)
                    {
                        for (var iCol = 0; iCol < rows[iRow].Length; iCol++)
                        {
                            res[iCol, iRow] = rows[iRow][iCol];
                        }
                    }

                    theException = null;

                    return res;
                }
                else
                    throw new IndexOutOfRangeException("There no readed lines");
            }
            catch (Exception e)
            {
                theException = e;
                return null;
            }
        }
    }
}