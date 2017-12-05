using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent2017.Day5
{
    class Part1
    {
        static void Main()
        {
            List<int> jumpInstructions = new List<int>();
            var rows = File.ReadAllLines("Day5\\Input\\input.txt");

            foreach(string row in rows)
                jumpInstructions.Add(Int32.Parse(row));

            int jumps = 0;
            int currentIndex = 0;
            int nextInstruction = 0;
            while (true)
            {
                nextInstruction = jumpInstructions[currentIndex];
                if (currentIndex + nextInstruction < 0 ||
                    currentIndex + nextInstruction >= jumpInstructions.Count)
                    break;

                jumpInstructions[currentIndex] = nextInstruction + 1;
                currentIndex += nextInstruction;
                jumps++;
            }
            Console.WriteLine(jumps + 1);
            Console.ReadLine();
        }
    }
}
