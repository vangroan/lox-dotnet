using System.Globalization;
using static Lox.TokenType;

namespace Lox
{
    public class Scanner
    {
        private readonly string _source;
        private readonly List<Token> _tokens = new List<Token>();
        private int _start = 0;
        private int _current = 0;
        private int _lineno = 1;

        private readonly static Dictionary<string, TokenType> s_keywords = new Dictionary<string, TokenType>() {
            {"and",    AND},
            {"class",  CLASS},
            {"else",   ELSE},
            {"false",  FALSE},
            {"for",    FOR},
            {"fun",    FUN},
            {"if",     IF},
            {"nil",    NIL},
            {"or",     OR},
            {"print",  PRINT},
            {"return", RETURN},
            {"super",  SUPER},
            {"this",   THIS},
            {"true",   TRUE},
            {"var",    VAR},
            {"while",  WHILE},
        };

        private bool IsAtEnd() => _current >= _source.Length;
        private bool IsDigit(char c) => c >= '0' && c <= '9';
        private bool IsAlpha(char c) => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
        private bool IsAlphaNumeric(char c) => IsAlpha(c) || IsDigit(c);
        private char Advance() => _source[_current++];

        public Scanner(string source)
        {
            _source = source;
        }

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                // Beginning of next lexeme
                _start = _current;
                ScanToken();
            }

            return _tokens;
        }

        private void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(': AddToken(LEFT_PAREN); break;
                case ')': AddToken(RIGHT_PAREN); break;
                case '{': AddToken(LEFT_BRACE); break;
                case '}': AddToken(RIGHT_BRACE); break;
                case ',': AddToken(COMMA); break;
                case '.': AddToken(DOT); break;
                case '-': AddToken(MINUS); break;
                case '+': AddToken(PLUS); break;
                case ';': AddToken(SEMICOLON); break;
                case '*': AddToken(STAR); break;

                case '!': AddToken(Match('=') ? BANG_EQUAL : BANG); break;
                case '=': AddToken(Match('=') ? EQUAL_EQUAL : EQUAL); break;
                case '<': AddToken(Match('=') ? LESS_EQUAL : LESS); break;
                case '>': AddToken(Match('=') ? GREATER_EQUAL : GREATER); break;

                case '/':
                    if (Peek() == '/')
                    {
                        Advance();

                        // A comment goes until the end of the line.
                        while (Peek() != '\n' && !IsAtEnd()) Advance();
                    }
                    if (Peek() == '*')
                    {
                        Advance();
                        ScanComment();
                    }
                    else
                        AddToken(SLASH);
                    break;

                case ' ':
                case '\r':
                case '\t':
                    // Ignore whitespace.
                    break;

                case '\n':
                    _lineno++;
                    break;

                case '"': ScanString(); break;

                default:
                    if (IsDigit(c))
                        ScanNumber();
                    else if (IsAlpha(c))
                        ScanIdentifier();
                    else
                        Program.Error(_lineno, $"unexpected character: {c}");
                    // Program.Error(_lineno, "unexpected character.");
                    break;
            }
        }

        private void AddToken(TokenType ttype)
        {
            AddToken(ttype, null);
        }

        private void AddToken(TokenType ttype, object? literal)
        {
            int length = Math.Max(0, _current - _start);
            string text = _source.Substring(_start, length);
            _tokens.Add(new Token(ttype, text, literal, _lineno));
        }

        private bool Match(char expected)
        {
            if (IsAtEnd()) return false;
            if (_source[_current] != expected) return false;

            _current++;
            return true;
        }

        private char Peek()
        {
            if (IsAtEnd()) return '\0';
            return _source[_current];
        }

        private char PeekNext()
        {
            if (_current + 1 >= _source.Length) return '\0';
            return _source[_current + 1];
        }

        private void ScanComment()
        {
            while (Peek() != '*' && PeekNext() != '/' && !IsAtEnd())
            {
                if (Peek() == '\n') _lineno++;
                Advance();
            }

            if (IsAtEnd())
            {
                Program.Error(_lineno, "Unterminated comment block,");
                return;
            }

            // consume closing '*/'
            Advance();
            Advance();
        }

        private void ScanString()
        {
            while (Peek() != '"' && !IsAtEnd())
            {
                if (Peek() == '\n') _lineno++;
                Advance();
            }

            if (IsAtEnd())
            {
                Program.Error(_lineno, "Unterminated string.");
                return;
            }

            // The closing ".
            Advance();

            // Trim the surrounding quotes.
            int length = _current - _start - 2;  // compensate for start+1
            String value = _source.Substring(_start + 1, length);
            AddToken(STRING, value);
        }

        private void ScanNumber()
        {
            while (IsDigit(Peek())) Advance();

            // Look for a fractional part.
            if (Peek() == '.' && IsDigit(PeekNext()))
            {
                // Consume the "."
                Advance();

                // Consume fraction
                while (IsDigit(Peek())) Advance();
            }

            int length = _current - _start;
            // var style = NumberStyles.AllowDecimalPoint;
            AddToken(NUMBER, double.Parse(_source.Substring(_start, length), CultureInfo.InvariantCulture));
        }

        private void ScanIdentifier()
        {
            while (IsAlphaNumeric(Peek())) Advance();

            int length = _current - _start;
            String text = _source.Substring(_start, length);
            TokenType type = IDENTIFIER;
            // when try get fails, it overwrites the value with 0
            if (!s_keywords.TryGetValue(text, out type)) type = IDENTIFIER;

            AddToken(type);
        }
    }
}