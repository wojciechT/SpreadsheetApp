using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spreadsheet
{
    public class Evaluator
    {
        public string Parse(string expression)
        {
            var precedence = new Dictionary<string, int>
            {
                {"+", 2 },
                {"-", 2 },
                {"/", 1 },
                {"*", 1 }
            };

            var output = new LinkedList<string>();
            var operators = new Stack<string>();

            const string pattern = @"([-]{0,1}\d+)\s*([-+*\/]){0,1}"; //Matches number and zero to one operators
            var matches = Regex.Matches(expression, pattern);

            foreach (Match match in matches)
            {
                output.AddLast(match.Groups[1].Value);

                if (precedence.ContainsKey(match.Groups[2].Value))
                {
                    var operatorToInsert = match.Groups[2].Value;

                    if (operators.Count == 0)
                    {
                        operators.Push(operatorToInsert);
                        continue;
                    }

                    while (operators.Count > 0 && precedence[operators.Peek()] <= precedence[operatorToInsert])
                    {
                        output.AddLast(operators.Pop());
                    }
                    operators.Push(operatorToInsert);
                }
            }

            while (operators.Count > 0)
            {
                output.AddLast(operators.Pop());
            }

            var sb = new StringBuilder();

            foreach (var item in output)
            {
                sb.Append(item);
                sb.Append(" ");
            }

            return sb.ToString().Trim();
        }

        public double Evaluate(string rpnExpression)
        {
            var workStack = new Stack<double>();
            var expressionParts = rpnExpression.Split(' ');

            foreach (var part in expressionParts)
            {
                if (double.TryParse(part, out _))
                {
                    workStack.Push(double.Parse(part));
                    continue;
                }

                var right = workStack.Pop();
                var left = workStack.Pop();
                workStack.Push(Calculate(left, right, part));
            }
            return workStack.Pop();
        }

        private double Calculate(double left, double right, string operation)
        {
            switch (operation)
            {
                case "+":
                    return left + right;
                case "-":
                    return left - right;
                case "*":
                    return left * right;
                case "/":
                    return left / right;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}