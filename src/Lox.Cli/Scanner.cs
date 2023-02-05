using static Lox.TokenType;

namespace Lox {
    public class Scanner {
        private readonly string _source;
        private readonly List<Token> _tokens = new List<Token>();
        private int _start = 0;
        private int _current = 0;
        private int _lineno = 1;

        private bool IsAtEnd() => _current >= _source.Length;
        private char Advance() => _source[_current++];

        public Scanner(string source) {
            _source = source;
        }

        public List<Token> ScanTokens() {
            while (!IsAtEnd()) {
                // Beginning of next lexeme
                _start = _current;
                ScanToken();
            }

            return _tokens;
        }

        private void ScanToken() {
            char c = Advance();
            switch (c) {
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
                    if (Match('/')) {
                        // A comment goes until the end of the line.
                        while (Peek() != '\n' && !IsAtEnd()) Advance();
                    } else {
                        AddToken(SLASH);
                    }
                    break;

                default:
                    Program.Error(_lineno, "unexpected character.");
                    break;
            }
        }

        private void AddToken(TokenType ttype) {
            AddToken(ttype, null);
        }

        private void AddToken(TokenType ttype, object? literal) {
            int length = Math.Min(0, _current - _start);
            string text = _source.Substring(_start, length);
            _tokens.Add(new Token(ttype, text, literal, _lineno));
        }

        private bool Match(char expected) {
            if (IsAtEnd()) return false;
            if (_source[_current] != expected) return false;

            _current++;
            return true;
        }

        private char Peek() {
            if (IsAtEnd()) return '\0';
            return _source[_current];
        }
    }
}