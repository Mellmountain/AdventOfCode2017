using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day24
{
    class Part1
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day24\\Input\\test.txt");
            List<Node> nodes = GetAllNodes(input);
            GetAllEdges(nodes);
        }

        

        private static int FindMaxStrength(List<Bridepart> bridges)
        {
            int currentMax = 0;
            foreach(Bridepart bridge in bridges)
            {
                int strength = FindMax(bridge);
                if (currentMax < strength)
                    currentMax = strength;
            }
            return currentMax;
        }

        private static int FindMax(Bridepart bridge)
        {
            return 0;
            if (bridge.connections.Count == 0)
            {
                
            }
            else
            {

            }
        }

        public static IList<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>();

            foreach (var item in source)
            {
                Visit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

        public static void Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found.");
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        Visit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }

        private static List<Node> GetAllNodes(string[] input)
        {
            return null;
        }

        private static void GetAllEdges(List<Node> nodes)
        {
            foreach(Node node in nodes)
            {
                List<Node> edgeTo = nodes.FindAll(nd => nd.IsMatch(node));
                foreach(Node edgeToNode in edgeTo)
                {
                    node.AddEdge(new Edge(node, edgeToNode, edgeToNode.weight));
                }
            }
        }
    }

    internal class Node
    {
        public string name;
        public int portA;
        public int portB;
        public int weight;
        
        List<Edge> edges = new List<Edge>();

        public Node(string name)
        {
            this.name = name;
            string[] weights = name.Split('/');
            this.portA = Int32.Parse(weights[0]);
            this.portB = Int32.Parse(weights[1]);
            this.weight = portA + portB;
        }

        public void AddEdge(Edge edge)
        {
            edges.Add(edge);
        }

        public bool IsMatch(Node node)
        {
            if (this.Equals(node))
                return false;
            if(this.portA == 0 || this.portB == 0)
                return false;
            return node.portA == this.portA ||
                node.portB == this.portB ||
                node.portB == this.portA ||
                node.portA == this.portB;
        }
    }

    internal class Edge
    {
        Node from;
        Node to;
        int weight;

        public Edge(Node from, Node to, int weight)
        {
            this.from = from;
            this.to = to;
            this.weight = weight;
        }
    }

    class Bridepart
    {
        public int portA;
        public int portB;
        int strength;
        public int totalStrength;

        Bridepart parent;
        public List<Bridepart> connections;

        public Bridepart(int portA, int portB)
        {
            this.portA = portA;
            this.portB = portB;
            this.strength = portA + portB;
            this.totalStrength = strength;
            connections = new List<Bridepart>();
        }



        public void AddParent(Bridepart parent)
        {
            this.parent = parent;
            this.totalStrength = strength + parent.totalStrength;
        }

        public void AddConnection(Bridepart part)
        {
            if (part.portA == 0 || part.portB == 0)
                return; /* dont add "root nodes". */
            if (!(part.portA == this.portA && part.portB == this.portB)) /* dont add selfs */
                connections.Add(part);
        }
    }
}
