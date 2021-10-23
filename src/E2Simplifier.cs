/**
* FileName: E2Simplifier.cs
* Author : Joey Courcelles
* Created On : 23/10/2021
* Last Modified On : 23/10/2021
* Copy Rights : Joey Courcelles
* Description : Primary class of the library. This class evaluates the expression or simplify it.
*               It creates the node tree data type of the expression.
*/

using System;
using System.Text.RegularExpressions;

namespace SharpE2
{
    public class E2Simplifier
    {
        private int charIndex;
        private string expression;
        private string originalExpression;

        public E2Simplifier(string expression)
        {
            this.originalExpression = expression.Trim() + (expression.EndsWith(";") ? "" : ";");
            this.expression = originalExpression;
        }
        public E2Simplifier()
        {

        }


        public string SetVariable(string variableName, double value)
        {
            return Regex.Replace(this.expression, @"\b" + variableName + @"\b", "" + value);
        }

        public string Simplify()
        {
            return Simplify(E2Number.Format.DECIMAL);
        }

        public string Simplify(string expression)
        {
            return Simplify(expression, E2Number.Format.DECIMAL);
        }

        public string Simplify(E2Number.Format outputFormat)
        {
            return Simplify(this.expression, outputFormat);
        }

        public string Simplify(string expression, E2Number.Format outputFormat)
        {
            this.originalExpression = expression.Trim() + (expression.EndsWith(";") ? "" : ";");
            this.expression = originalExpression;
            this.charIndex = 0;
            E2Element expressionElement = ParseSummand();
            expressionElement = expressionElement.Evaluate();
            if (expressionElement is E2Number)
            {
                return ((E2Number)expressionElement).GetValue(outputFormat);
            }
            else
            {
                string exp = expressionElement.ToString();
                if (exp.StartsWith("(") && exp.EndsWith(")"))
                {
                    exp = exp.Substring(1, exp.Length - 2);
                }
                return exp;
            }
        }

        private E2Element ParseAtom()
        {
            if (expression[charIndex] == '-')
            {
                IncrementIndex();
                return new E2Unary(ParseAtom(), UnaryOperator.MINUS);
            }
            else if (Char.IsDigit(expression[charIndex]) && (expression[charIndex + 1] == 'x' || expression[charIndex + 1] == 'X'))
            {
                string value = "";
                while (Char.IsLetterOrDigit(expression[charIndex]))
                {
                    value += expression[charIndex];
                    charIndex++;
                }
                NextToken();
                return new E2Number(Convert.ToInt64(value.Substring(2, value.Length - 2), 16), E2Number.Format.HEXADECIMAL);
            }
            else if (Char.IsDigit(expression[charIndex]))
            {
                string value = "";
                while (Char.IsDigit(expression[charIndex]) || expression[charIndex] == '.')
                {
                    value += expression[charIndex];
                    charIndex++;
                }
                NextToken();
                return new E2Number(double.Parse(value), E2Number.Format.DECIMAL);
            }
            else if (Char.IsLetter(expression[charIndex]) || expression[charIndex] == '_')
            {
                string name = "";
                while (Char.IsLetterOrDigit(expression[charIndex]) || expression[charIndex] == '_')
                {
                    name += expression[charIndex];
                    charIndex++;
                }
                NextToken();
                return new E2Variable(name);
            }
            return null;
        }

        private E2Element ParseParenthesis()
        {
            if (expression[charIndex] == '(')
            {
                IncrementIndex();
                E2Element node = new E2Priority(ParseSummand());
                if (expression[charIndex] == ')')
                {
                    IncrementIndex();
                }
                else
                {
                    // Missing closing parenthesis...
                }
                return node;
            }
            else
            {
                return ParseAtom();
            }
        }

        private E2Element ParseExponent()
        {
            E2Element lhs = ParseParenthesis();
            if (expression[charIndex] == '^')
            {
                IncrementIndex();
                E2Element node = new E2Binary(lhs, ParseParenthesis(), Operator.EXPONENT);
                return node;
            }
            else
            {
                return lhs;
            }
        }

        private E2Element ParseFactor()
        {
            E2Element lhs = ParseExponent();
            while (true)
            {
                Operator op = Operator.NOP;

                if (expression[charIndex] == '*')
                {
                    op = Operator.MULTIPLICATION;
                    IncrementIndex();
                }
                else if (expression[charIndex] == '/')
                {
                    op = Operator.DIVISION;
                    IncrementIndex();
                }
                else if (expression[charIndex] == '%')
                {
                    op = Operator.MODULO;
                    IncrementIndex();
                }

                if (op == Operator.NOP)
                {
                    return lhs;
                }

                lhs = new E2Binary(lhs, ParseExponent(), op);
            }
        }
        private E2Element ParseSummand()
        {
            E2Element lhs = ParseFactor();
            while (true)
            {
                Operator op = Operator.NOP;

                if (expression[charIndex] == '+')
                {
                    op = Operator.ADDITION;
                    IncrementIndex();
                }
                else if (expression[charIndex] == '-')
                {
                    op = Operator.SUBTRACTION;
                    IncrementIndex();
                }

                if (op == Operator.NOP)
                {
                    return lhs;
                }
                if (op == Operator.SUBTRACTION)
                {
                    lhs = new E2Binary(lhs, new E2Unary(ParseFactor(), UnaryOperator.MINUS), Operator.ADDITION);
                }
                else
                {
                    lhs = new E2Binary(lhs, ParseFactor(), Operator.ADDITION);
                }
            }
        }

        private void IncrementIndex()
        {
            charIndex++;
            NextToken();
        }

        private void NextToken()
        {
            while (expression[charIndex] == ' ')
            {
                charIndex++;
            }
        }
    }
}
