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
    class Part2
    {
        static void Main()
        {
            Generator A = new Generator(722, 16807);
            Generator B = new Generator(354, 48271);
            //Generator A = new Generator(65, 16807);
            //Generator B = new Generator(8921, 48271);

            int counter = 0;
            int matchingPairs = 0;
            while (counter < 5000000) /* loop 5 million times! */
            {
                int valueA = A.GenerateNextValue(4);
                int valueB = B.GenerateNextValue(8);

                string valueAbin = Convert.ToString(valueA, 2);
                string valueBbin = Convert.ToString(valueB, 2);

                valueAbin = Part1.Make16BitString(valueAbin);
                valueBbin = Part1.Make16BitString(valueBbin);

                int a16bit = Convert.ToInt32(valueAbin, 2);
                int b16bit = Convert.ToInt32(valueBbin, 2);

                if (a16bit == b16bit)
                    matchingPairs++;
                counter++;
            }

            Console.WriteLine(matchingPairs);
            Console.ReadLine();
        }
    }
}
