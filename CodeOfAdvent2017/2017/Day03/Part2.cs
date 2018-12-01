using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day03
{
    /// <summary>
    /// http://adventofcode.com/2017/day/3
    /// --- Day 3: Spiral Memory ---
    /// </summary>
    class Part2
    {
        enum SpiralDirection
        {
            Right = 0,
            Up = 1,
            Left = 2,
            Down = 3
        };

        static void Main()
        {
            int target = 265149;
            int gridbase = 515;

            int startLocation = (gridbase - 1) / 2;
            int[,] grid = new int[gridbase,gridbase];
            grid[startLocation,startLocation] = 1;

            SpiralDirection direction = SpiralDirection.Right;
            int x = startLocation;
            int y = startLocation;

            while(true)
            {                
                if (direction == SpiralDirection.Right)
                {
                    x++;
                    if (grid[x, y - 1] == 0)
                        direction = SpiralDirection.Up;
                }
                else if(direction == SpiralDirection.Up)
                {
                    y--;
                    if (grid[x - 1, y] == 0)
                        direction = SpiralDirection.Left;
                }
                else if(direction == SpiralDirection.Left)
                {
                    x--;
                    if (grid[x, y + 1] == 0)
                        direction = SpiralDirection.Down;
                }
                else
                {
                    y++;
                    if (grid[x + 1, y] == 0)
                        direction = SpiralDirection.Right;
                }

                grid[x, y] = SumOfAdjacent(new Tuple<int, int>(x, y), grid);
                if (grid[x, y] > target)
                    break;
            }
            Console.WriteLine(grid[x, y]);
            Console.ReadLine();
        }

        static int SumOfAdjacent(Tuple<int,int> square, int[,] grid)
        {
            return grid[square.Item1 - 1, square.Item2] +
                grid[square.Item1 + 1, square.Item2] +
                grid[square.Item1 + 1, square.Item2 + 1] +
                grid[square.Item1 - 1, square.Item2 - 1] +
                grid[square.Item1 + 1, square.Item2 - 1] +
                grid[square.Item1 - 1, square.Item2 + 1] +
                grid[square.Item1, square.Item2 - 1] +
                grid[square.Item1, square.Item2 + 1];
        }
    }
}
