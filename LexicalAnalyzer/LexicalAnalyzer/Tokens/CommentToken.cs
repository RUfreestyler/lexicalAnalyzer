namespace LexicalAnalyzer
{
    /// <summary>
    /// Комментарий.
    /// </summary>
    public class CommentToken : TokenBase
    {
        /// <summary>
        /// Состояние токена.
        /// </summary>
        private enum State
        {
            A,
            B,
            Final
        }

        /// <summary>
        /// Текущее состояние.
        /// </summary>
        private State currentState;

        public override int Validate()
        {
            this.currentState = State.A;

            while (true)
            {
                if (this.currentSymbolIndex == this.Value.Length && (this.currentState == State.Final || this.currentState == State.B))
                    return -1;
                this.currentSymbol = this.ReadNextSymbol();

                if (this.currentState == State.A)
                {
                    if (this.currentSymbol == '\'')
                    {
                        this.currentState = State.B;
                        this.currentSymbolIndex++;
                        continue;
                    }
                    return this.currentSymbolIndex;
                }
                else if (this.currentState == State.B)
                {
                    if (this.currentSymbol == '\n' || this.currentSymbol == '\0' || this.currentSymbolIndex == this.Value.Length - 1)
                        this.currentState = State.Final;
                    this.currentSymbolIndex++;
                    continue;
                }
                else if (this.currentState == State.Final)
                    return -1;
            }
        }

        /// <summary>
        /// Прочитать следующий символ токена.
        /// </summary>
        /// <returns>Символ.</returns>
        private char ReadNextSymbol()
        {
            return this.Value[this.currentSymbolIndex];
        }

        /// <summary>
        /// Создать экземпляр токена комментария.
        /// </summary>
        /// <param name="value">Текст.</param>
        /// <param name="type">Тип токена.</param>
        public CommentToken(string value, TokenType type) : base(value, type)
        {
        }
    }
}
