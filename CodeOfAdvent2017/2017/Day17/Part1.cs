using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17
{
    class Part1
    {
        static void Main()
        {
            int stepSize = 343;
            int stopValue = 2017;
            List<int> circularBuffer = GenerateCircularBuffer(stepSize, stopValue);
            int index = circularBuffer.FindIndex(integer => integer == stopValue);

            Console.WriteLine(circularBuffer[index + 1]);
            Console.ReadLine();
        }

        private static List<int> GenerateCircularBuffer(int stepSize, int stopValue)
        {
            int tempValue = 1;
            int currentIndex = 0;
            int previousIndex = 0;
            List<int> buffer = new List<int>();
            buffer.Add(0);
            while(tempValue <= stopValue)
            {
                currentIndex = (previousIndex + stepSize) % buffer.Count;
                buffer.Insert(currentIndex + 1, tempValue);
                currentIndex += 1;
                previousIndex = currentIndex;
                tempValue++;
            }
            return buffer;
        }
    }
}
