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

                default:
                    Program.Error(_lineno, "unexpected character.");
                    break;
            }
        }

        private void AddToken(TokenType ttype) {
            AddToken(ttype, null);
        }

        private void AddToken(TokenType ttype, object? literal) {
            string text = _source.Substring(_start, _current);
            _tokens.Add(new Token(ttype, text, literal, _lineno));
        }
    }
}