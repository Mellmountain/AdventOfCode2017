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
    class Part1
    {
        static void Main()
        {
            int targetValue = 265149;
            Tuple<int, int> targetLocation;
            int gridbase = 1;

            while(gridbase*gridbase < targetValue)
                gridbase += 2;

            int portLocation = (gridbase - 1) / 2;

            targetLocation = GetTargetLocation(targetValue, gridbase);
            Console.WriteLine(String.Format("Manhattan distance = {0}", Math.Abs(targetLocation.Item1 - portLocation) + Math.Abs(targetLocation.Item2 - portLocation)));
            Console.ReadLine();
        }

        public static Tuple<int,int> GetTargetLocation(int target, int gridbase)
        {
            /// Side = 1 => Bottom side of square
            /// Side = 2 => Left side of square
            /// Side = 3 => Top side of square
            /// Side = 4 => Right side of square
            int targetx;
            int targety;
            for (int side = 1; side <= 4; side++)
            {
                if(target >= (gridbase*gridbase) - (gridbase-1) * side)
                {
                    int min = (gridbase*gridbase) - (gridbase - 1) * side;
                    int max = min + (gridbase - 1);
                    if(side % 2 == 0)
                    {
                        targetx = side == 2 ? 0 : gridbase - 1;
                        targety = min == target ? 0 : max - target;
                    }
                    else
                    {
                        /* special case for top-side (index reversed) */

                        targetx = side == 1 ? Math.Abs(min - target) : max - target;
                        targety = side == 1 ? gridbase - 1 : 0;
                    }
                    return new Tuple<int, int>(targetx, targety);
                }
            }
            return null;
        }
    }
}
