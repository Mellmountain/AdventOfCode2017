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
    class Part1
    {
        static void Main()
        {
            int sum = 0;
            var input = File.ReadAllText("Day1\\Input\\input.txt");
            for(int i = 0; i < input.Length; i++)
            {
                int value;
                Int32.TryParse(input[i].ToString(), out value);

                if (i == input.Length - 1)
                    sum += (input[i] == input[0]) ? value : 0;
                else
                    sum += (input[i] == input[i + 1]) ? value : 0;
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}
