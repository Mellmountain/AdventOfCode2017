using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day10
{
    /// <summary>
    /// http://adventofcode.com/2017/day/10
    /// --- Day 10: Knot Hash ---
    /// </summary>
    class Part1
    {
        static void Main()
        {
            int[] hash = new int[256];
            for (int i = 0; i < hash.Length; i++)
                hash[i] = i;

            string data = File.ReadAllText("Day10\\Input\\input.txt");
            string[] input = data.Split(',');
            int[] lengths = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
                lengths[i] = Int32.Parse(input[i]);

            int currentPosition = 0;
            int skipSize = 0;
            KnotHash(ref hash, ref lengths, 0, 0, out currentPosition, out skipSize);

            Console.WriteLine(hash[0] * hash[1]);
            Console.ReadLine();
        }

        static public void KnotHash(ref int[] hash, ref int[] lengths, 
            int skip, int position, out int currentPosition, out int skipSize)
        {
            for (int i = 0; i < lengths.Length; i++)
            {
                int[] reversed = new int[lengths[i]];
                CopyArrayAndReverse(ref hash, ref reversed, position, lengths[i]);

                position += lengths[i] + skip;
                skip++;
            }
            skipSize = skip;
            currentPosition = position;
        }

        static void CopyArrayAndReverse(ref int[] source, ref int[] target, int startIndex, int length)
        {
            int tempIndex = startIndex;
            for (int i = 0; i < length; i++)
            {
                tempIndex = tempIndex % source.Length;
                target[i] = source[tempIndex];
                tempIndex++;
            }

            foreach (int element in target.Reverse())
            {
                startIndex = startIndex % source.Length;
                source[startIndex] = element;
                startIndex++;
            }
        }
    }
}
