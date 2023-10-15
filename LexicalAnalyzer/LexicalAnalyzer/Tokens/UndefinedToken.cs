using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzer
{
    public class UndefinedToken : TokenBase
    {
        public override int Validate()
        {
            return -1;
        }

        /// <summary>
        /// Создать экземпляр неопределенного токена.
        /// </summary>
        public UndefinedToken() : base (string.Empty, TokenType.Undefined)
        {
            
        }
    }
}
