using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spreadsheet
{
    public class Resolver
    {
        public string ResolveInExpression(string expression, Dictionary<string, string> spreadsheet)
        {
            var pattern = @"[A-Z]\d+";

            foreach (Match match in Regex.Matches(expression, pattern))
            {

            }
        }

        public string ResolveInSpreadsheet(string expression, string rootAddress,
            Dictionary<string, string> spreadsheet)
        {

        }
    }
}
