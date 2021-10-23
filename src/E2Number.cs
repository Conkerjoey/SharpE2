/**
* FileName: E2Number.cs
* Author : Joey Courcelles
* Created On : 23/10/2021
* Last Modified On : 23/10/2021
* Copy Rights : Joey Courcelles
* Description : Simple class defining a number node type.
*               It can be decimal, hexadecimal, octal or binary
*/

using System;

namespace ExpressionEngine
{
    public class E2Number : E2Element
    {
        public enum Format
        {
            DECIMAL,
            OCTAL,
            HEXADECIMAL,
            BINARY,
        }

        private double value;
        private Format format;

        public E2Number(double value, Format format)
        {
            this.value = value;
            this.isEvaluable = true;
            this.format = format;
        }

        public override E2Element Evaluate()
        {
            isEvaluable = true;
            if (value == 0)
            {
                return null;
            }
            return this;
        }

        public double GetValue()
        {
            return value;
        }

        public string GetValue(Format format)
        {
            if (format == Format.DECIMAL)
            {
                return value + "";
            }
            if (format == Format.HEXADECIMAL)
            {
                return "0x" + ((int)value).ToString("X");
            }
            if (format == Format.BINARY)
            {
                throw new NotImplementedException();
            }
            if (format == Format.OCTAL)
            {
                return "0" + Convert.ToString((int)value, 8);
            }
            throw new NotImplementedException();
        }

        public override E2Element Negate()
        {
            this.value *= -1;
            return this;
        }

        public override string ToString()
        {
            return "" + value;
        }
    }
}
