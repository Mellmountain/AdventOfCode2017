using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day13
{
    class Part1
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("2015\\Day13\\Input\\input.txt");
            List<Person> table = new List<Person>();

            foreach(string arrangement in input)
            {
                string[] parts = arrangement.Split(' ');
                string name = parts[0];
                int happiness = Int32.Parse(parts[3]);
                string neighbour = parts[10].Substring(0, parts[10].Length - 1);
                bool positive = parts[2] == "gain" ? true : false;
                happiness = positive ? happiness : -1 * happiness;
                if (table.Exists(p => p.name == name))
                {
                    Person person = table.Find(p => p.name == name);
                    person.AddNeighbour(neighbour, happiness);
                }
                else
                {
                    Person person = new Person(name);
                    person.AddNeighbour(neighbour, happiness);
                    table.Add(person);
                }
            }

            Person me = new Person("me");
            foreach(Person person in table)
            {
                person.AddNeighbour("me", 0);
                me.AddNeighbour(person.name, 0);
            }
            table.Add(me);

            Utils.Graphs.Algorithms algs = new Utils.Graphs.Algorithms();
            var permutations = algs.Permutate<Person>(table, table.Count);
            int minHappiness = Int32.MaxValue;
            int maxHappiness = Int32.MinValue;
            foreach (var permutation in permutations)
            {
                int happiness = 0;
                for (int i = 0; i < permutation.Count(); i++)
                {
                    Person person = permutation.ElementAt(i);
                    if (i == 0)
                    {
                        happiness += person.neighbours[permutation.ElementAt(i + 1).name];
                        happiness += person.neighbours[permutation.ElementAt(permutation.Count() - 1).name];
                    }
                    else if(i > 0 && i < permutation.Count() - 1)
                    {
                        happiness += person.neighbours[permutation.ElementAt(i - 1).name];
                        happiness += person.neighbours[permutation.ElementAt(i + 1).name];
                    }
                    else
                    {
                        happiness += person.neighbours[permutation.ElementAt(i - 1).name];
                        happiness += person.neighbours[permutation.ElementAt(0).name];
                    }
                    string padd = i != permutation.Count() - 1 ? " -> " : "";
                    Console.Write(person.name + padd);
                }
                if (happiness < minHappiness)
                    minHappiness = happiness;
                if (happiness > maxHappiness)
                    maxHappiness = happiness;
                Console.Write(" (" + happiness + ")");
                Console.WriteLine();
            }

            Console.WriteLine("Minimum distance = " + minHappiness);
            Console.WriteLine("Maximum distance = " + maxHappiness);
            Console.ReadLine();
        }

        internal class Person
        {
            public string name;
            public Dictionary<string, int> neighbours;

            public Person(string name)
            {
                this.name = name;
                neighbours = new Dictionary<string, int>();
            }

            public void AddNeighbour(string name, int happiness)
            {
                if (!neighbours.ContainsKey(name))
                    neighbours.Add(name, happiness);
            }
        }
    }
}
