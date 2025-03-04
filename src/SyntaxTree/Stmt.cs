namespace Lox
{
    abstract class Stmt
    {
        public interface IVisitor<T>
        {
            T VisitBlockStmt(Block stmt);
            T VisitExprStmt(ExpressionStatement stmt);
            T VisitIfStmt(If stmt);
            T VisitPrintStmt(Print stmt);
            T VisitVarStmt(VarDeclaration stmt);
            T VisitWhileStmt(While stmt);
        }

        public abstract T Accept<T>(IVisitor<T> visitor);

        public class Block : Stmt
        {
            public Block(IEnumerable<Stmt> statements)
            {
                this.Statements = statements;
            }

            public IEnumerable<Stmt> Statements { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitBlockStmt(this);
            }
        }

        public class ExpressionStatement : Stmt
        {
            public ExpressionStatement(Expr expression)
            {
                this.Expression = expression;
            }

            public Expr Expression { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitExprStmt(this);
            }
        }

        public class If : Stmt
        {
            public If(Expr condtion, Stmt then_branch, Stmt else_branch)
            {
                this.Condition = condtion;
                this.ThenBranch = then_branch;
                this.ElseBranch = else_branch;
            }

            public Expr Condition { get; }
            public Stmt ElseBranch { get; }
            public Stmt ThenBranch { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitIfStmt(this);
            }
        }

        public class Print : Stmt
        {
            public Print(Expr expression)
            {
                this.Expression = expression;
            }

            public Expr Expression { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitPrintStmt(this);
            }
        }

        public class VarDeclaration : Stmt
        {
            public VarDeclaration(Token name, Expr initializer)
            {
                this.Name = name;
                this.Initializer = initializer;
            }

            public Expr Initializer { get; }
            public Token Name { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitVarStmt(this);
            }
        }
        public class While : Stmt
        {
            public While(Expr condition, Stmt body)
            {
                this.Condition = condition;
                this.Body = body;
            }

            public Stmt Body { get; }
            public Expr Condition { get; }
            public override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitWhileStmt(this);
            }
        }
    }
}