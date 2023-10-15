using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LexicalAnalyzer;

namespace LexicalAnalyzerTests
{
    [TestFixture]
    public class CommentTokenTests
    {
        [Test]
        [TestCase("'")]
        [TestCase("'\n")]
        [TestCase("'\0")]
        [TestCase("'4''h&^@")]
        [TestCase("'4''h&^@\0")]
        [TestCase("'4''h&^@\n")]
        public void ValidateCorrectTokenTest(string value)
        {
            var commentToken = new CommentToken(value, TokenType.Comment);
            var incorrectSymbolIndex = commentToken.Validate();
            var expectedIndex = -1;
            Assert.That(incorrectSymbolIndex, Is.EqualTo(expectedIndex));
        }

        [Test]
        [TestCase("/", 0)]
        public void ValidateIncorrectTokenTest(string value, int expectedIndex)
        {
            var commentToken = new CommentToken(value, TokenType.Comment);
            var incorrectSymbolIndex = commentToken.Validate();
            Assert.That(incorrectSymbolIndex, Is.EqualTo(expectedIndex));
        }
    }
}
