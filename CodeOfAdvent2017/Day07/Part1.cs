using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day07
{
    /// <summary>
    /// http://adventofcode.com/2017/day/7
    /// --- Day 7: Recursive Circus ---
    /// </summary>
    class Part1
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day7\\Input\\input.txt");
            List<Node> singleNodes = ParseLeafs(input);
            List<Node> treeSchema = ParseChildren(input, singleNodes);
            Node root = treeSchema.Find(node => node.parent == null);

            Console.WriteLine(root.name);
            Console.ReadLine();
        }

        public static List<Node> ParseChildren(string[] input, List<Node> leafs)
        {
            List<Node> nodes = new List<Node>();
            foreach (string schema in input)
            {
                string[] node = schema.Split(' ');
                if (node.Length > 2)
                {
                    Node parent = leafs.Find(nd => nd.name == node[0]);
                    /* parse all children */
                    List<Node> children = new List<Node>();
                    for (int i = 3; i < node.Length; i++)
                    {
                        string name = node[i].Trim(',');
                        Node child = leafs.Find(ch => ch.name == name);
                        child.parent = parent;
                        children.Add(child);
                    }
                    parent.children = children;
                    nodes.Add(parent);
                }

            }
            return nodes;
        }

        public static List<Node> ParseLeafs(string[] input)
        {
            List<Node> nodes = new List<Node>();
            foreach (string schema in input)
            {
                string[] node = schema.Split(' ');
                Node n = CreateNode(node);
                nodes.Add(n);
            }
            return nodes;
        }

        public static Node CreateNode(string[] info)
        {
            string strWeight = info[1].Substring(1, info[1].Length - 2);
            int weight = Int32.Parse(strWeight);
            return new Node(info[0], null, weight, null);
        }
    }

    public class Node
    {
        public Node parent;
        public List<Node> children;
        public int weight;
        public string name;
        public int totalWeight = 0;
        public bool balanced;

        public Node(string name, Node parent, int weight, List<Node> children)
        {
            this.parent = parent;
            this.weight = weight;
            this.children = children;
            this.name = name;

        }

        internal bool IsBalanced()
        {
            if (children == null)
            {
                balanced = true;
                return true;
            }

            foreach (Node child in children)
            {
                int childWeight = 0;
                if (childWeight == 0)
                    childWeight = child.weight;
                else
                {
                    if (child.weight != childWeight)
                        return false;
                }
            }

            return true;
        }

        internal int GetTotalWeight()
        {
            int totalWeight = weight;
            if (children != null)
            {
                foreach (Node child in children)
                {
                    totalWeight += child.GetTotalWeight();
                }
            }
            this.totalWeight = totalWeight;
            return totalWeight;
        }
    }
}
