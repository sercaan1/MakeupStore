using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class NonpositiveQuantityException : Exception
    {
        public NonpositiveQuantityException() : base("At least one item")
        {
        }

        public NonpositiveQuantityException(string message) : base(message)
        {
        }

        public NonpositiveQuantityException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
