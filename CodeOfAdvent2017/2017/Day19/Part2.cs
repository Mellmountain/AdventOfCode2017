using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day19
{
    class Part2
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day19\\Input\\input.txt");
            string[,] map = new string[input.Length, input[0].Length];

            int currentPositionX = 0;
            int currentPositionY = 0;

            Part1.Direction currentDirection = Part1.Direction.Down;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    string part = input[i].Substring(j, 1);
                    if (i == 0 && part == "|")
                    {
                        currentPositionX = j;
                        currentPositionY = i;
                    }

                    if (part == " ")
                        map[i, j] = "@";
                    else
                        map[i, j] = part;
                }
            }

            string passedLetters = "";
            int steps = 0;

            while (true)
            {
                if (map[currentPositionY, currentPositionX] == "@") /* we are at the end */
                    break;

                if (map[currentPositionY, currentPositionX] == "+") /* intersection */
                {
                    if (currentDirection == Part1.Direction.Up || currentDirection == Part1.Direction.Down)
                    {
                        /* Must go left or right */
                        if (map[currentPositionY, currentPositionX - 1] == "-" ||
                            Regex.IsMatch(map[currentPositionY - 1, currentPositionX], @"^[A-Z]+$"))
                            currentDirection = Part1.Direction.Left;
                        else
                            currentDirection = Part1.Direction.Right;
                    }
                    else
                    {
                        /* Must go up or down */
                        if (map[currentPositionY - 1, currentPositionX] == "|" ||
                            Regex.IsMatch(map[currentPositionY - 1, currentPositionX], @"^[A-Z]+$"))
                            currentDirection = Part1.Direction.Up;
                        else
                            currentDirection = Part1.Direction.Down;
                    }
                }

                if (Regex.IsMatch(map[currentPositionY, currentPositionX], @"^[A-Z]+$"))
                    passedLetters += map[currentPositionY, currentPositionX];

                switch (currentDirection)
                {
                    case Part1.Direction.Up:
                        currentPositionY -= 1;
                        break;
                    case Part1.Direction.Down:
                        currentPositionY += 1;
                        break;
                    case Part1.Direction.Left:
                        currentPositionX -= 1;
                        break;
                    case Part1.Direction.Right:
                        currentPositionX += 1;
                        break;
                    default:
                        Console.WriteLine("Unkown direction!");
                        break;

                }
                steps++;
            }
            Console.WriteLine(passedLetters);
            Console.WriteLine(steps);
            Console.ReadLine();
        }
    }
}
