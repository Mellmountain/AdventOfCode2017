using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent2017.Day6
{
    class Part1
    {
        static void Main()
        {
            string input = File.ReadAllText("Day6\\Input\\input.txt");
            string[] blocks = input.Split('\t');
            int[] memoryBank = new int[blocks.Length];

            for (int i = 0; i < blocks.Length; i++)
                memoryBank[i] = Int32.Parse(blocks[i]);

            List<string> memoryStates = new List<string>();

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

                string state = "";
                for (int i = 0; i < memoryBank.Length; i++)
                {
                    state += memoryBank[i];
                }

                if (VerifyUniqueState(state, memoryStates))
                    memoryStates.Add(state);
                else
                    break;
            }

            Console.WriteLine(memoryStates.Count + 1);
            Console.ReadLine();
        }

        private static bool VerifyUniqueState(string stateToTest, List<string> memoryStates)
        {
            foreach (string state in memoryStates)
            {
                if (state == stateToTest)
                    return false;
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
