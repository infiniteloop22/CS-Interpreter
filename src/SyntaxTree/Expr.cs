namespace Lox
{
    abstract class Expr
    {
        public interface IVisitor<T>
        {
            T VisitAssignExpr(Assign expr);
            T VisitBinaryExpr(Binary expr);
            T VisitGroupingExpr(Grouping expr);
            T VisitLiteralExpr(Literal expr);
            T VisitLogicalExpr(Logical expr);
            T VisitUnaryExpr(Unary expr);
            T VisitVariableExpr(Variable expr);
        }

        public abstract T Accept<T>(IVisitor<T> visitor);

        public class Assign : Expr
        {
            public Expr value;
            public Assign(Token name, Expr value)
            {
                this.Name = name;
                this.value = value;
            }

            public Token Name { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitAssignExpr(this);
            }
        }

        public class Binary : Expr
        {
            public Binary(Expr left, Token op, Expr right)
            {
                this.Left = left;
                this.Operator = op;
                this.Right = right;
            }

            public Expr Left { get; }
            public Token Operator { get; }
            public Expr Right { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitBinaryExpr(this);
            }
        }

        public class Grouping : Expr
        {
            public Grouping(Expr expression)
            {
                this.Expression = expression;
            }

            public Expr Expression { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitGroupingExpr(this);
            }
        }

        public class Literal : Expr
        {
            public Literal(object value)
            {
                this.Value = value;
            }

            public object Value { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitLiteralExpr(this);
            }
        }

        public class Logical : Expr
        {
            public Logical(Expr left, Token op, Expr right)
            {
                this.Left = left;
                this.Operator = op;
                this.Right = right;
            }

            public Expr Left { get; }
            public Token Operator { get; }
            public Expr Right { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitLogicalExpr(this);
            }
        }

        public class Unary : Expr
        {
            public Unary(Token op, Expr right)
            {
                this.Operator = op;
                this.Right = right;
            }

            public Token Operator { get; }
            public Expr Right { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitUnaryExpr(this);
            }
        }

        public class Variable : Expr
        {
            public Variable(Token name)
            {
                this.Name = name;
            }

            public Token Name { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitVariableExpr(this);
            }
        }
    }
}