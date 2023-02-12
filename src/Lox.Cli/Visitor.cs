
namespace Lox
{
    public interface Visitor<R>
    {
        R VisitBinary(Expr.Binary expr);
        R VisitGrouping(Expr.Grouping expr);
        R VisitLiteral(Expr.Literal expr);
        R VisitUnary(Expr.Unary expr);
    }
}