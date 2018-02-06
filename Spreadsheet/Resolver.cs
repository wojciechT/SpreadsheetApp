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
                var replacement = spreadsheet[match.Value];
                expression = expression.Replace(match.Value, replacement);
            }

            return expression;
        }

        public string ResolveInSpreadsheet(string expression, string address, List<string> addressTrace,
            Dictionary<string, string> spreadsheet)
        {
            var pattern = @"[A-Z]\d+";

            if (addressTrace.Contains(address))
            {
                throw new CircularReferenceException(address, "Circular reference found.");
            }

            addressTrace.Add(address);

            if (double.TryParse(expression, out _))
            {
                return expression;
            }

            foreach (Match match in Regex.Matches(expression,pattern))
            {
                expression = expression.Replace(match.Value, ResolveInSpreadsheet(spreadsheet[match.Value], match.Value, addressTrace.ToList(), spreadsheet));
            }

            return expression;
        }
    }
}
