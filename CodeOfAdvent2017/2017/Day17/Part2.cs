using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17
{
    class Part2
    {
        static void Main()
        {
            int stopValue = 50000000;
            int stepSize = 343;
            int tempValue = 1;
            int currentIndex = 0;
            int previousIndex = 0;
            int bufferCount = 1; /* zero added */
            while (tempValue <= stopValue)
            {
                currentIndex = (previousIndex + stepSize) % bufferCount;
                if (currentIndex == 0)
                    Console.WriteLine("Inserted " + tempValue + "\t after zero. \tCount: " + bufferCount + "\t Previous Index: " + previousIndex);

                currentIndex += 1;
                previousIndex = currentIndex;
                tempValue++;
                bufferCount++;
            }
            Console.ReadLine();
        }
    }
}
