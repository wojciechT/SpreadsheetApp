using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spreadsheet
{
    public class SpreadsheetService
    {
        private readonly Evaluator _evaluator;

        public SpreadsheetService()
        {
            _evaluator = new Evaluator();
        }
        public SpreadsheetService(Evaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public Dictionary<string, double> EvaluateSpreadsheet(Dictionary<string, string> unevaluatedSpreadsheet)
        {
            var evaluatedSpreadsheet = new Dictionary<string, double>();

            foreach (var cell in unevaluatedSpreadsheet)
            {
                evaluatedSpreadsheet.Add(cell.Key, Evaluate(cell.Value, unevaluatedSpreadsheet));   
            }

            return evaluatedSpreadsheet;
        }

        public double Evaluate(string expression, Dictionary<string, string> spreadsheet)
        {
            //Jedna liczba
            if (double.TryParse(expression, out _))
            {
                return double.Parse(expression);
            }

            //Adres do komórki w arkuszsu
            var pattern = @"^[A-Z]{1}\d+$";
            var match = Regex.Match(expression, pattern);

            if (match.Success)
            {
                return Evaluate(Find(match.Value, spreadsheet), spreadsheet);
            }

            //Dwie liczby lub dwa adresy i operator
            pattern = @"^([A-Z]{0,1}\d+)\s*([-+*\/])\s*([A-Z]{0,1}\d+)$";

            match = Regex.Match(expression, pattern);

            var firstValue = match.Groups[1].Value;
            var secondValue = match.Groups[3].Value;

            switch (match.Groups[2].Value)
            {
                case "+":
                    return Evaluate(firstValue, spreadsheet) + Evaluate(secondValue, spreadsheet);
                case "-":
                    return Evaluate(firstValue, spreadsheet) - Evaluate(secondValue, spreadsheet);
                case "*":
                    return Evaluate(firstValue, spreadsheet) * Evaluate(secondValue, spreadsheet);
                case "/":
                    return Evaluate(firstValue, spreadsheet) / Evaluate(secondValue, spreadsheet);
            }

            return 0;
        }

        private string Find(string address, Dictionary<string, string> spreadsheet)
        {
            return spreadsheet[address];
        }
    }
}
