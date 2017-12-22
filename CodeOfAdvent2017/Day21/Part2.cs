using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day21
{
    class Part2
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day21\\Input\\input.txt");
            Dictionary<string, string> enhancementRules = new Dictionary<string, string>();

            foreach (string rule in input)
            {
                string outPattern, inPattern, pattern;
                pattern = rule.Replace(">", string.Empty);
                inPattern = pattern.Split('=')[0].Trim();
                outPattern = pattern.Split('=')[1].Trim();

                enhancementRules.Add(inPattern, outPattern);
            }

            char[,] art = new char[,] { { '.', '#', '.' },
                                        { '.', '.', '#' },
                                        { '#', '#', '#' } };

            Console.WriteLine("Original");
            Part1.PrintArt(art);
            for (int i = 1; i <= 18; i++)
            {
                Console.WriteLine("Interation {" + i + "}");
                string newart = "";
                List<char[,]> artparts = new List<char[,]>();
                if (art.GetLength(0) % 2 == 0)
                {
                    Console.WriteLine("Rule 1 {%2}");

                    for (int j = 0; j + 1 < art.GetLength(0); j += 2)
                    {
                        for (int k = 0; k + 1 < art.GetLength(0); k += 2)
                        {
                            char[,] partart = new char[,] { { art[j, k], art[j, k + 1] },
                                                            { art[j + 1, k], art[j + 1, k + 1] } };
                            string[] permutations = Part1.PermutateArt(partart);
                            char[,] enhanced;
                            foreach (string permutation in permutations)
                            {
                                if (enhancementRules.ContainsKey(permutation))
                                {
                                    newart = enhancementRules[permutation];
                                    enhanced = Part1.GeneratePartArt(newart);
                                    artparts.Add(enhanced);
                                    break;
                                }
                            }
                        }
                    }
                    art = Part1.GenerateNewArt(artparts, art.GetLength(0), 2);
                    Part1.PrintArt(art);
                    artparts.Clear();
                }
                else
                {
                    Console.WriteLine("Rule 2 {%3}");
                    for (int j = 0; j + 2 < art.GetLength(0); j += 3)
                    {
                        for (int k = 0; k + 2 < art.GetLength(0); k += 3)
                        {
                            char[,] partart = new char[,] { { art[j, k], art[j, k + 1], art[j, k + 2] },
                                                            { art[j + 1, k], art[j + 1, k + 1], art[j + 1, k + 2] },
                                                            { art[j + 2, k], art[j + 2, k + 1], art[j + 2, k + 2] } };
                            string[] permutations = Part1.PermutateArt(partart);
                            char[,] enhanced;
                            foreach (string permutation in permutations)
                            {
                                if (enhancementRules.ContainsKey(permutation))
                                {
                                    newart = enhancementRules[permutation];
                                    enhanced = Part1.GeneratePartArt(newart);
                                    artparts.Add(enhanced);
                                    break;
                                }
                            }
                        }
                    }
                    art = Part1.GenerateNewArt(artparts, art.GetLength(0), 3);
                    Part1.PrintArt(art);
                    artparts.Clear();
                }

            }
            int count = 0;
            for (int i = 0; i < art.GetLength(0); i++)
                for (int j = 0; j < art.GetLength(1); j++)
                {
                    if (art[i, j] == '#')
                        count++;
                }
            Console.WriteLine("Count: " + count);
            Console.ReadLine();
        }
    }
}

