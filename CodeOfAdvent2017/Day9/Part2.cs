using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent2017.Day9
{
    class Part2
    {
        static void Main()
        {
            string input = File.ReadAllText("Day9\\Input\\input.txt");
            char[] chars = input.ToCharArray();
            int score = 0;
            int level = 0;
            int removed = 0;
            bool garbage = false;
            bool ignore = false;
            foreach (char c in chars)
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
                        else if (c == '!')
                            ignore = true;
                    }
                    else
                    {
                        if (c == '!')
                            ignore = true;
                        else if (c == '>')
                            garbage = false;
                        else
                            removed++;
                    }
                }
                else
                    ignore = false;
            }
            Console.WriteLine(score);
            Console.WriteLine(removed);
            Console.ReadLine();
        }
    }
}
