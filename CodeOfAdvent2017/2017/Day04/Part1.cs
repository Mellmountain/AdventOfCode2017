using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day04
{
    /// <summary>
    /// http://adventofcode.com/2017/day/4
    /// --- Day 4: High-Entropy Passphrases ---
    /// </summary>
    class Part1
    {
        static void Main()
        {
            int validPhrases = 0;
            var passphrases = File.ReadAllLines("Day4\\Input\\input.txt");
            foreach (string passphrase in passphrases)
            {
                if (ValidatePassphrase(passphrase))
                    validPhrases++;
            }

            Console.WriteLine("Valid passphrases: " + validPhrases);
            Console.ReadLine();
        }

        static bool ValidatePassphrase(string passphrase)
        {
            string[] words = passphrase.Split(' ');
            for (int i = 0; i < words.Length; i++)
                for (int j = 0; j < words.Length; j++)
                {
                    if (i == j) /* Don't check ourself */
                        continue;
                    if (words[i] == words[j])
                        return false;
                }
            return true;
        }
    }
}

