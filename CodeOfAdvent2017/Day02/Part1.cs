using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent2017.Day2
{
    class Part1
    {
        static void Main()
        {
            int checksum = 0;
            var rows = File.ReadAllLines("Day2\\Input\\input.txt");
            foreach(string row in rows)
            {
                string[] cols = row.Split('\t');
                int lowest = Int32.MaxValue;
                int highest = Int32.MinValue;
                for (int i = 0; i < cols.Length; i++)
                {
                    int value;
                    Int32.TryParse(cols[i], out value);

                    lowest = value < lowest ? value : lowest;
                    highest = value > highest ? value : highest;
                }
                checksum += highest - lowest;
            }

            Console.WriteLine(checksum);
            Console.ReadKey();
        }
    }
}
