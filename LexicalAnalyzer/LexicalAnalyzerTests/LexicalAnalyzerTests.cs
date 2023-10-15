using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzerTests
{
    [TestFixture]
    public class LexicalAnalyzerTests
    {
        #region Корректный текст

        private const string singleLineText = @"000001010ab 001 ab000001 ab ab 001 001 'comment";

        private const string multiLineOnlyBinaryTokensText = @"001
000001
000000001010
001 001      000001 000001010
001010010010
001                 
001                                      ";

        private const string multiLineOnlyLiteralTokensText = @"ab
abbacd
ab abcd       abaaccdd abcd
ab                
ab                 ";

        private const string multiLineOnlyCommentTokensText = @"'comment
'anothercomment             
'commentwith'subcomment
''''''commentwithmany'                   ";

        private const string multiLineDifferentTokensText = @"001ab 000001 'comment
'comment
ab001 001 '1234567890!@#$%^&*()_+-=/\|;,.[]{}`~
ab ab 000001010abcd abba001010";

        #endregion

        #region Некорректный текст

        private const string multiLineBinaryTokensIncorrectText = @"001 000001 001010
000000000001                    001010010010
001         100
000001010      ";

        private const string multiLineLiteralTokensIncorrectText = @"ababcd ba
abaabbccdd";

        private const string multiLineCommentTokensIncorrectText = @"'comment!@#$%^&*()_+-={}[]:""`~\|/
//incorrectcomment
'commentagain";

        private const string multiLineDifferentTokensIncorrectText = @"001abcd abcd000001 'comment
'comment
'comment
bacdab
000001010";

        #endregion

        [Test]
        [TestCase(singleLineText)]
        [TestCase(multiLineOnlyBinaryTokensText)]
        [TestCase(multiLineOnlyLiteralTokensText)]
        [TestCase(multiLineOnlyCommentTokensText)]
        [TestCase(multiLineDifferentTokensText)]
        public void ValidateCorrectTextCasesTest(string text)
        {
            var lexicalAnalyzer = new LexicalAnalyzer.LexicalAnalyzer();
            Assert.DoesNotThrow(() => lexicalAnalyzer.ValidateText(text));
        }

        [Test]
        [TestCase(multiLineBinaryTokensIncorrectText)]
        [TestCase(multiLineLiteralTokensIncorrectText)]
        [TestCase(multiLineCommentTokensIncorrectText)]
        [TestCase(multiLineDifferentTokensIncorrectText)]
        public void ValidateIncorrectTextCasesTest(string text)
        {
            var lexicalAnalyzer = new LexicalAnalyzer.LexicalAnalyzer();
            Assert.Throws<InvalidTokenException>(() => lexicalAnalyzer.ValidateText(text));
        }
    }
}
