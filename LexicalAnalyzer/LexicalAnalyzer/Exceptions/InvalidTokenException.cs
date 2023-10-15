using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzer
{
    public class InvalidTokenException : Exception
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public InvalidTokenException(string message) : base(message)
        {
            
        }
    }
}
