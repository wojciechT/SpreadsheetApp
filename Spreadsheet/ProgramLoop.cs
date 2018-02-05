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
        public void Run()
        {
            var spreadsheetLine = string.Empty;
            while (!spreadsheetLine.EndsWith(";"))
            {
                spreadsheetLine = Console.ReadLine();
                var lineData = spreadsheetLine.Split('|');
                if (spreadsheetLine.EndsWith(";"))
                {
                    lineData[lineData.Length - 1] = lineData[lineData.Length - 1].Remove(lineData[lineData.Length - 1].Length - 1);
                }
                var spreadsheetLineData = lineData.Select(l => Evaluate(l)).ToList();
            }
        }

        private double Evaluate(string expression)
        {
            //Jedna liczba
            if (double.TryParse(expression, out _))
            {
                return double.Parse(expression);
            }

            //Adres do komórki w arkuszsu
            var pattern = @"^[A-Z]{0,1}\d+$";
            var match = Regex.Match(expression, pattern);


            //Dwie liczby lub dwa adresy i operator
            pattern = @"^(\d+)\s*([-+*\/])\s*(\d+)$";

            match = Regex.Match(expression, pattern);

            var firstValue = match.Groups[1].Value;
            var secondValue = match.Groups[3].Value;

            switch (match.Groups[2].Value)
            {
                case "+":
                    return Evaluate(firstValue) + Evaluate(secondValue);
                case "-":
                    return Evaluate(firstValue) - Evaluate(secondValue);
                case "*":
                    return Evaluate(firstValue) * Evaluate(secondValue);
                case "/":
                    return Evaluate(firstValue) / Evaluate(secondValue);
            }

            //Adres typu jedna litera kolumny, jedna cyfra rzędu


            return 0;
        }

        private double Find(string address, Dictionary<string, string> spreadsheet)
        {
            return 0;
        }
    }
}
