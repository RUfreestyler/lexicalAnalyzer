namespace LexicalAnalyzer
{
    /// <summary>
    /// Неопределенный токен.
    /// </summary>
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
