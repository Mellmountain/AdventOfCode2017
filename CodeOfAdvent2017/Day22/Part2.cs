using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day22
{
    class Part2
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day22\\Input\\input.txt");
            char[,] inputMap = Part1.ReadInput(input);
            char[,] worldMap = Part1.CreateWorldMap(inputMap, inputMap.GetLength(1) * 243);

            int burst = 1;
            int infected = 0;
            int virusX = (worldMap.GetLength(1) - 1) / 2;
            int virusY = (worldMap.GetLength(0) - 1) / 2;
            Part1.Direction virusDirection = Part1.Direction.Up;

            for (; burst <= 10000000; burst++)
            {
                if (worldMap[virusY, virusX] == '.') /* node is clean */
                {
                    virusDirection = Part1.TurnLeft(virusDirection);
                    worldMap[virusY, virusX] = 'W'; /* weakened */
                }
                else if (worldMap[virusY, virusX] == 'W') /* weekened */
                {
                    worldMap[virusY, virusX] = '#'; /* infected */
                    infected++;
                }
                else if (worldMap[virusY, virusX] == '#') /* infected */
                {
                    virusDirection = Part1.TurnRight(virusDirection);
                    worldMap[virusY, virusX] = 'F'; /* flag */
                }
                else if (worldMap[virusY, virusX] == 'F') /* flagged */
                {
                    virusDirection = Reverse(virusDirection);
                    worldMap[virusY, virusX] = '.'; /* clean */
                }
                else
                    Console.WriteLine("Unknown node state!");
                //Part1.PrintMap(worldMap);
                //Console.ReadLine();

                switch (virusDirection)
                {
                    case Part1.Direction.Up:
                        virusY--;
                        break;
                    case Part1.Direction.Down:
                        virusY++;
                        break;
                    case Part1.Direction.Left:
                        virusX--;
                        break;
                    case Part1.Direction.Right:
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

        private static Part1.Direction Reverse(Part1.Direction virusDirection)
        {
            if (virusDirection == Part1.Direction.Down)
                return Part1.Direction.Up;
            else if (virusDirection == Part1.Direction.Up)
                return Part1.Direction.Down;
            else if (virusDirection == Part1.Direction.Left)
                return Part1.Direction.Right;
            else
                return Part1.Direction.Left;
        }
    }
}
