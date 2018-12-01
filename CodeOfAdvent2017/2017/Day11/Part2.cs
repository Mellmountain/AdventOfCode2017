using System;
using System.IO;
using System.Windows.Forms;

namespace AdventOfCode.Day11
{
    /// <summary>
    /// http://adventofcode.com/2017/day/11
    /// --- Day 11: Hex Ed ---
    /// </summary>
    class Part2
    {
        [STAThread]
        static void Main()
        {
            string input = File.ReadAllText("Day11\\Input\\input.txt");
            string[] directions = input.Split(',');

            int x = 0;
            int y = 0;
            int maxDistance = 0;
            foreach (string direction in directions)
            {
                switch (direction)
                {
                    case "s":
                            y++;
                            break;
                    case "se":
                            x++;
                            break;
                    case "sw":
                            x--;
                            y++;
                            break;
                    case "n":
                            y--;
                            break;
                    case "ne":
                            x++;
                            y--;
                            break;
                    case "nw":
                            x--;
                            break;
                    default:
                            Console.WriteLine("Unknown direction!");
                            break;
                }
                int distFromCenter = (Math.Abs(x) + Math.Abs(0 - x - y) + Math.Abs(y)) / 2;
                maxDistance = distFromCenter > maxDistance ? distFromCenter : maxDistance;
            }
            
            Console.WriteLine(maxDistance);
            Clipboard.SetText(maxDistance.ToString());
            Console.ReadLine();
        }
    }
}
