namespace LexicalAnalyzer
{
    /// <summary>
    /// Бинарный токен.
    /// </summary>
    public class BinaryToken : TokenBase
    {
        /// <summary>
        /// Состояние токена.
        /// </summary>
        private enum State
        {
            A,
            B,
            C,
            DFinal,
            E,
            F
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
                if (this.currentSymbolIndex == this.Value.Length && this.currentState == State.DFinal)
                    return -1;
                else if (this.currentSymbolIndex == this.Value.Length && this.currentState != State.DFinal)
                    return this.currentSymbolIndex - 1;

                this.currentSymbol = this.ReadNextSymbol();

                if (this.currentState == State.A)
                {
                    if (this.currentSymbol == '0')
                    {
                        this.currentState = State.B;
                        this.currentSymbolIndex++;
                        continue;
                    }
                    return this.currentSymbolIndex;
                }
                else if (this.currentState == State.B)
                {
                    if (this.currentSymbol == '0')
                    {
                        this.currentState = State.C;
                        this.currentSymbolIndex++;
                        continue;
                    }
                    return this.currentSymbolIndex;
                }
                else if (this.currentState == State.C)
                {
                    if (this.currentSymbol == '0')
                    {
                        this.currentState = State.A;
                        this.currentSymbolIndex++;
                        continue;
                    }
                    else if (this.currentSymbol == '1')
                    {
                        this.currentState = State.DFinal;
                        this.currentSymbolIndex++;
                        continue;
                    }
                    return this.currentSymbolIndex;
                }
                else if (this.currentState == State.DFinal)
                {
                    if (this.currentSymbol == '0')
                    {
                        this.currentState = State.E;
                        this.currentSymbolIndex++;
                        continue;
                    }
                    return this.currentSymbolIndex;
                }
                else if (this.currentState == State.E)
                {
                    if (this.currentSymbol == '1')
                    {
                        this.currentState = State.F;
                        this.currentSymbolIndex++;
                        continue;
                    }
                    return this.currentSymbolIndex;
                }
                else if (this.currentState == State.F)
                {
                    if (this.currentSymbol == '0')
                    {
                        this.currentState = State.DFinal;
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
        /// Создать экземпляр бинарного токена.
        /// </summary>
        /// <param name="value">Текст.</param>
        /// <param name="type">Тип токена.</param>
        public BinaryToken(string value, TokenType type) : base(value, type)
        {
        }
    }
}
