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
    class Part2
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
                    if (IsAnagram(words[i], words[j]))
                        return false;
                }
            return true;
        }

        static bool IsAnagram(string word1, string word2)
        {
            if (word1.Length != word2.Length)
                return false;

            for (int i = 0; i < word2.ToCharArray().Length; i++)
            {
                /* If all letters in word1 exists in word2 we could create an anagram. */
                int index = word1.IndexOf(word2[i]);
                if (index >= 0)
                    word1.Remove(index, 1);
                else
                    return false;
            }
            return (word1.Length > 0) ? true : false;
        }
    }
}
