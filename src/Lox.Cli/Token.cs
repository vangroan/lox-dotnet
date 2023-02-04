
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
        readonly TokenType Type;
        readonly string Lexeme;
        readonly object? Literal;
        readonly int LineNo;

        public Token(TokenType type, string lexeme, object? literal, int lineno) {
            Type = type;
            Lexeme = lexeme;
            Literal = literal;
            LineNo = lineno;
        }

        public override string ToString() => $"{Type} {Lexeme} {Literal}";
    }
}
