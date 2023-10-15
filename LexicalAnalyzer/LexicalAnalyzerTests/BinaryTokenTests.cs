namespace LexicalAnalyzerTests
{
    /// <summary>
    /// Тесты бинарного токена.
    /// </summary>
    [TestFixture]
    public class BinaryTokenTests
    {
        /// <summary>
        /// Проверить корректные токены.
        /// </summary>
        /// <param name="value"></param>
        [Test]
        [TestCase("001")]
        [TestCase("000001010")]
        [TestCase("000000001010010")]
        [TestCase("000001")]
        [TestCase("001010")]
        [TestCase("000000001")]
        [TestCase("001010010")]
        public void ValidateCorrectTokenTest(string value)
        {
            var binaryToken = new BinaryToken(value, TokenType.Binary);
            var incorrectSymbolIndex = binaryToken.Validate();
            var expectedIndex = -1;
            Assert.That(incorrectSymbolIndex, Is.EqualTo(expectedIndex));
        }

        /// <summary>
        /// Проверить некорректные токены.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expectedSymbolIndex"></param>
        [Test]
        [TestCase("1", 0)]
        [TestCase("2", 0)]
        [TestCase("010", 1)]
        [TestCase("0001", 3)]
        [TestCase("00001", 4)]
        [TestCase("0000011", 6)]
        [TestCase("a", 0)]
        [TestCase("000%", 3)]
        public void ValidateIncorrectTokenTest(string value, int expectedSymbolIndex)
        {
            var binaryToken = new BinaryToken(value, TokenType.Binary);
            var incorrectSymbolIndex = binaryToken.Validate();
            Assert.That(incorrectSymbolIndex, Is.EqualTo(expectedSymbolIndex));
        }
    }
}