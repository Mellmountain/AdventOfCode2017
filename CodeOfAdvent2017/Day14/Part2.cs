using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day14
{
    class Part2
    {
        static void Main()
        {
            string input = "xlqgujun";
            int[,] memory = new int[128, 128];
            Part1.FillDisk(input, memory, int.MaxValue);
            int groupCount = 1;
            for(int y = 0; y < 128; y++)
                for(int x = 0; x < 128; x++)
                {
                    FloodFill(new Tuple<int, int>(x, y), groupCount, ref memory);
                    if (memory[y, x] == groupCount)
                        groupCount++;
                }

            Console.WriteLine("Group count: " + (groupCount - 1));
            Console.ReadLine();
        }

        private static void FloodFill(Tuple<int, int> from, int groupValue, ref int[,] memory)
        {
            if (memory[from.Item2, from.Item1] == 0)
                return;
            if (memory[from.Item2, from.Item1] <= groupValue)
                return;
            if (memory[from.Item2, from.Item1] == int.MaxValue)
                memory[from.Item2, from.Item1] = groupValue;

            if (from.Item2 <= 126)
                FloodFill(new Tuple<int, int>(from.Item1, from.Item2 + 1), groupValue, ref memory); /* south */
            if(from.Item2 > 0)
                FloodFill(new Tuple<int, int>(from.Item1, from.Item2 - 1), groupValue, ref memory); /* north */
            if(from.Item1 > 0)
                FloodFill(new Tuple<int, int>(from.Item1 - 1, from.Item2), groupValue, ref memory); /* west */
            if (from.Item1 <= 126)
                FloodFill(new Tuple<int, int>(from.Item1 + 1, from.Item2), groupValue, ref memory); /* east */
            return;
        }
    }
}
