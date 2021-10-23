/**
* FileName: E2Binary.cs
* Author : Joey Courcelles
* Created On : 23/10/2021
* Last Modified On : 23/10/2021
* Copy Rights : Joey Courcelles
* Description : This node class group two other nodes in other to be able to create a tree of node.
*               An Operator defines how the two nodes are linked.
*/

using System;

namespace SharpE2
{
    public class E2Binary : E2Element
    {
        private E2Element lhs;
        private E2Element rhs;
        private Operator op;

        public E2Binary(E2Element lhs, E2Element rhs, Operator op)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
            this.isEvaluable = lhs.isEvaluable && rhs.isEvaluable;
        }

        public override E2Element Negate()
        {
            if (op == Operator.ADDITION || op == Operator.SUBTRACTION)
            {
                lhs = lhs.Negate();
                rhs = rhs.Negate();
            }
            else if (op == Operator.MULTIPLICATION || op == Operator.DIVISION)
            {
                if (lhs.isEvaluable)
                {
                    lhs = lhs.Negate();
                }
                else if (rhs.isEvaluable)
                {
                    rhs = rhs.Negate();
                }
                else
                {
                    lhs = lhs.Negate();
                }
            }
            else if (op == Operator.EXPONENT)
            {
                lhs = lhs.Negate();
            }
            return this;
        }

        public override string ToString()
        {
            if (rhs is E2Unary)
            {
                if (op == Operator.ADDITION)
                {
                    return "(" + lhs.ToString() + rhs.ToString() + ")";
                }
                else if(op == Operator.SUBTRACTION)
                {
                    return "(" + lhs.ToString() + OperatorToString(op) + rhs.Negate().ToString() + ")";
                }
            }
            else if (rhs is E2Number)
            {
                if (op == Operator.ADDITION && ((E2Number)rhs).GetValue() < 0)
                {
                    return "(" + lhs.ToString() + rhs.ToString() + ")";
                }
            }
            return "(" + lhs.ToString() + OperatorToString(op) + rhs.ToString() + ")";
        }

        public Operator GetOperator()
        {
            return op;
        }

        public E2Element GetLhs()
        {
            return lhs;
        }

        public E2Element GetRhs()
        {
            return rhs;
        }

        public string OperatorToString(Operator op)
        {
            if (op == Operator.NOP)
            {
                return "";
            }
            else if (op == Operator.ADDITION)
            {
                return "+";
            }
            else if (op == Operator.SUBTRACTION)
            {
                return "-";
            }
            else if (op == Operator.MULTIPLICATION)
            {
                return "*";
            }
            else if (op == Operator.DIVISION)
            {
                return "/";
            }
            else if (op == Operator.EXPONENT)
            {
                return "^";
            }
            else if (op == Operator.MODULO)
            {
                return "%";
            }
            return "";
        }

        public override E2Element Evaluate()
        {
            lhs = lhs.Evaluate();
            rhs = rhs.Evaluate();

            if (lhs == null)
            {
                if (op == Operator.ADDITION || op == Operator.SUBTRACTION)
                {
                    return rhs;
                }
                else if (op == Operator.MULTIPLICATION || op == Operator.DIVISION)
                {
                    return new E2Number(0, E2Number.Format.DECIMAL);
                }
            }
            if (rhs == null)
            {
                if (op == Operator.ADDITION || op == Operator.SUBTRACTION)
                {
                    return lhs;
                }
                else if (op == Operator.MULTIPLICATION || op == Operator.DIVISION)
                {
                    return new E2Number(0, E2Number.Format.DECIMAL);
                }
            }

            if (lhs.isEvaluable && rhs.isEvaluable)
            {
                return ApplyOperator(((E2Number)lhs), ((E2Number)rhs), op);
            }
            else if (lhs.isEvaluable && !rhs.isEvaluable)
            {
                if (rhs is E2Binary)
                {
                    if (((E2Binary)rhs).GetOperator() == op)
                    {
                        if (((E2Binary)rhs).GetRhs() is E2Number)
                        {
                            lhs = ApplyOperator(((E2Number)((E2Binary)rhs).GetRhs()), ((E2Number)lhs), op);
                            rhs = ((E2Binary)rhs).GetLhs();
                        }
                        else if(((E2Binary)rhs).GetLhs() is E2Number)
                        {
                            lhs = ApplyOperator(((E2Number)((E2Binary)rhs).GetLhs()), ((E2Number)lhs), op);
                            rhs = ((E2Binary)rhs).GetRhs();
                        }
                    }
                }
            }
            else if (!lhs.isEvaluable && rhs.isEvaluable)
            {
                if (lhs is E2Binary)
                {
                    if (((E2Binary)lhs).GetOperator() == op)
                    {
                        if (((E2Binary)lhs).GetRhs() is E2Number)
                        {
                            rhs = ApplyOperator(((E2Number)((E2Binary)lhs).GetRhs()), ((E2Number)rhs), op);
                            lhs = ((E2Binary)lhs).GetLhs();
                        }
                        else if (((E2Binary)lhs).GetLhs() is E2Number)
                        {
                            rhs = ApplyOperator(((E2Number)((E2Binary)lhs).GetLhs()), ((E2Number)rhs), op);
                            lhs = ((E2Binary)lhs).GetRhs();
                        }
                    }
                }
            }
            this.isEvaluable = lhs.isEvaluable && rhs.isEvaluable;
            return this;
        }

        private E2Number ApplyOperator(E2Number lhs, E2Number rhs, Operator op)
        {
            if (op == Operator.ADDITION)
            {
                return new E2Number(lhs.GetValue() + rhs.GetValue(), E2Number.Format.DECIMAL);
            }
            if (op == Operator.SUBTRACTION)
            {
                return new E2Number(lhs.GetValue() - rhs.GetValue(), E2Number.Format.DECIMAL);
            }
            if (op == Operator.MULTIPLICATION)
            {
                return new E2Number(lhs.GetValue() * rhs.GetValue(), E2Number.Format.DECIMAL);
            }
            if (op == Operator.DIVISION)
            {
                return new E2Number(lhs.GetValue() / rhs.GetValue(), E2Number.Format.DECIMAL);
            }
            if (op == Operator.EXPONENT)
            {
                return new E2Number(Math.Pow(lhs.GetValue(), rhs.GetValue()), E2Number.Format.DECIMAL);
            }
            if (op == Operator.MODULO)
            {
                return new E2Number(lhs.GetValue() % rhs.GetValue(), E2Number.Format.DECIMAL);
            }
            throw new NotSupportedException("This Operator is not yet fully implemented.");
        }
    }
}
