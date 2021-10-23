/**
* FileName: E2Unary.cs
* Author : Joey Courcelles
* Created On : 23/10/2021
* Last Modified On : 23/10/2021
* Copy Rights : Joey Courcelles
* Description : Class defining a unary operation in an expression.
*/

namespace ExpressionEngine
{
    public class E2Unary : E2Element
    {
        private E2Element node;
        private UnaryOperator uop;

        public E2Unary(E2Element node, UnaryOperator uop)
        {
            this.node = node;
            this.uop = uop;
            this.isEvaluable = node.isEvaluable;
        }

        public override E2Element Evaluate()
        {
            node = node.Evaluate();
            if (node != null)
            {
                node = node.Negate();
            }
            return node;
        }

        public override string ToString()
        {
            return "-" + node.ToString();
        }

        public override E2Element Negate()
        {
            if (uop == UnaryOperator.MINUS)
            {
                return node;
            }
            else
            {
                return new E2Unary(this, UnaryOperator.MINUS);
            }
        }
    }
}
