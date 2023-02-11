using Lox;

namespace Lox.Cli.Test
{

    public class ScannerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_SingleCharacter()
        {
            var scanner = new Scanner("(){},.-+;*");
            var tokens = scanner.ScanTokens();

            Assert.That(tokens.Count, Is.EqualTo(10));
            Assert.That(tokens[0].Type, Is.EqualTo(TokenType.LEFT_PAREN));
            Assert.That(tokens[1].Type, Is.EqualTo(TokenType.RIGHT_PAREN));
            Assert.That(tokens[2].Type, Is.EqualTo(TokenType.LEFT_BRACE));
            Assert.That(tokens[3].Type, Is.EqualTo(TokenType.RIGHT_BRACE));
            Assert.That(tokens[4].Type, Is.EqualTo(TokenType.COMMA));
            Assert.That(tokens[5].Type, Is.EqualTo(TokenType.DOT));
            Assert.That(tokens[6].Type, Is.EqualTo(TokenType.MINUS));
            Assert.That(tokens[7].Type, Is.EqualTo(TokenType.PLUS));
            Assert.That(tokens[8].Type, Is.EqualTo(TokenType.SEMICOLON));
            Assert.That(tokens[9].Type, Is.EqualTo(TokenType.STAR));
        }

        [Test]
        public void Test_CommentBlock()
        {
            var scanner = new Scanner("before /* foobar */ after");
            var tokens = scanner.ScanTokens();

            Assert.That(tokens.Count, Is.EqualTo(2));
            Assert.That(tokens[0].Type, Is.EqualTo(TokenType.IDENTIFIER));
            Assert.That(tokens[1].Type, Is.EqualTo(TokenType.IDENTIFIER));
        }
    }
}