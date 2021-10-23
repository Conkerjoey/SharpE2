/**
* FileName: E2Element.cs
* Author : Joey Courcelles
* Created On : 23/10/2021
* Last Modified On : 23/10/2021
* Copy Rights : Joey Courcelles
* Description : Abstract class of the main node type.
*/

namespace SharpE2
{
    public enum Operator
    {
        NOP,
        ADDITION,
        SUBTRACTION,
        MULTIPLICATION,
        DIVISION,
        EXPONENT,
        MODULO,
    }

    public enum UnaryOperator
    {
        NOP,
        MINUS,
    }

    public abstract class E2Element
    {
        public int priority = -1;
        public bool isEvaluable = false;
        public abstract E2Element Evaluate();
        public abstract E2Element Negate();
        public abstract override string ToString();
    }
}
