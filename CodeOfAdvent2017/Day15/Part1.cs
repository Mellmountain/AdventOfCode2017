using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day15
{
    /// <summary>
    /// http://adventofcode.com/2017/day/15
    /// --- Day 15: Dueling Generators ---
    /// </summary>
    class Part1
    {
        static void Main()
        {
            Generator A = new Generator(722, 16807);
            Generator B = new Generator(354, 48271);

            int counter = 0;
            int matchingPairs = 0;
            while(counter < 40000000) /* loop 40 million times! */
            {
                int valueA = A.GenerateNextValue();
                int valueB = B.GenerateNextValue();

                string valueAbin = Convert.ToString(valueA, 2);
                string valueBbin = Convert.ToString(valueB, 2);

                valueAbin = Make16BitString(valueAbin);
                valueBbin = Make16BitString(valueBbin);

                int a16bit = Convert.ToInt32(valueAbin, 2);
                int b16bit = Convert.ToInt32(valueBbin, 2);

                if (a16bit == b16bit)
                    matchingPairs++;
                counter++;
            }

            Console.WriteLine(matchingPairs);
            Console.ReadLine();
        }

        public static string Make16BitString(string binValue)
        {
            if (binValue.Length < 16)
            {
                while (binValue.Length != 16)
                {
                    binValue = binValue.Insert(0, "0");
                }
                return binValue;
            }
            else
            {
                while(binValue.Length != 16)
                {
                    binValue = binValue.Remove(0, 1);
                }
                return binValue;
            }
        }
    }

    class Generator
    {
        private int factor;
        private int previousValue;
        private int divisor = 2147483647;

        public Generator(int startValue, int factor)
        {
            this.previousValue = startValue;
            this.factor = factor;
            
        }

        public int GenerateNextValue()
        {
            ulong multiplication = ((ulong)previousValue * (ulong)factor);
            ulong value = multiplication % (ulong)divisor;
            previousValue = (int)value;
            return previousValue;
        }

        public int GenerateNextValue(int multiple)
        {
            while(true)
            {
                ulong multiplication = ((ulong)previousValue * (ulong)factor);
                ulong value = multiplication % (ulong)divisor;
                previousValue = (int)value;
                if (previousValue % multiple == 0)
                    break;
            }
            return previousValue;
        }
    }
}
