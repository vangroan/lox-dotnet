
using System.Text;
using System.Globalization;

namespace Lox
{
    public class AstPrinter : Visitor<string>
    {
        public string Print(Expr expr) => expr.Accept(this);

        private string Parenthesize(string name, params Expr[] exprs)
        {
            var builder = new StringBuilder();

            builder.Append("(").Append(name);
            foreach (Expr expr in exprs)
            {
                builder.Append(" ");
                builder.Append(expr.Accept(this));
            }
            builder.Append(")");

            return builder.ToString();
        }

        string Visitor<string>.VisitBinary(Expr.Binary expr) => Parenthesize(expr.Operator.Lexeme, expr.Left, expr.Right);
        string Visitor<string>.VisitGrouping(Expr.Grouping expr) => Parenthesize("group", expr.Expression);
        string Visitor<string>.VisitLiteral(Expr.Literal expr) => string.Format(CultureInfo.InvariantCulture, "{0}", expr.Value ?? "nil");
        string Visitor<string>.VisitUnary(Expr.Unary expr) => Parenthesize(expr.Operator.Lexeme, expr.Right);
    }
}