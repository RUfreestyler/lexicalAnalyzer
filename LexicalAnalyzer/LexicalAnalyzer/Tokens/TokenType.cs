namespace LexicalAnalyzer
{
    /// <summary>
    /// Тип токена.
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// Бинарный.
        /// </summary>
        Binary,

        /// <summary>
        /// Буквенный.
        /// </summary>
        Literal,

        /// <summary>
        /// Комментарий.
        /// </summary>
        Comment,

        /// <summary>
        /// Неопределенный.
        /// </summary>
        Undefined
    }
}
