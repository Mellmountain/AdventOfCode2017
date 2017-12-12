using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day12
{
    /// <summary>
    /// http://adventofcode.com/2017/day/12
    /// --- Day 12: Digital Plumber ---
    /// </summary>
    class Part2
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day12\\Input\\input.txt");
            List<VillageProgram> village = new List<VillageProgram>();
            Dictionary<string, bool> groups = new Dictionary<string, bool>();
            Part1.ParseAndSetupProblem(input, village);

            int groupTotal = 0;
            foreach (VillageProgram program in village)
            {
                if (!groups.ContainsKey(program.ID))
                {
                    Dictionary<string, bool> group = Part1.BreadthFirstTraversal(program, village);
                    groups = groups.Concat(group).ToDictionary(dict => dict.Key, dict => dict.Value);
                    groupTotal++;
                }
            }

            Console.WriteLine("Total number of groups: " + groupTotal);
            Console.ReadLine();

        }
    }
}
