using static Lox.TokenType;

namespace Lox
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _current = 0;


        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public Expr? Parse()
        {
            try
            {
                return Expression();
            }
            catch (ParseError)
            {
                return null;
            }
        }

        private bool MatchAny(TokenType first, TokenType second)
        {
            if (Check(first) || Check(second))
            {
                Advance();
                return true;
            }

            return false;
        }

        private bool MatchAny(params TokenType[] ttypes)
        {
            foreach (var ttype in ttypes)
            {
                if (Check(ttype))
                {
                    Advance();
                    return true;
                }
            }

            return false;
        }

        private bool Check(TokenType ttype)
        {
            if (IsAtEnd()) return false;
            return Peek().Type == ttype;
        }

        private Token Advance()
        {
            if (!IsAtEnd()) _current++;
            return Previous();
        }

        private Token Consume(TokenType ttype, string message)
        {
            if (Check(ttype)) return Advance();
            throw Error(Peek(), message);
        }

        private bool IsAtEnd() => Peek().Type == EOF;
        private Token Peek() => _tokens[_current];
        private Token Previous() => _tokens[_current - 1];

        private ParseError Error(Token token, string message)
        {
            Program.Error(token, message);
            return new ParseError();
        }

        private Expr Expression()
        {
            return Equality();
        }

        private Expr Equality()
        {
            Expr expr = Comparison();

            while (MatchAny(BANG_EQUAL, EQUAL_EQUAL))
            {
                Token op = Previous();
                Expr right = Comparison();
                expr = new Expr.Binary(op, left: expr, right: right);
            }

            return expr;
        }

        private Expr Comparison()
        {
            Expr expr = Term();

            while (MatchAny(GREATER, GREATER_EQUAL, LESS, LESS_EQUAL))
            {
                Token op = Previous();
                Expr right = Term();
                expr = new Expr.Binary(op, left: expr, right: right);
            }

            return expr;
        }

        private Expr Term()
        {
            Expr expr = Factor();

            while (MatchAny(MINUS, PLUS))
            {
                Token op = Previous();
                Expr right = Factor();
                expr = new Expr.Binary(op, left: expr, right: right);
            }

            return expr;
        }

        private Expr Factor()
        {
            Expr expr = Unary();

            while (MatchAny(SLASH, STAR))
            {
                Token op = Previous();
                Expr right = Unary();
                expr = new Expr.Binary(op, left: expr, right: right);
            }

            return expr;
        }

        private Expr Unary()
        {
            if (MatchAny(BANG, MINUS))
            {
                Token op = Previous();
                Expr right = Unary();
                return new Expr.Unary(op, right);
            }

            return Primary();
        }

        private Expr Primary()
        {
            if (MatchAny(FALSE)) return new Expr.Literal(false);
            if (MatchAny(TRUE)) return new Expr.Literal(true);
            if (MatchAny(NIL)) return new Expr.Literal(null);

            if (MatchAny(NUMBER, STRING))
            {
                return new Expr.Literal(Previous().Literal);
            }

            if (MatchAny(LEFT_PAREN))
            {
                Expr expr = Expression();
                Consume(RIGHT_PAREN, "Expect ')' after expression.");
                return new Expr.Grouping(expr);
            }

            throw Error(Peek(), "Expect expression.");
        }

        internal class ParseError : Exception { }
    }
}

