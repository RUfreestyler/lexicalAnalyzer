namespace LexicalAnalyzer
{
    /// <summary>
    /// Буквенный токен.
    /// </summary>
    public class LiteralToken : TokenBase
    {
        /// <summary>
        /// Состояние токена.
        /// </summary>
        private enum State
        {
            A,
            B,
            CFinal
        }

        /// <summary>
        /// Текущее состояние.
        /// </summary>
        private State currentState;

        /// <summary>
        /// Символы, которые могут использоваться в токене.
        /// </summary>
        private char[] validSymbols = new char[] { 'a', 'b', 'c', 'd' };

        public override int Validate()
        {
            this.currentState = State.A;
            while (true)
            {
                if (this.currentSymbolIndex == this.Value.Length && this.currentState == State.CFinal)
                    return -1;
                else if (this.currentSymbolIndex == this.Value.Length && this.currentState != State.CFinal)
                    return currentSymbolIndex - 1;
                this.currentSymbol = this.ReadNextSymbol();

                if (this.currentState == State.A)
                {
                    if (this.currentSymbol == 'a')
                    {
                        this.currentState = State.B;
                        this.currentSymbolIndex++;
                        continue;
                    }
                    return this.currentSymbolIndex;
                }
                else if (this.currentState == State.B)
                {
                    if (this.currentSymbol == 'b')
                    {
                        this.currentState = State.CFinal;
                        this.currentSymbolIndex++;
                        continue;
                    }
                    return this.currentSymbolIndex;
                }
                else if (this.currentState == State.CFinal)
                {
                    if (this.validSymbols.Contains(this.currentSymbol))
                    {
                        this.currentSymbolIndex++;
                        continue;
                    }
                    return this.currentSymbolIndex;
                }
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
        /// Создать экземпляр буквенного токена.
        /// </summary>
        /// <param name="value">Текст.</param>
        /// <param name="type">Тип токена.</param>
        public LiteralToken(string value, TokenType type) : base(value, type)
        {
        }
    }
}
