namespace LexicalAnalyzer
{
    /// <summary>
    /// Лексический анализатор.
    /// </summary>
    public class LexicalAnalyzer
    {
        /// <summary>
        /// Символ комментария.
        /// </summary>
        private const char commentSymbol = '\'';

        /// <summary>
        /// Текущий обрабатываемый токен.
        /// </summary>
        private TokenBase currentToken;

        /// <summary>
        /// Тип предыдущего токена.
        /// </summary>
        private TokenType previousTokenType = TokenType.Undefined;

        /// <summary>
        /// Текущая строка. Отсчет начинается с 1.
        /// </summary>
        private int currentSymbolRow = 1;

        /// <summary>
        /// Номер первого символа, текущего токена. Отсчет начинается с 1.
        /// </summary>
        private int currentSymbolColumn = 1;

        /// <summary>
        /// Символы, относящиеся к бинарному токену.
        /// </summary>
        private char[] binaries = new char[] { '0', '1' };

        /// <summary>
        /// Символы, относящиеся к буквенному токену.
        /// </summary>
        private char[] literals = new char[] { 'a', 'b', 'c', 'd' };

        /// <summary>
        /// Обрабатываемый текст.
        /// </summary>
        private string text = string.Empty;

        /// <summary>
        /// Проверить текст на корректность.
        /// </summary>
        /// <param name="text">Текст.</param>
        /// <exception cref="FormatException">Выбрасывается, если слова одного типа не были разделены хотя бы одним пробелом.</exception>
        public void ValidateText(string text)
        {
            this.Initialize();
            this.text = string.Join(string.Empty, text.Select(symbol => char.ToLower(symbol)));
            while (true)
            {
                this.SkipBreaklines();
                var whitespacesSkipped = this.SkipWhitespaces();
                this.currentToken = this.ReadNextToken();
                if (this.currentToken == null)
                    return;
                if (this.currentToken.Type == TokenType.Undefined)
                    throw new InvalidTokenException("Некорретный токен.")
                    {
                        Row = this.currentSymbolRow,
                        Column = this.currentSymbolColumn,
                    };
                if (this.currentToken.Type == this.previousTokenType && !whitespacesSkipped)
                {
                    throw new InvalidTokenException("Токены одного типа должны разделяться хотя бы одним пробелом.")
                    {
                        Row = this.currentSymbolRow,
                        Column = this.currentSymbolColumn,
                    };
                }
                var incorrectSymbolIndex = this.currentToken.Validate();
                if (incorrectSymbolIndex != -1)
                    throw new InvalidTokenException("Токен записан неверно.")
                    {
                        Row = this.currentSymbolRow,
                        Column = this.currentSymbolColumn + incorrectSymbolIndex
                    };
                this.currentSymbolColumn += this.currentToken.Value.Length;
                this.previousTokenType = this.currentToken.Type;
            }
        }

        /// <summary>
        /// Сбросить анализатор до исходного состояния.
        /// </summary>
        private void Initialize()
        {
            this.currentToken = null;
            this.previousTokenType = TokenType.Undefined;
            this.currentSymbolRow = 1;
            this.currentSymbolColumn = 1;
            this.text = string.Empty;
        }

        /// <summary>
        /// Прочитать следующий токен.
        /// </summary>
        /// <returns>Токен.</returns>
        private TokenBase ReadNextToken()
        {
            if (string.IsNullOrWhiteSpace(this.text))
                return null;
            var value = string.Empty;
            var tokenType = this.RecognizeTokenTypeSymbol(this.text.FirstOrDefault());
            if (tokenType == TokenType.Undefined)
                return new UndefinedToken();

            foreach (var symbol in this.text)
            {
                if (symbol == '\r')
                {
                    this.SkipBreaklines();
                    this.text = string.Join(string.Empty, this.text.Skip(value.Length));
                    return this.GetTokenByTokenType(value, tokenType);
                }
                if (tokenType != TokenType.Comment && tokenType != this.RecognizeTokenTypeSymbol(symbol))
                {
                    this.text = string.Join(string.Empty, this.text.Skip(value.Length));
                    return this.GetTokenByTokenType(value, tokenType);
                }
                if (tokenType == TokenType.Binary && this.binaries.Contains(symbol))
                    value += symbol;
                else if (tokenType == TokenType.Literal && this.literals.Contains(symbol))
                    value += symbol;
                else if (tokenType == TokenType.Comment)
                    value += symbol;
            }
            this.text = string.Join(string.Empty, this.text.Skip(value.Length));
            return this.GetTokenByTokenType(value, tokenType);
        }

        /// <summary>
        /// Убрать пробелы и переносы строк перед словом.
        /// </summary>
        /// <returns>True, если был убран хотя бы один пробел или перенос, иначе false.</returns>
        private bool SkipWhitespaces()
        {
            var initialLength = this.text.Length;
            this.text = string.Join(string.Empty, this.text.SkipWhile(symbol => symbol == ' '));
            this.currentSymbolColumn += initialLength - this.text.Length;
            return this.text.Length < initialLength;
        }

        /// <summary>
        /// Убрать переносы строк перед словом.
        /// </summary>
        private void SkipBreaklines()
        {
            var initialLength = this.text.Length;
            this.text = string.Join(string.Empty, this.text.SkipWhile(symbol => symbol == '\r' || symbol == '\n'));
            this.currentSymbolRow += (initialLength - this.text.Length) / 2;
            this.currentSymbolColumn = this.text.Length < initialLength ? 1 : this.currentSymbolColumn;
        }

        /// <summary>
        /// Определить тип токена по первому символу текста.
        /// </summary>
        /// <param name="symbol">Символ.</param>
        /// <returns>Тип токена.</returns>
        private TokenType RecognizeTokenTypeSymbol(char symbol)
        {
            if (this.binaries.Contains(symbol))
                return TokenType.Binary;
            else if (this.literals.Contains(symbol))
                return TokenType.Literal;
            else if (symbol == commentSymbol)
                return TokenType.Comment;
            else
                return TokenType.Undefined;
        }

        /// <summary>
        /// Получить токен по типу токена.
        /// </summary>
        /// <param name="type">Тип токена.</param>
        /// <returns>Токен с заданным типом.</returns>
        /// <exception cref="NotSupportedException">Выбрасывается, если передан не поддерживаемый тип токена..</exception>
        private TokenBase GetTokenByTokenType(string value, TokenType type)
        {
            switch (type)
            {
                case TokenType.Binary:
                    return new BinaryToken(value, TokenType.Binary);
                case TokenType.Literal:
                    return new LiteralToken(value, TokenType.Literal);
                case TokenType.Comment:
                    return new CommentToken(value, TokenType.Comment);
                case TokenType.Undefined:
                    return new UndefinedToken();
                default:
                    throw new NotSupportedException($"Тип токена {type} не поддерживается.");
            }
        }
    }
}