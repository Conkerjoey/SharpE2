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
        NOP, // level 0
        ADDITION, // level 1
        SUBTRACTION, // level 1
        MULTIPLICATION, // level 2
        DIVISION, // level 2
        EXPONENT, // level 3
        MODULO, // level 3
    }

    public enum UnaryOperator
    {
        NOP, // level 0
        MINUS, // level 1
    }

    public abstract class E2Element
    {
        public int priority = 0;
        public bool isEvaluable = false;
        public abstract E2Element Evaluate();
        public abstract E2Element Negate();
        public abstract override string ToString();
    }
}
