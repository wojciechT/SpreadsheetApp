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
        private readonly Resolver _resolver;
        private readonly Evaluator _evaluator;

        public ProgramLoop()
        {
            _resolver = new Resolver();
            _evaluator = new Evaluator();
        }

        public ProgramLoop(Resolver resolver, Evaluator evaluator)
        {
            _resolver = resolver;
            _evaluator = evaluator;
        }

        public void Run()
        {
            var spreadsheet = new Dictionary<string, string>();

            var spreadsheetLine = string.Empty;
            var addressChar = 'A';

            var index = 1;
            while (!spreadsheetLine.EndsWith(";"))
            {
                spreadsheetLine = Console.ReadLine();
                var lineData = spreadsheetLine.Split('|');

                if (spreadsheetLine.EndsWith(";"))
                {
                    lineData[lineData.Length - 1] = lineData[lineData.Length - 1].Remove(lineData[lineData.Length - 1].Length - 1);
                }

                foreach (var item in lineData)
                {
                    spreadsheet.Add(addressChar + index.ToString(), item);
                    addressChar++;
                }
                index++;
                addressChar = 'A';
            }

            var evaluatedSpreadsheet = new Dictionary<string, string>();

            foreach (var cell in spreadsheet)
            {
                evaluatedSpreadsheet.Add(cell.Key, _resolver.ResolveInSpreadsheet(cell.Value, cell.Key, new List<string>(), spreadsheet));
            }

            Console.Write("Input expression to evaluate: ");
            var expression = Console.ReadLine();
            var result = _evaluator.ParseAndEvaluate(_resolver.ResolveInExpression(expression, evaluatedSpreadsheet));

            Console.Write($"{result}");
        }
    }
}
