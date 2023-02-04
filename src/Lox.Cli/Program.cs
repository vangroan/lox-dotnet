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
            RunPrompt();
        }

        Console.WriteLine("exit");
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

        foreach (var token in tokens) {
            Console.WriteLine(token);
        }
    }

    public static void Error(int lineno, string message) {
        Report(lineno, "", message);
    }

    static void Report(int lineno, string where, string message) {
        Console.Error.WriteLine("[line {0}] Error{1}: {2}", lineno, where, message);
        HadError = true;
    }
}