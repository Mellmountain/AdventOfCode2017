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
    class Part1
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day12\\Input\\input.txt");
            List<VillageProgram> village = new List<VillageProgram>();

            ParseAndSetupProblem(input, village);

            VillageProgram source = village.Find(program => program.ID == "0");
            Console.WriteLine("Programs in group 0: " + BreadthFirstTraversal(source, village).Count);
            Console.ReadLine();
        }

        public static Dictionary<string, bool> BreadthFirstTraversal(VillageProgram source, List<VillageProgram> village)
        {
            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            Queue<VillageProgram> queue = new Queue<VillageProgram>();
            visited.Add(source.ID, true);
            queue.Enqueue(source);
            while(queue.Count != 0)
            {
                VillageProgram program = queue.Dequeue();
                program = village.Find(p => p.ID == program.ID);
                foreach(VillageProgram prog in program.connections)
                {
                    if(!visited.ContainsKey(prog.ID))
                    {
                        visited.Add(prog.ID, true);
                        queue.Enqueue(prog);
                    }
                }
            }
            return visited;
        }

        public static void ParseAndSetupProblem(string[] input, List<VillageProgram> village)
        {
            foreach (string list in input)
            {
                string[] parts = list.Split(' ');
                string programID = parts[0];
                string[] connections = GetConnectionsFromInput(parts);
                VillageProgram program;

                if (!village.Exists(prog => prog.ID == programID))
                    program = new VillageProgram(programID);
                else
                    program = village.Find(prog => prog.ID == parts[0]);


                foreach (string programId in connections)
                {
                    VillageProgram con = new VillageProgram(programId);
                    program.connections.Add(con);
                }
                village.Add(program);
            }
        }

        private static string[] GetConnectionsFromInput(string[] parts)
        {
            string formatted = "";
            for (int i = 2; i < parts.Length; i++)
            {
                formatted += parts[i];
            }
            return formatted.Split(',');
        }
    }

    class VillageProgram
    {
        public string ID;
        public List<VillageProgram> connections;

        public VillageProgram(string id)
        {
            this.ID = id;
            connections = new List<VillageProgram>();
        }
    }
}
