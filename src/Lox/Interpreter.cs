namespace Lox
{
    internal class Interpreter : Expr.IVisitor<object>, Stmt.IVisitor<object>
    {
        private Environment _environment = new Environment();
        public void Intepret(List<Stmt> statements)
        {
            try
            {
                foreach (Stmt statement in statements)
                {
                    Execute(statement);
                }
            }
            catch (RuntimeError error)
            {
                Lox.RuntimeError(error);
            }
        }

        /*public void Interpret(Expr expression)
        {
            try
            {
                object value = Evaluate(expression);
                Console.WriteLine(Stringify(value));
            }
            catch (RuntimeError error)
            {
                Lox.RuntimeError(error);
            }
        }*/

        public object VisitLiteralExpr(Expr.Literal expr)
        {
            return expr.Value;
        }

        public object VisitLogicalExpr(Expr.Logical expr)
        {
            object left = Evaluate(expr.Left);

            if (expr.Operator.Type == TokenType.OR)
            {
                if (IsTruthy(left))
                {
                    return left;
                }
            } 
            else
            {
                if (!IsTruthy(left)) return left;
            }

            return Evaluate(expr.Right);
        }

        public object VisitUnaryExpr(Expr.Unary expr)
        {
            object right = Evaluate(expr.Right);

            switch (expr.Operator.Type)
            {
                case TokenType.BANG:
                    return !IsTruthy(right);
                case TokenType.MINUS:
                    CheckNumberOperand(expr.Operator, right);
                    return -(double)right;
            }

            return null;
        }

        public object VisitVariableExpr(Expr.Variable expr)
        {
            return _environment.Get(expr.Name);
        }

        private void CheckNumberOperand(Token @operator, object operand)
        {
            if (operand is double)
            {
                return;
            }

            throw new RuntimeError(@operator, "Operand must be a number.");
        }

        private void CheckNumberOperands(Token @operator, object left, object right)
        {
            if (left is double && right is double)
            {
                return;
            }

            throw new RuntimeError(@operator, "Operands must be numbers.");
        }

        private bool IsTruthy(object @object)
        {
            if (@object == null)
            {
                return false;
            }

            if (@object is bool booleanValue)
            {
                return booleanValue;
            }
            return true;
        }

        private bool IsEqual(object a, object b)
        {
            if (a == null && b == null)
            {
                return true;
            }

            if (a == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        private string Stringify(object @object)
        {
            if (@object == null)
            {
                return "nil";
            }

            if (@object is double)
            {
                string text = @object.ToString();

                if (text.EndsWith(".0"))
                {
                    text = text.Substring(0, text.Length - 2);
                }
                return text;
            }

            return @object.ToString();
        }

        public object VisitGroupingExpr(Expr.Grouping expr)
        {
            return Evaluate(expr.Expression);
        }

        private object Evaluate(Expr expr)
        {
            return expr.Accept(this);
        }

        private void Execute(Stmt stmt)
        {
            stmt.Accept(this);
        }

        public void ExecuteBlock(IEnumerable<Stmt> statements, Environment environment)
        {
            Environment previous = this._environment;
            try
            {
                this._environment = environment;

                foreach (Stmt statement in statements)
                {
                    Execute(statement);
                }
            }
            finally
            {
                this._environment = previous;
            }
        }

        public object VisitBlockStmt(Stmt.Block stmt)
        {
            ExecuteBlock(stmt.Statements, new Environment(_environment));
            return null;
        }

        public object VisitExprStmt(Stmt.ExpressionStatement stmt)
        {
            Evaluate(stmt.Expression);
            return null;
        }

        public object VisitIfStmt(Stmt.If stmt)
        {
            if (IsTruthy(Evaluate(stmt.Condition)))
            {
                Execute(stmt.ThenBranch);
            }
            else if (stmt.ElseBranch != null)
            {
                Execute(stmt.ElseBranch);
            }
            return null;
        }

        public object VisitPrintStmt(Stmt.Print stmt)
        {
            object value = Evaluate(stmt.Expression);
            Console.WriteLine(Stringify(value));
            return null;
        }

        public object VisitVarStmt(Stmt.VarDeclaration stmt)
        {
            object value = null;
            if (stmt.Initializer != null)
            {
                value = Evaluate(stmt.Initializer);
            }

            _environment.Define(stmt.Name.Lexeme, value);
            return null;
        }

        public object VisitWhileStmt(Stmt.While stmt)
        {
            while (IsTruthy(Evaluate(stmt.Condition)))
            {
                Execute(stmt.Body);
            }
            return null;
        }

        public object VisitAssignExpr(Expr.Assign expr)
        {
            object value = Evaluate(expr.value);
            _environment.Assign(expr.Name, value);
            return value;
        }

        public object VisitBinaryExpr(Expr.Binary expr)
        {
            object left = Evaluate(expr.Left);
            object right = Evaluate(expr.Right);

            switch (expr.Operator.Type)
            {
                case TokenType.BANG_EQUAL:
                    return !IsEqual(left, right);
                case TokenType.EQUAL_EQUAL:
                    return IsEqual(left, right);
                case TokenType.GREATER:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left > (double)right;
                case TokenType.GREATER_EQUAL:
                    return (double)left >= (double)right;
                case TokenType.LESS:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left < (double)right;
                case TokenType.LESS_EQUAL:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left <= (double)right;
                case TokenType.MINUS:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left - (double)right;
                case TokenType.PLUS:
                    if (left is double && right is double)
                    {
                        return (double)left + (double)right;
                    }

                    if (left is string && right is string)
                    {
                        return (string)left + (string)right;
                    }

                    throw new RuntimeError(expr.Operator, "Operands must be two numbers or two strings.");
                case TokenType.SLASH:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left / (double)right;
                case TokenType.STAR:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left * (double)right;
            }

            return null;
        }
    }
}