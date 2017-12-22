using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day22
{
    class Part1
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
            string[] input = File.ReadAllLines("Day22\\Input\\input.txt");
            char[,] inputMap = ReadInput(input);
            char[,] worldMap = CreateWorldMap(inputMap, inputMap.GetLength(1) * 243);

            int burst = 1;
            int infected = 0;
            int virusX = (worldMap.GetLength(1) - 1) / 2;
            int virusY = (worldMap.GetLength(0) - 1) / 2;
            Direction virusDirection = Direction.Up;

            for (; burst <= 10000; burst++)
            {
                if (worldMap[virusY, virusX] == '#') /* is current node infected? */
                {
                    virusDirection = TurnRight(virusDirection);
                    worldMap[virusY, virusX] = '.'; /* clean the node */
                }
                else
                {
                    virusDirection = TurnLeft(virusDirection);
                    worldMap[virusY, virusX] = '#'; /* infect */
                    infected++;
                }
                
                switch(virusDirection)
                {
                    case Direction.Up:
                        virusY--;
                        break;
                    case Direction.Down:
                        virusY++;
                        break;
                    case Direction.Left:
                        virusX--;
                        break;
                    case Direction.Right:
                        virusX++;
                        break;
                    default:
                        Console.WriteLine("Something bad happend...");
                        break;
                }
            }
            Console.WriteLine(infected);
            Console.ReadLine();
        }

        public static Direction TurnLeft(Direction virusDirection)
        {
            if (virusDirection == Direction.Up)
                return Direction.Left;
            else if (virusDirection == Direction.Down)
                return Direction.Right;
            else if (virusDirection == Direction.Left)
                return Direction.Down;
            else
                return Direction.Up;
            
        }

        public static Direction TurnRight(Direction virusDirection)
        {
            if (virusDirection == Direction.Up)
                return Direction.Right;
            else if (virusDirection == Direction.Down)
                return Direction.Left;
            else if (virusDirection == Direction.Left)
                return Direction.Up;
            else
                return Direction.Down;
        }

        public static char[,] CreateWorldMap(char[,] inputMap, int size)
        {
            char[,] result = new char[size, size];
            for (int i = 0; i < result.GetLength(0); i++)
                for (int j = 0; j < result.GetLength(1); j++)
                    result[i, j] = '.';

            int startX = size/2 - (inputMap.GetLength(1) - 1) / 2;
            int startY = size/2 - (inputMap.GetLength(1) - 1) / 2;

            for(int i = startY, y = 0; y < inputMap.GetLength(0); y++, i++)
            {
                for(int j = startX, x = 0; x < inputMap.GetLength(1); x++, j++)
                {
                    result[i, j] = inputMap[y, x];
                }
                
            }
            return result;
        }

        public static char[,] ReadInput(string[] input)
        {
            char[,] result = new char[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    result[i, j] = input[i][j];
                }
            }
            return result;
        }

        public static void PrintMap(char[,] map)
        {
            string row = "";
            for (int i = 0; i < map.GetLength(0); i++)
            {
                if (i != 0)
                    row = "";
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    row += map[i, j];
                }
                Console.WriteLine(row);
            }
            Console.WriteLine();
        }
    }
}
