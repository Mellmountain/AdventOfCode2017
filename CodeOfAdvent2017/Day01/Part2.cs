using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day01
{
    /// <summary>
    /// http://adventofcode.com/2017/day/1
    /// --- Day 1: Inverse Captcha ---
    /// </summary>
    class Part2
    {
        static void Main()
        {
            var input = File.ReadAllText("Day1\\Input\\input.txt");

            int sum = 0;
            int step = input.Length / 2;

            for (int i = 0; i < input.Length; i++)
            {
                int value;
                Int32.TryParse(input[i].ToString(), out value);

                if (i + step >= input.Length)
                {
                    int diff = i + step - input.Length;
                    sum += (input[i] == input[diff]) ? value : 0;
                }
                else
                    sum += (input[i] == input[i + step]) ? value : 0;
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}
