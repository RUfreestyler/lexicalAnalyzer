namespace LexicalAnalyzer
{
    /// <summary>
    /// Токен.
    /// </summary>
    public abstract class TokenBase
    {
        /// <summary>
        /// Тип токена.
        /// </summary>
        public TokenType Type { get; protected set; } = TokenType.Undefined;

        /// <summary>
        /// Значение токена.
        /// </summary>
        public string Value { get; protected set; } = string.Empty;

        /// <summary>
        /// Текущий символ.
        /// </summary>
        protected char currentSymbol;

        /// <summary>
        /// Индекс текущего символа.
        /// </summary>
        protected int currentSymbolIndex = 0;

        /// <summary>
        /// Провалидировать токен.
        /// </summary>
        /// <returns>-1, если токен валиден, иначе индекс невалидного символа.</returns>
        public abstract int Validate();

        /// <summary>
        /// Создать экземпляр токена.
        /// </summary>
        /// <param name="value">Текст.</param>
        /// <param name="type">Тип токена.</param>
        public TokenBase(string value, TokenType type)
        {
            this.Type = type;
            this.Value = value;
        }
    }
}
