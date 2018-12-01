using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day06
{
    /// <summary>
    /// http://adventofcode.com/2017/day/6
    /// --- Day 6: Memory Reallocation ---
    /// </summary>
    class Part2
    {
        static void Main()
        {
            string input = File.ReadAllText("Day6\\Input\\test.txt");
            string[] blocks = input.Split('\t');
            int[] memoryBank = new int[blocks.Length];

            for (int i = 0; i < blocks.Length; i++)
                memoryBank[i] = Int32.Parse(blocks[i]);

            List<int[]> memoryStates = new List<int[]>();

            while (true)
            {
                int targetBankIndex = GetBankWithMostMemory(memoryBank);
                int memoryBlocks = memoryBank[targetBankIndex];
                memoryBank[targetBankIndex] = 0;

                int nextIndex = targetBankIndex + 1;
                while (memoryBlocks > 0)
                {
                    nextIndex = nextIndex % memoryBank.Length;
                    memoryBank[nextIndex] += 1;
                    memoryBlocks--;
                    nextIndex++;
                }

                if (VerifyUniqueState(memoryBank, memoryStates))
                    memoryStates.Add(memoryBank.ToArray());
                else
                    break;
            }

            Console.WriteLine(memoryStates.Count);
            Console.ReadLine();
        }

        private static bool VerifyUniqueState(int[] stateToTest, List<int[]> memoryStates)
        {
            foreach (int[] state in memoryStates)
            {
                for (int i = 0; i < state.Length; i++)
                {
                    if (stateToTest[i] != state[i])
                        break;
                    if (stateToTest[i] == state[i] && i + 1 == state.Length)
                        return false;
                }
            }
            return true;
        }

        private static int GetBankWithMostMemory(int[] memoryBank)
        {
            int highestIndex = 0;
            /* Loop from end to start to always return lowest-numbered memory */
            for (int i = memoryBank.Length - 1; i >= 0; i--)
            {
                if (memoryBank[i] >= memoryBank[highestIndex])
                    highestIndex = i;
            }
            return highestIndex;
        }

    }
}

