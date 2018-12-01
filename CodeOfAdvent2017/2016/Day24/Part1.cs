using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using AdventOfCode.Utils.Graphs;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016.Day24
{
    class Part1
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("2016\\Day24\\Input\\Input.txt");

            List<Target> locations;
            Target start;

            char[,] maze = ReadMaze(input, out locations, out start);
            int shortestPath = Int32.MaxValue;

            List<Target> shortest_path = new List<Target>();

            //Loop over each permutation of targets to find the shortest path.
            Algorithms algs = new Algorithms();
            var perm_targets = algs.Permutate(locations, locations.Count);

            int objects = perm_targets.Count();
            int iterator = 0;
            Point new_start = start.Location;
            int path = 0;
            foreach (var targets in perm_targets )
            {
                iterator++;
                new_start = start.Location;
                foreach(Target p in targets)
                {
                    path += AStarPathFind(new_start.X, new_start.Y, p.Location.X, p.Location.Y, maze);
                    new_start.X = p.Location.X;
                    new_start.Y = p.Location.Y;
                    //a little optimization
                    //if we know our current path is bigger than smallest quit!
                    if (path > shortestPath)
                        break;
                }
                Console.Write("{0}->", start.Index);
                foreach (Target p in targets)
                    Console.Write("{0}->", p.Index);
                Console.Write("[{0}]", path);
                Console.WriteLine();
                if (path < shortestPath)
                {
                    shortestPath = path;
                    shortest_path = new List<Target>(targets);
                    Console.WriteLine("Current shortest: {0}, tested {1}/{2} paths", shortestPath, iterator, objects);
                }
                path = 0;
            }
            
            Console.WriteLine("Minimum steps: {0}", shortestPath);
            Console.Write("{0},{1}({2})->", start.Location.X, start.Location.Y, start.Index);
            foreach (Target p in shortest_path)
                Console.Write("{0},{1}({2})->", p.Location.X, p.Location.Y, p.Index);
            Console.Write("DONE!");
            Console.ReadLine();


        }

        private static char[,] ReadMaze(string[] input, out List<Target> targets, out Target start)
        {
            targets = new List<Target>();
            start = new Target();
            int width = input[0].Length;
            int height = input.Length;
            char[,] result = new char[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (IsTarget(input[y][x]))
                    {
                        if (input[y][x] == '0')
                        {
                            start.Location = new Point(x, y);
                            start.Index = '0';
                        }
                        else
                            targets.Add(new Target { Location = new Point(x, y), Index = input[y][x] });
                        result[y, x] = '.';
                    }
                    else
                        result[y, x] = input[y][x];
                }
            }
            return result;
        }

        private static bool IsTarget(char v)
        {
            return (v == '0' ||
                    v == '1' ||
                    v == '2' ||
                    v == '3' ||
                    v == '4' ||
                    v == '5' ||
                    v == '6' ||
                    v == '7' ||
                    v == '8' ||
                    v == '9');
        }

        public static int AStarPathFind(int fromX, int fromY, int targetX, int targetY, char[,] maze)
        {
            List<GridSquare> open_list = new List<GridSquare>();
            List<GridSquare> closed_list = new List<GridSquare>();
            open_list.Add(new GridSquare(fromX, fromY, true));
            bool bTargetReachable = false;

            while (open_list.Count > 0)
            {
                GridSquare square = open_list.OrderBy(sq => sq.Score).First();
                closed_list.Add(square);

                if (closed_list.Exists(sq => sq.X == targetX && sq.Y == targetY))
                {
                    bTargetReachable = true;
                    break;
                }

                List<GridSquare> neighbours = GetNeighbours(square, maze, targetX, targetY);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    //In closed list? ignore!
                    if (closed_list.Exists(sq => sq.X == neighbours[i].X && sq.Y == neighbours[i].Y))
                        continue;
                    //In open list?
                    if (open_list.Exists(sq => sq.X == neighbours[i].X && sq.Y == neighbours[i].Y))
                    {
                        GridSquare update = open_list.Find(sq => sq.X == neighbours[i].X && sq.Y == neighbours[i].Y);
                        if (neighbours[i].Score < update.Score)
                        {
                            update.Score = neighbours[i].Score;
                        }
                    }
                    else
                    {
                        open_list.Add(neighbours[i]);
                    }
                }

                open_list.Remove(square);
            }

            int steps = 0;
            if (bTargetReachable)
            {
                GridSquare target = closed_list.First(sq => sq.X == targetX && sq.Y == targetY);
                while (target.Parent != null)
                {
                    //maze[target.Y, target.X] = 'O';
                    target = target.Parent;
                    steps++;
                }
            }

            return steps;
        }

        private static List<GridSquare> GetNeighbours(GridSquare from, char[,] maze, int targetx, int targety)
        {
            List<GridSquare> result = new List<GridSquare>();
            if (from.Y - 1 >= 0 && maze[from.Y - 1, from.X] == '.')
            {
                GridSquare neighbour = new GridSquare(from.X, from.Y - 1, true);
                neighbour.Score = Math.Abs((from.X - targetx) + (from.Y - targety - 1));
                neighbour.Parent = from;
                result.Add(neighbour);
            }
            if (from.Y + 1 < maze.GetLength(0) && maze[from.Y + 1, from.X] == '.')
            {
                GridSquare neighbour = new GridSquare(from.X, from.Y + 1, true);
                neighbour.Score = Math.Abs((from.X - targetx) + (from.Y - targety + 1));
                neighbour.Parent = from;
                result.Add(neighbour);
            }
            if (from.X - 1 >= 0 && maze[from.Y, from.X - 1] == '.')
            {
                GridSquare neighbour = new GridSquare(from.X - 1, from.Y, true);
                neighbour.Score = Math.Abs((from.X - 1 - targetx) + (from.Y - targety));
                neighbour.Parent = from;
                result.Add(neighbour);
            }
            if (from.X + 1 < maze.GetLength(1) && maze[from.Y, from.X + 1] == '.')
            {
                GridSquare neighbour = new GridSquare(from.X + 1, from.Y, true);
                neighbour.Score = Math.Abs((from.X + 1 - targetx) + (from.Y - targety));
                neighbour.Parent = from;
                result.Add(neighbour);
            }

            return result;
        }
    }
    public class Target
    {
        public Point Location { get; set; }
        public char Index { get; set; }
    }
    public class GridSquare
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Score { get; set; }

        public bool Walkable { get; private set; }
        public GridSquare Parent { get; set; }

        public GridSquare(int x, int y, bool walkable)
        {
            X = x;
            Y = y;
            Walkable = walkable;
            Parent = null;
            Score = 0;
        }
    }
}
