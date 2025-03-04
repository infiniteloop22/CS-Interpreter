using System.Text;

namespace Lox
{
    internal class Lox
    {
        private static readonly Interpreter _interpreter = new Interpreter();
        static bool hadError = false;
        static bool hadRuntimeError = false;
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: C#lox [script]"); // if the user provides more than one command-line argument, an error message is shown
                System.Environment.Exit(64); // indicates a usage error
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]); // assumes its a file path and calls RunFile to execute the file path
            }
            else
            {
                RunPrompt(); // if no args provided
            }
        }

        private static void RunFile(string path)
        {
            string source = File.ReadAllText(Path.GetFullPath(path), Encoding.UTF8); // File.ReadAllText internally reads bytes and decodes them into a string in a single step. UTF-8 is more consistent across systems.
            Run(source);

            if (hadError)
            {
                System.Environment.Exit(65); // Indicate an error in the exit code
            }

            if (hadRuntimeError)
            {
                System.Environment.Exit(70); // Indicate an error in the exit code
            }
        }

        private static void RunPrompt()
        {
            while (true) // infinite loop to keep prompting user for input until they exit
            {
                Console.WriteLine("> ");
                string line = Console.ReadLine(); // handles input buffering internally
                if (line == null) break; // Exit on EOF (end of file) ended input, loop breaks and REPL exits
                Run(line);
                hadError = false; // reset flag in the interactive loop
            }
        }

        private static void Run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.ScanTokens();
            Parser parser = new Parser(tokens);
            List<Stmt> statements = parser.Parse();
            //Expr expression = parser.Parse();

            if (hadError)
            {
                return;
            }

            _interpreter.Intepret(statements);
            //_interpreter.Interpret(expression);

            //Console.WriteLine(new AstPrinter().Print(expression));

            /*foreach (Token token in tokens)
             {
                 Console.WriteLine(token);
             }*/
        }

        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        private static void Report(int line, string where, string message)
        {
            Console.Error.WriteLine("[line " + line + "] Error" + where + ": " + message);
            hadError = true;
        }

        public static void Error(Token token, string message)
        {
            if (token.Type == TokenType.EOF)
            {
                Report(token.Line, " at end", message);
            }
            else
            {
                Report(token.Line, " at '" + token.Lexeme + "'", message);
            }
        }

        public static void RuntimeError(RuntimeError error)
        {
            Console.Error.WriteLine(error.Message + "\n[line " + error.Token.Line + "]");
            hadRuntimeError = true;
        }
    }
}