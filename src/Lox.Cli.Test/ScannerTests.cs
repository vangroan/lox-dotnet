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

            Assert.That(10, Is.EqualTo(tokens.Count));
            Assert.That(TokenType.LEFT_PAREN, Is.EqualTo(tokens[0].Type));
            Assert.That(TokenType.RIGHT_PAREN, Is.EqualTo(tokens[1].Type));
            Assert.That(TokenType.LEFT_BRACE, Is.EqualTo(tokens[2].Type));
            Assert.That(TokenType.RIGHT_BRACE, Is.EqualTo(tokens[3].Type));
            Assert.That(TokenType.COMMA, Is.EqualTo(tokens[4].Type));
            Assert.That(TokenType.DOT, Is.EqualTo(tokens[5].Type));
            Assert.That(TokenType.MINUS, Is.EqualTo(tokens[6].Type));
            Assert.That(TokenType.PLUS, Is.EqualTo(tokens[7].Type));
            Assert.That(TokenType.SEMICOLON, Is.EqualTo(tokens[8].Type));
            Assert.That(TokenType.STAR, Is.EqualTo(tokens[9].Type));
        }
    }
}