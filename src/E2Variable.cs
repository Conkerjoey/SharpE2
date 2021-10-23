/**
* FileName: E2Variable.cs
* Author : Joey Courcelles
* Created On : 23/10/2021
* Last Modified On : 23/10/2021
* Copy Rights : Joey Courcelles
* Description : Class defining a variable in an expression.
*/

namespace ExpressionEngine
{
    public class E2Variable : E2Element
    {
        private string varName;

        public E2Variable(string name)
        {
            this.varName = name;
        }

        public override E2Element Evaluate()
        {
            return this;
        }

        public override string ToString()
        {
            return varName;
        }

        public override E2Element Negate()
        {
            return new E2Unary(this, UnaryOperator.MINUS);
        }
    }
}
