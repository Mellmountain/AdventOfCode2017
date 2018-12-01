using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day07
{
    /// <summary>
    /// http://adventofcode.com/2017/day/7
    /// --- Day 7: Recursive Circus ---
    /// </summary>
    class Part2
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day7\\Input\\input.txt");
            List<Node> singleNodes = Part1.ParseLeafs(input);
            List<Node> treeSchema = Part1.ParseChildren(input, singleNodes);
            Node root = treeSchema.Find(node => node.parent == null);

            int imbalance = 0;
            Node badNode = FindImbalance(root, 0, out imbalance);

            Console.WriteLine("Node " + badNode.name + " (" + badNode.weight + ")" + " needs to be adjusted " + imbalance);
            Console.ReadLine();
        }

        private static Node FindImbalance(Node node, int diff, out int imbalance)
        {
            if (node.children.Count == 0 || ChildrenAreBalanced(node.children))
            {
                imbalance = diff;
                return node;
            }

            Node badChild = FindImbalancedChild(node.children);
            node.children.Remove(badChild);
            imbalance = node.children.First().totalWeight - badChild.totalWeight;
            diff = imbalance;
            return FindImbalance(badChild, diff, out imbalance);

        }

        private static Node FindImbalancedChild(List<Node> children)
        {
            int totalWeight = 0;
            foreach(Node child in children.OrderBy(child => child.weight))
            {
                if (totalWeight == 0)
                    totalWeight = child.totalWeight;
                else
                {
                    if (totalWeight != child.totalWeight)
                        return child;
                }
            }
            return null; /* should not happend */
        }

        private static bool ChildrenAreBalanced(List<Node> children)
        {
            int totalWeight = children.First().totalWeight;
            foreach(Node child in children)
            {
                if (totalWeight != child.totalWeight)
                    return false;
            }
            return true;
        }
    }
}
