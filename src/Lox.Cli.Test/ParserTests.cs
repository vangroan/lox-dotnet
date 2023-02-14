namespace Lox.Cli.Test
{
    public class ParserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Expr()
        {
            var scanner = new Scanner("-123 * (456 + 789)");
            var tokens = scanner.ScanTokens();
            var parser = new Parser(tokens);
            var expression = parser.Parse();

            Assert.NotNull(expression);
        }
    }
}
