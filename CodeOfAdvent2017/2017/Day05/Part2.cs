using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day05
{
    /// <summary>
    /// http://adventofcode.com/2017/day/5
    /// --- Day 5: A Maze of Twisty Trampolines, All Alike ---
    /// </summary>
    class Part2
    {
        static void Main()
        {
            List<int> jumpInstructions = new List<int>();
            var rows = File.ReadAllLines("Day5\\Input\\input.txt");

            foreach (string row in rows)
                jumpInstructions.Add(Int32.Parse(row));

            int jumps = 0;
            int currentIndex = 0;
            int offset = 0;
            while (true)
            {
                offset = jumpInstructions[currentIndex];
                if (currentIndex + offset < 0 ||
                    currentIndex + offset >= jumpInstructions.Count)
                    break;

                if(offset > 0)
                    jumpInstructions[currentIndex] = offset >= 3 ? offset - 1 : offset + 1;
                else
                    jumpInstructions[currentIndex] = Math.Abs(offset) >= 3 ? offset + 1 : offset + 1;
                currentIndex += offset;
                jumps++;
            }
            Console.WriteLine(jumps + 1);
            Console.ReadLine();
        }
    }
}
