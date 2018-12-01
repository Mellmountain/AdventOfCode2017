using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14
{
    class Part1
    {
        static void Main()
        {
            string input = "flqrgnkx";
            int[,] memory = new int[128, 128];

            /* generate the hashes */
            FillDisk(input, memory, 1);

            int usedMemory = 0;
            for (int i = 0; i < 128; i++)
                for (int j = 0; j < 128; j++)
                {
                    if (memory[i, j] == 1)
                        usedMemory++;
                }

            Console.WriteLine("Used memory:" + usedMemory);
            Console.ReadLine();
        }

        public static void FillDisk(string input, int[,] memory, int fillValue)
        {
            for (int i = 0; i < 128; i++)
            {
                string hexhash = Day10.Part2.KnotHash(input + "-" + i);
                char[] hexchars = hexhash.ToCharArray();
                string binaryRepresentation = ConverToBinary(hexchars);

                for (int k = 0; k < 128; k++)
                {
                    if (binaryRepresentation[k] == '0')
                        memory[i, k] = 0;
                    else
                        memory[i, k] = fillValue;
                }
            }
        }

        private static string ConverToBinary(char[] hexchars)
        {
            string output = "";
            for (int j = 0; j < hexchars.Length; j++)
            {
                string binary = Convert.ToString(Convert.ToInt32(hexchars[j].ToString(), 16), 2);
                while (binary.Length != 4)
                    binary = binary.Insert(0, "0");
                output += binary;
            }

            return output;
        }
    }
}
