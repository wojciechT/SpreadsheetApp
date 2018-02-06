using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet
{
    public class CircularReferenceException : Exception
    {
        public string Address { get; }

        public CircularReferenceException(string address, string message):base(message)
        {
            Address = address;
        }

        public CircularReferenceException(string address)
        {
            Address = address;
        }

        public CircularReferenceException(string address, string message, Exception innerException) : base(message,
            innerException)
        {
            Address = address;
        }

        public override string ToString()
        {
            return $"Error on address: {Address} {base.ToString()}";
        }
    }
}
