using System;
using System.IO;
using System.Windows.Forms;

namespace AdventOfCode2017.Day11
{
    /// <summary>
    /// http://adventofcode.com/2017/day/11
    /// --- Day 11: Hex Ed ---
    /// </summary>
    class Part1
    {
        [STAThread]
        static void Main()
        {
            string input = File.ReadAllText("Day11\\Input\\input.txt");
            string[] directions = input.Split(',');

            int x = 0;
            int y = 0;
            foreach(string direction in directions)
            {
                switch(direction)
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
            }
            int distance = (Math.Abs(0 - x) + Math.Abs(0 - x - y) + Math.Abs(0 - y)) / 2;
            Console.WriteLine(distance);
            Clipboard.SetText(distance.ToString());
            Console.ReadLine();
        }
    }
}
