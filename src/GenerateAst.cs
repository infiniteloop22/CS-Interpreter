namespace Lox
{
    public class GenerateAst
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: generate_ast <output directory>");
                System.Environment.Exit(64);
            }
            string outputDir = args[0];

            List<string> exprTypes = new List<string>
            {
                "Binary   : Expr left, Token operator, Expr right",
                "Assign   : Token name, Expr value",
                "Grouping : Expr expression",
                "Literal  : object value",
                "Logical  : Expr left, Token operator, Expr right",
                "Unary    : Token operator, Expr right"
            };

            List<string> stmtTypes = new List<string>
            {
                "Block      : List<Stmt> statements",
                "Expression : Expr expression",
                "If         : Expr condition, Stmt thenBranch," + " Stmt elseBranch",
                "Print      : Expr expression",
                "Var        : Token name, Expr initializer",
                "While      : Expr condition, Stmt body"
            };

            DefineAst(outputDir, "Stmt", stmtTypes);

            DefineAst(outputDir, "Expr", exprTypes);
        }

        private static void DefineAst(string outputDir, string baseName, List<string> types)
        {
            string path = outputDir + "/" + baseName + ".cs";
            StreamWriter writer = new StreamWriter(path);

            writer.WriteLine("using com.craftinginterpreters.lox;");
            writer.WriteLine();
            writer.WriteLine("using System.Collections;");
            writer.WriteLine();
            writer.WriteLine("abstract class " + baseName + " {");

            DefineVisitor(writer, baseName, types);

            foreach (string type in types)
            {
                string className = type.Split(":")[0].Trim();
                string fields = type.Split(":")[1].Trim();
                DefineType(writer, baseName, className, fields);
            }

            writer.WriteLine();
            writer.WriteLine("  abstract <R> R accept(Visitor<R> visitor);");

            writer.WriteLine("}");
            writer.Close();
        }

        private static void DefineVisitor(StreamWriter writer, string baseName, List<string> types)
        {
            writer.WriteLine("  interface Visitor<R> {");

            foreach (string type in types)
            {
                string typeName = type.Split(":")[0].Trim();
                writer.WriteLine("    R visit" + typeName + baseName + "(" + typeName + " " + baseName.ToLower() + ");");
            }

            writer.WriteLine("  }");
        }

        private static void DefineType(StreamWriter writer, string baseName, string className, string fieldList)
        {
            writer.WriteLine(" static class " + className + " extends " + baseName + " {");

            writer.WriteLine("    " + className + "(" + fieldList + ") {");

            string[] fields = fieldList.Split(", ");
            foreach (string field in fields)
            {
                string name = field.Split(" ")[1];
                writer.WriteLine("      this." + name + " = " + name + ";");

                writer.WriteLine("    }");
            }

            writer.WriteLine();
            foreach (string field in fields)
            {
                writer.WriteLine("    final " + field + ";");
            }

            writer.WriteLine("  }");

            writer.WriteLine();
            writer.WriteLine("    @Override");
            writer.WriteLine("    <R> R accept(Visitor<R> visitor) {");
            writer.WriteLine("      return visitor.visit" + className + baseName + "(this);");
            writer.WriteLine("    }");
        }
    }
}