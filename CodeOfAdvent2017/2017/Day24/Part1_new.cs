using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day24
{
    class Part1_new
    {
        public static List<Node> nodes;
        public static int[,] adjacencyMatrix;
        static void Main()
        {
            string[] input = File.ReadAllLines("Day24\\Input\\test.txt");
            nodes = GetAllNodes(input);
            adjacencyMatrix = GenerateAdjencencyMatrix(nodes);
            PrintMatrix(adjacencyMatrix);
            //List<Bridge> bridges = GenerateBridges(adjencencyMatrix);

        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("[" + matrix[i, j] + "]\t");
                }
                Console.Write('\n');
            }
            Console.ReadLine();
        }


        
        private static int[,] GenerateAdjencencyMatrix(List<Node> nodes)
        {
            int[,] matrix = new int[nodes.Count, nodes.Count];
            foreach(Node node in nodes)
            {
                /* find all nodes we could have an edge to i.e if 
                either port match on some other edge we say we have a
                connection. Do we need to make exception for 0? */
                List<Node> neighbours = nodes.FindAll(nd => nd.IsMatch(node));
                foreach(Node neighbour in neighbours)
                {
                    matrix[nodes.IndexOf(node), nodes.IndexOf(neighbour)] = 1;
                }
            }
            return matrix;
        }

        private static List<Node> GetAllNodes(string[] input)
        {
            List<Node> result = new List<Node>();
            foreach (string node in input)
                result.Add(new Node(node));

            return result;
        }

        internal class Node
        {
            public string name;
            public int portA;
            public int portB;
            public int weight;

            public Node(string name)
            {
                this.name = name;
                string[] weights = name.Split('/');
                this.portA = Int32.Parse(weights[0]);
                this.portB = Int32.Parse(weights[1]);
                this.weight = portA + portB;
            }

            public bool IsMatch(Node node)
            {
                if (this.Equals(node))
                    return false;
                if (this.portA == 0 || this.portB == 0)
                    return false;
                return node.portA == this.portA ||
                    node.portB == this.portB ||
                    node.portB == this.portA ||
                    node.portA == this.portB;
            }
        }
    }

    internal class Bridge
    {
        public List<Node> parts = new List<Node>();
        public Bridge()
        {
        }
    }
}
