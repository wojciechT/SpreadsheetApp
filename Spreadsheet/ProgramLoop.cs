using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spreadsheet
{
    class ProgramLoop
    {
        private readonly SpreadsheetService _spreadsheetService;

        public ProgramLoop()
        {
            _spreadsheetService = new SpreadsheetService();
        }

        public ProgramLoop(SpreadsheetService spreadsheetService)
        {
            _spreadsheetService = spreadsheetService;
        }

        public void Run()
        {
            var unevaluatedSpreadsheet = new Dictionary<string, string>();
            var evaluatedSpreadsheet = new Dictionary<string, double>();

            var spreadsheetLine = string.Empty;
            var addressChar = 'A';

            while (!spreadsheetLine.EndsWith(";"))
            {
                spreadsheetLine = Console.ReadLine();
                var lineData = spreadsheetLine.Split('|');

                if (spreadsheetLine.EndsWith(";"))
                {
                    lineData[lineData.Length - 1] = lineData[lineData.Length - 1].Remove(lineData[lineData.Length - 1].Length - 1);
                }

                for (var i = 1; i <= lineData.Length; i++)
                {
                    unevaluatedSpreadsheet.Add(addressChar + i.ToString(), lineData[i - 1]);
                }

                addressChar++;
            }

            evaluatedSpreadsheet = _spreadsheetService.EvaluateSpreadsheet(unevaluatedSpreadsheet);
        }
    }
}
