using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day24
{
    class Part1_cheat
    {
        internal class Component
        {
            public int port1, port2;
            public bool used;
            public Component(int port1, int port2, bool used)
            {
                this.port1 = port1;
                this.port2 = port2;
                this.used = used;
            }
        }


        static int max_length;
        static int max_overall_strength;
        static int max_strength_among_longest;
        static List<Component> components;

        public static void Main()
        {
            string[] input = File.ReadAllLines("Day24\\Input\\input.txt");
            components = GetAllComponents(input);
            Recurse(0, 0, 0);

            Console.WriteLine("Part1 " + max_overall_strength);
            Console.WriteLine("Part2 " + max_strength_among_longest);
            Console.ReadLine();
        }

        private static void Recurse(int ports, int length, int strength)
        {
            max_overall_strength = Math.Max(strength, max_overall_strength);
            max_length = Math.Max(length, max_length);

            if (length == max_length)
                max_strength_among_longest = Math.Max(strength, max_strength_among_longest);

            foreach(Component c in components)
            {
                if(!c.used && (c.port1 == ports || c.port2 == ports))
                {
                    c.used = true;
                    Recurse((c.port1 == ports) ? c.port2 : c.port1, length + 1, strength + c.port1 + c.port2);
                    c.used = false;
                }
            }
        }

        private static List<Component> GetAllComponents(string[] input)
        {
            List<Component> result = new List<Component>();
            foreach (string node in input)
            {
                string[] ports = node.Split('/');
                result.Add(new Component(Int32.Parse(ports[0]), Int32.Parse(ports[1]), false));
            }

            return result;
        }
    }
    
}
