using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day19
{
    public class Part1
    {
        public enum Direction
        {
            Down,
            Up,
            Left,
            Right
        };

        static void Main()
        {
            string[] input = File.ReadAllLines("Day19\\Input\\input.txt");
            string[,] map = new string[input.Length, input[0].Length];

            int currentPositionX = 0;
            int currentPositionY = 0;

            Direction currentDirection = Direction.Down;

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

            while (true)
            {
                if (map[currentPositionY, currentPositionX] == "@") /* we are at the end */
                    break;

                if (map[currentPositionY, currentPositionX] == "+") /* intersection */
                {
                    if (currentDirection == Direction.Up || currentDirection == Direction.Down)
                    {
                        /* Must go left or right */
                        if (map[currentPositionY, currentPositionX - 1] == "-" ||
                            Regex.IsMatch(map[currentPositionY - 1, currentPositionX], @"^[A-Z]+$"))
                            currentDirection = Direction.Left;
                        else
                            currentDirection = Direction.Right;
                    }
                    else
                    {
                        /* Must go up or down */
                        if (map[currentPositionY - 1, currentPositionX] == "|" ||
                            Regex.IsMatch(map[currentPositionY - 1, currentPositionX], @"^[A-Z]+$"))
                            currentDirection = Direction.Up;
                        else
                            currentDirection = Direction.Down;
                    }
                }

                if (Regex.IsMatch(map[currentPositionY, currentPositionX], @"^[A-Z]+$"))
                    passedLetters += map[currentPositionY, currentPositionX];

                switch (currentDirection)
                {
                    case Direction.Up:
                        currentPositionY -= 1;
                        break;
                    case Direction.Down:
                        currentPositionY += 1;
                        break;
                    case Direction.Left:
                        currentPositionX -= 1;
                        break;
                    case Direction.Right:
                        currentPositionX += 1;
                        break;
                    default:
                        Console.WriteLine("Unkown direction!");
                        break;

                }
            }
            Console.WriteLine(passedLetters);
            Console.ReadLine();
        }
    }
}
