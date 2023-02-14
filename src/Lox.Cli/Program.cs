using System;
using System.IO;
using System.Text;
using Lox;

internal class Program
{
    static bool HadError = false;

    private static void Main(string[] args)
    {
        if (args.Length > 1)
        {
            Console.WriteLine("Usage: lox [script]");
            Environment.Exit((int)ExitCode.Usage);
        }
        else if (args.Length == 1)
        {
            RunFile(args[0]);
        }
        else
        {
            PrintAst();
            RunPrompt();
        }

        Console.WriteLine("exit");
    }

    private static void PrintAst()
    {
        Expr expression = new Expr.Binary(
            new Token(TokenType.STAR, "*", null, 1),
            new Expr.Unary(
                new Token(TokenType.MINUS, "-", null, 1),
                new Expr.Literal(123)
            ),
            new Expr.Grouping(new Expr.Binary(
                new Token(TokenType.PLUS, "+", null, 1),
                new Expr.Literal(45.67),
                new Expr.Literal(98.76)
            ))
        );

        Console.WriteLine(new AstPrinter().Print(expression));
    }

    private static void RunFile(string path)
    {
        string source = File.ReadAllText(path, Encoding.UTF8);
        Run(source);

        if (HadError) Environment.Exit((int)ExitCode.DataError);
    }

    private static void RunPrompt()
    {
        for (;;)
        {
            Console.Write("> ");
            string? line = Console.ReadLine();
            if (String.IsNullOrEmpty(line))
            {
                break;
            }

            Run(line);

            // Reset error state so session can continue.
            HadError = false;
        }
    }

    private static void Run(string source)
    {
        var scanner = new Scanner(source);
        var tokens = scanner.ScanTokens();
        var parser = new Parser(tokens);
        var expression = parser.Parse();

        if (HadError || expression == null) return;

        Console.WriteLine(new AstPrinter().Print(expression));
    }

    public static void Error(int lineno, string message)
    {
        Report(lineno, "", message);
    }

    public static void Error(Token token, string message)
    {
        if (token.Type == TokenType.EOF)
            Report(token.LineNo, " at end", message);
        else
            Report(token.LineNo, $" at '{token.Lexeme}'", message);
    }

    static void Report(int lineno, string where, string message)
    {
        Console.Error.WriteLine("[line {0}] Error{1}: {2}", lineno, where, message);
        HadError = true;
    }
}