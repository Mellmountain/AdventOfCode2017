using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day24
{
    class Part1_take2
    {
        public static List<Node> nodes;
        public static List<Tuple<Node, Node>> edges;
        static void Main()
        {
            string[] input = File.ReadAllLines("Day24\\Input\\test.txt");
            nodes = GetAllNodes(input);
            edges = GetAllEdges(nodes);
            Graph<Node> graph = new Graph<Node>(nodes, edges);

            int maxscore = 0;
            List<Node> path = new List<Node>();
            HashSet<Node> reachable = DFS(graph, nodes[0], node => path.Add(node));
            int test = 0;
            PrintPossibleBridges(path);

        }

        private static void PrintPossibleBridges(List<Node> path)
        {
            throw new NotImplementedException();
        }

        private static HashSet<Node> DFS(Graph<Node> graph, Node start, Action<Node> preVisit = null)
        {
            var visited = new HashSet<Node>();

            if (!graph.AdjacencyList.ContainsKey(start))
                return visited;

            var stack = new Stack<Node>();
            stack.Push(start);

            while(stack.Count > 0)
            {
                var node = stack.Pop();

                if (visited.Contains(node))
                    continue;
                if (preVisit != null)
                    preVisit(node);

                visited.Add(node);

                foreach (Node neighbor in graph.AdjacencyList[node])
                    if (!visited.Contains(neighbor))
                        stack.Push(neighbor);
            }

            return visited;
        }

        private static List<Tuple<Node,Node>> GetAllEdges(List<Node> nodes)
        {
            List<Tuple<Node, Node>> result = new List<Tuple<Node, Node>>();
            foreach (Node node in nodes)
            {
                /* find all nodes we could have an edge to i.e if 
                either port match on some other edge we say we have a
                connection. Do we need to make exception for 0? */
                List<Node> neighbours = nodes.FindAll(nd => nd.IsMatch(node));
                foreach (Node neighbour in neighbours)
                {
                    result.Add(Tuple.Create(node, neighbour));
                }
            }
            return result;
        }

        private static List<Node> GetAllNodes(string[] input)
        {
            List<Node> result = new List<Node>();
            int index = 0;
            foreach (string node in input)
            {
                result.Add(new Node(node, index));
                index++;
            }

            return result;
        }

        internal class Graph<T>
        {
            public Graph() { }
            public Graph(IEnumerable<T> vertices, IEnumerable<Tuple<T, T>> edges)
            {
                foreach (var vertex in vertices)
                    AddVertex(vertex);

                foreach (var edge in edges)
                    AddEdge(edge);
            }

            public Dictionary<T, HashSet<T>> AdjacencyList { get; } = new Dictionary<T, HashSet<T>>();

            public void AddVertex(T vertex)
            {
                AdjacencyList[vertex] = new HashSet<T>();
            }

            public void AddEdge(Tuple<T, T> edge)
            {
                if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2))
                {
                    AdjacencyList[edge.Item1].Add(edge.Item2);
                    AdjacencyList[edge.Item2].Add(edge.Item1);
                }
            }
        }

        internal class Node
        {
            public string name;
            public int portA;
            public int portB;
            public int weight;
            public int index;
            public int depth;

            public Node(string name, int index)
            {
                this.name = name;
                this.index = index;
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
}
