﻿namespace Lox
{
    internal class Token
    {
        public TokenType Type { get; }
        public string Lexeme { get; }
        public object Literal { get; }
        public int Line { get; }

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            this.Type = type;
            this.Lexeme = lexeme;
            this.Literal = literal;
            this.Line = line;
        }

        public override string ToString()
        {
            //return type + " " + lexeme + " " + literal;
            return $"{this.Type} {this.Lexeme} {this.Literal}";
        }
    }
}