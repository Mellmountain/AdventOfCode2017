using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day22
{
    class Part1
    {
        static void Main(string[] args)
        {
            int nodeY = 31;

            Dictionary<Point, GridNode> nodes = new Dictionary<Point, GridNode>();

            string[] input = File.ReadAllLines("2016\\Day22\\Input\\Input.txt");
            for (int i = 0, x = 0, y = 0; i < input.Length; i++, y++)
            {
                string[] data = input[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                nodes.Add(new Point(x, y), new GridNode
                {
                    Position = new Point(x, y),
                    Capacity = Int32.Parse(data[1].Substring(0, data[1].Length - 1)),
                    Used = Int32.Parse(data[2].Substring(0, data[2].Length - 1))
                });

                if (y == nodeY)
                {
                    x++;
                    y = 0;
                }
            }

            if (nodes.Count != input.Length)
                Console.WriteLine("We fucked up!");

            int pairs = 0;
            foreach(KeyValuePair<Point, GridNode> a in nodes)
            {
                foreach (KeyValuePair<Point, GridNode> b in nodes)
                {
                    if (!a.Value.Empty &&
                        a.Value.Position != b.Value.Position &&
                        b.Value.Accepts(a.Value.Used))
                        pairs++;
                }
            }

            Console.WriteLine("Number of viable pairs: {0}", pairs);
            Console.ReadLine();
            
        }
    }

    public class GridNode
    {
        public Point Position { get; set; }
        public int Capacity { get; set; }
        public int Used { get; set; }
        public int Available { get { return Capacity - Used; } }
        public int Usage { get { return (Used / Capacity) * 100; } }
        public bool Empty { get { return Used == 0; } }

        public bool Accepts(int amount)
        {
            return amount <= Available;
        }
    }

}
