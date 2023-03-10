
using System.Globalization;

namespace Lox {
    public enum TokenType : int {
        // Single-character tokens.
        LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE, RIGHT_BRACE,
        COMMA, DOT, MINUS, PLUS, SEMICOLON, SLASH, STAR,

        // One or two character tokens.
        BANG, BANG_EQUAL,
        EQUAL, EQUAL_EQUAL,
        GREATER, GREATER_EQUAL,
        LESS, LESS_EQUAL,

        // Literals.
        IDENTIFIER, STRING, NUMBER,

        // Keywords.
        AND, CLASS, ELSE, FALSE, FUN, FOR, IF, NIL, OR,
        PRINT, RETURN, SUPER, THIS, TRUE, VAR, WHILE,

        EOF,
    }

    public class Token {
        public readonly TokenType Type;
        public readonly string Lexeme;
        public readonly object? Literal;
        public readonly int LineNo;

        public Token(TokenType type, string lexeme, object? literal, int lineno) {
            Type = type;
            Lexeme = lexeme;
            Literal = literal;
            LineNo = lineno;
        }

        // Ensure floats are formatted with dot separator
        private string LiteralRepr => String.Format(CultureInfo.InvariantCulture, "{0}", Literal);
        public override string ToString() => $"{Type} {Lexeme} {LiteralRepr}";
    }
}
