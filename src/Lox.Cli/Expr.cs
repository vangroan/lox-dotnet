
namespace Lox
{

    public abstract class Expr
    {
        public abstract R Accept<R>(Visitor<R> visitor);

        public class Binary : Expr
        {
            public readonly Token Operator;
            public readonly Expr Left;
            public readonly Expr Right;

            public Binary(Token op, Expr left, Expr right)
            {
                Operator = op;
                Left = left;
                Right = right;
            }

            public override R Accept<R>(Visitor<R> visitor) => visitor.VisitBinary(this);
        }

        public class Grouping : Expr
        {
            public readonly Expr Expression;

            public Grouping(Expr expression)
            {
                Expression = expression;
            }

            public override R Accept<R>(Visitor<R> visitor) => visitor.VisitGrouping(this);
        }

        public class Literal : Expr
        {
            public readonly object Value;

            public Literal(object value)
            {
                Value = value;
            }

            public override R Accept<R>(Visitor<R> visitor) => visitor.VisitLiteral(this);
        }

        public class Unary : Expr
        {
            public readonly Token Operator;
            public readonly Expr Right;

            public Unary(Token op, Expr right)
            {
                Operator = op;
                Right = right;
            }

            public override R Accept<R>(Visitor<R> visitor) => visitor.VisitUnary(this);
        }
    }
}
