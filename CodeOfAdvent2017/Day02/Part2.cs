using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day02
{
    /// <summary>
    /// http://adventofcode.com/2017/day/2
    /// --- Day 2: Corruption Checksum ---
    /// </summary>
    class Part2
    {
        static void Main()
        {
            int sum = 0;
            var rows = File.ReadAllLines("Day2\\Input\\input.txt");
            foreach (string row in rows)
            {
                string[] cols = row.Split('\t');
                for (int i = 0; i < cols.Length; i++)
                {
                    int value1;
                    Int32.TryParse(cols[i], out value1);

                    for(int j = 0; j < cols.Length; j++)
                    {
                        if (i == j)
                            continue;
                        int value2;
                        Int32.TryParse(cols[j], out value2);

                        if(value1 % value2 == 0)
                        {
                            sum += value1 >= value2 ? value1 / value2 : value2 / value1;
                        }
                    }
                }
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}
