/**
* FileName: E2Priority.cs
* Author : Joey Courcelles
* Created On : 23/10/2021
* Last Modified On : 23/10/2021
* Copy Rights : Joey Courcelles
* Description : This is a simple class defining a node priority (aka parenthesis)
*/

namespace ExpressionEngine
{
    public class E2Priority : E2Element
    {
        private E2Element node;

        public E2Priority(E2Element node)
        {
            this.node = node;
        }

        public override E2Element Evaluate()
        {
            node = node.Evaluate();
            return node;
        }

        public override string ToString()
        {
            if (node is E2Number)
                return node.ToString();
            return "(" + node.ToString() + ")";
        }

        public override E2Element Negate()
        {
            return new E2Unary(this, UnaryOperator.MINUS);
        }
    }
}
