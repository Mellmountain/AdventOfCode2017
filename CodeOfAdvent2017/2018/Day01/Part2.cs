using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017._2018.Day01
{
    class Part2
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("2018\\Day01\\input\\input.txt");
            int frequency = 0;
            bool searchFrequency = true;
            HashSet<int> frequencies = new HashSet<int>();
            frequencies.Add(frequency);

            while (searchFrequency)
            {
                foreach (string str in input)
                {
                    int change = Int32.Parse(str);
                    frequency += change;

                    if (frequencies.Contains(frequency))
                    {
                        searchFrequency = false;
                        break;
                    }
                    else
                        frequencies.Add(frequency);
                }
            }
            Console.WriteLine("frequency is: " + frequency);
            Console.ReadLine();

        }
    }
}
