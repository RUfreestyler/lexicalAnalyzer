using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzer
{
    /// <summary>
    /// Исключение, сигнализирующее о том, что токен написан не правильно.
    /// </summary>
    public class InvalidTokenException : Exception
    {
        /// <summary>
        /// Строка, в которой найдена ошибка. 
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Колонка, в которой найдена ошибка.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Создать экземпляр исключения.
        /// </summary>
        /// <param name="message"></param>
        public InvalidTokenException(string message) : base(message)
        {
            
        }
    }
}
