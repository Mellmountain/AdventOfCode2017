using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day09
{
    /// <summary>
    /// http://adventofcode.com/2017/day/9
    /// --- Day 9: Stream Processing ---
    /// </summary>
    class Part1
    {
        static void Main()
        {
            string input = File.ReadAllText("Day9\\Input\\input.txt");
            char[] chars = input.ToCharArray();
            int score = 0;
            int level = 0;
            bool garbage = false;
            bool ignore = false;
            foreach(char c in chars)
            {
                if (!ignore)
                {
                    if (!garbage)
                    {
                        if (c == '{')
                        {
                            level++;
                            score += level;
                        }
                        else if (c == '}')
                            level--;
                        else if (c == '<')
                            garbage = true;
                    }
                    else
                    {
                        if (c == '!')
                            ignore = true;
                        else if(c == '>')  
                            garbage = false;
                    }
                }
                else
                    ignore = false;
            }
            Console.WriteLine(score);
            Console.ReadLine();
        }

    }
}
