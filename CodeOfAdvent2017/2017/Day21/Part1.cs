using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day21
{
    class Part1
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
            PrintArt(art);
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Interation {" + i + "}");
                string newart = "";
                List<char[,]> artparts = new List<char[,]>();
                if (art.GetLength(0) % 2 == 0)
                {
                    Console.WriteLine("Rule 1 {%2}");
                    
                    for (int j = 0; j + 1 < art.GetLength(0); j+=2)
                    {
                        for(int k = 0; k + 1 < art.GetLength(0); k+=2)
                        {
                            char[,] partart = new char[,] { { art[j, k], art[j, k + 1] },
                                                            { art[j + 1, k], art[j + 1, k + 1] } };
                            string[] permutations = PermutateArt(partart);
                            char[,] enhanced;
                            foreach (string permutation in permutations)
                            {
                                if(enhancementRules.ContainsKey(permutation))
                                {
                                    newart = enhancementRules[permutation];
                                    enhanced = GeneratePartArt(newart);
                                    artparts.Add(enhanced);
                                    break;
                                }
                            }
                        }
                    }
                    art = GenerateNewArt(artparts, art.GetLength(0), 2);
                    PrintArt(art);
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
                            string[] permutations = PermutateArt(partart);
                            char[,] enhanced;
                            foreach (string permutation in permutations)
                            {
                                if (enhancementRules.ContainsKey(permutation))
                                {
                                    newart = enhancementRules[permutation];
                                    enhanced = GeneratePartArt(newart);
                                    artparts.Add(enhanced);
                                    break;
                                }
                            }
                        }
                    }
                    art = GenerateNewArt(artparts, art.GetLength(0), 3);
                    PrintArt(art);
                    artparts.Clear();
                }
                
            }
            int count = 0;
            for(int i = 0; i < art.GetLength(0); i++)
                for(int j = 0; j < art.GetLength(1);j++)
                {
                    if (art[i, j] == '#')
                        count++;
                }
            Console.WriteLine("Count: " + count);
            Console.ReadLine();
        }

        

        public static void PrintArt(char[,] art)
        {
            string row = "";
            for (int i = 0; i < art.GetLength(0); i++)
            {
                if (i != 0)
                    row = "";
                for (int j = 0; j < art.GetLength(1); j++)
                {
                    row += art[i, j];
                }
                Console.WriteLine(row);
            }
            Console.WriteLine();
        }

        public static string[] PermutateArt(char[,] art)
        {
            string[] permutations = new string[8];
            permutations[0] = TranslateArt(art);
            permutations[1] = Rotate(art, out art);
            permutations[2] = Flip(art, out art);
            Flip(art, out art);
            permutations[3] = Rotate(art, out art);
            permutations[4] = Flip(art, out art);
            Flip(art, out art);
            permutations[5] = Rotate(art, out art);
            permutations[6] = Flip(art, out art);
            Flip(art, out art);
            Rotate(art, out art);
            permutations[7] = Flip(art, out art);
            return permutations;
        }

        public static string TranslateArt(char[,] art)
        {
            string result = "";
            for(int i = 0; i < art.GetLength(0); i++)
            {
                if (i != 0)
                    result += "/";
                for(int j = 0; j < art.GetLength(1); j++)
                {
                    result += art[i, j];
                }
            }
            return result;
        }

        public static char[,] GenerateNewArt(List<char[,]> artparts, int currentSize, int divisor)
        {
            int partSize = artparts[0].GetLength(0);
            int artSize = (int)Math.Sqrt(artparts.Count) * partSize;
            if (artparts.Count == 1)
                return GeneratePartArt(TranslateArt(artparts[0]));


            char[,] newart = new char[artSize, artSize];
            int listitem = 0;
            for (int j = 0; j + (partSize - 1) < newart.GetLength(0); j += partSize)
            {
                for (int k = 0; k + (partSize - 1) < newart.GetLength(1); k += partSize)
                {
                    if (partSize == 2)
                    {
                        newart[j, k] = artparts[listitem][0, 0];
                        newart[j, k + 1] = artparts[listitem][0, 1];
                        newart[j + 1, k] = artparts[listitem][1, 0];
                        newart[j + 1, k + 1] = artparts[listitem][1, 1];
                    }
                    else if (partSize == 3)
                    {
                        newart[j, k] = artparts[listitem][0, 0];
                        newart[j, k + 1] = artparts[listitem][0, 1];
                        newart[j, k + 2] = artparts[listitem][0, 2];
                        newart[j + 1, k] = artparts[listitem][1, 0];
                        newart[j + 1, k + 1] = artparts[listitem][1, 1];
                        newart[j + 1, k + 2] = artparts[listitem][1, 2];
                        newart[j + 2, k] = artparts[listitem][2, 0];
                        newart[j + 2, k + 1] = artparts[listitem][2, 1];
                        newart[j + 2, k + 2] = artparts[listitem][2, 2];
                    }
                    else if(partSize == 4)
                    {
                        newart[j, k] = artparts[listitem][0, 0];
                        newart[j, k + 1] = artparts[listitem][0, 1];
                        newart[j, k + 2] = artparts[listitem][0, 2];
                        newart[j, k + 3] = artparts[listitem][0, 3];
                        newart[j + 1, k] = artparts[listitem][1, 0];
                        newart[j + 1, k + 1] = artparts[listitem][1, 1];
                        newart[j + 1, k + 2] = artparts[listitem][1, 2];
                        newart[j + 1, k + 3] = artparts[listitem][1, 3];
                        newart[j + 2, k] = artparts[listitem][2, 0];
                        newart[j + 2, k + 1] = artparts[listitem][2, 1];
                        newart[j + 2, k + 2] = artparts[listitem][2, 2];
                        newart[j + 2, k + 3] = artparts[listitem][2, 3];
                        newart[j + 3, k] = artparts[listitem][3, 0];
                        newart[j + 3, k + 1] = artparts[listitem][3, 1];
                        newart[j + 3, k + 2] = artparts[listitem][3, 2];
                        newart[j + 3, k + 3] = artparts[listitem][3, 3];
                    }
                    else
                    {
                        Console.WriteLine("Unknown partsize!");
                    }
                    listitem++;
                }
            }
            return newart;
        }

        public static char[,] GeneratePartArt(string art)
        {
            int dimension = art.Count(c => c == '/');
            char[,] newart = new char[dimension + 1, dimension + 1];

            int temp = 0;
            for (int i = 0; i <= dimension; i++)
            {
                for (int j = 0; j <= dimension; j++)
                {
                    if (art[temp] == '/')
                        temp++;
                    newart[i, j] = art[temp];
                    temp++;
                }
            }
            return newart;
        }

        public static string Rotate(char[,] art, out char[,] rotated)
        {
            rotated = new char[art.GetLength(0), art.GetLength(1)];
            if(art.GetLength(0) == 2)
            {
                rotated[0, 0] = art[1, 0];
                rotated[0, 1] = art[0, 0];
                rotated[1, 0] = art[1, 1];
                rotated[1, 1] = art[0, 1];
            }
            else
            {
                rotated[0, 0] = art[2, 0];
                rotated[0, 1] = art[1, 0];
                rotated[0, 2] = art[0, 0];
                rotated[1, 0] = art[2, 1];
                rotated[1, 1] = art[1, 1];
                rotated[1, 2] = art[0, 1];
                rotated[2, 0] = art[2, 2];
                rotated[2, 1] = art[1, 2];
                rotated[2, 2] = art[0, 2];
            }
            return TranslateArt(rotated);
        }

        public static string Flip(char[,] art, out char[,] flipped)
        {
            flipped = new char[art.GetLength(0), art.GetLength(1)];
            if (art.GetLength(0) == 2)
            {
                flipped[0, 0] = art[0, 1];
                flipped[0, 1] = art[0, 0];
                flipped[1, 0] = art[1, 1];
                flipped[1, 1] = art[1, 0];
            }
            else
            {
                flipped[0, 0] = art[0, 2];
                flipped[0, 1] = art[0, 1];
                flipped[0, 2] = art[0, 0];
                flipped[1, 0] = art[1, 2];
                flipped[1, 1] = art[1, 1];
                flipped[1, 2] = art[1, 0];
                flipped[2, 0] = art[2, 2];
                flipped[2, 1] = art[2, 1];
                flipped[2, 2] = art[2, 0];
            }
            return TranslateArt(flipped);
        }
    }
}
