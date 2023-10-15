using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzerTests
{
    [TestFixture]
    public class LiteralTokenTests
    {
        [Test]
        [TestCase("ab")]
        [TestCase("ababcd")]
        [TestCase("abaaa")]
        [TestCase("abbbb")]
        [TestCase("abccc")]
        [TestCase("abddd")]
        public void ValidateCorrectTokenTest(string value)
        {
            var literalToken = new LiteralToken(value, TokenType.Literal);
            var incorrectSymbolIndex = literalToken.Validate();
            var expectedIndex = -1;
            Assert.That(incorrectSymbolIndex, Is.EqualTo(expectedIndex));
        }

        [Test]
        [TestCase("b", 0)]
        [TestCase("e", 0)]
        [TestCase("abe", 2)]
        [TestCase("ab%", 2)]
        public void ValidateIncorrectTokenTest(string value, int expectedIndex)
        {
            var literalToken = new LiteralToken(value, TokenType.Literal);
            var incorrectSymbolIndex = literalToken.Validate();
            Assert.That(incorrectSymbolIndex, Is.EqualTo(expectedIndex));
        }
    }
}
