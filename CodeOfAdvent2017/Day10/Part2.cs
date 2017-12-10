using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeOfAdvent2017.Day10
{
    class Part2
    {
        [STAThread]
        static void Main()
        {
            int[] sparseHash = new int[256];
            for (int i = 0; i < sparseHash.Length; i++)
                sparseHash[i] = i;

            string data = File.ReadAllText("Day10\\Input\\input.txt");
            string[] input = data.Split(',');
            
            List<int> lengths = EncodeInputToASCII(input);

            /* run 64 rounds */
            int currentPosition = 0;
            int skipSize = 0;
            for(int i = 0; i < 64; i++)
            {
                int[] arrLengths = lengths.ToArray();
                Part1.KnotHash(ref sparseHash, ref arrLengths, skipSize, currentPosition,
                    out currentPosition, out skipSize);
            }

            int[] denseHash = GenerateDenseHash(sparseHash);

            string hexhash = "";
            foreach(int part in denseHash)
            {
                if (part < 10)
                    hexhash += "0";
                hexhash += part.ToString("x");
            }

            Console.WriteLine(hexhash);
            Console.WriteLine(hexhash.Length);
            Clipboard.SetText(hexhash);
            Console.ReadLine();
        }

        private static int[] GenerateDenseHash(int[] sparseHash)
        {
            int[] denseHash = new int[16];
            for (int j = 0; j < 16; j++)            
                for (int i = j * 16; i < 16 + j * 16; i++)
                    denseHash[j] ^= sparseHash[i];
            return denseHash;
        }

        private static List<int> EncodeInputToASCII(string[] input)
        {
            List<int> lengths = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input[i]);
                foreach (byte b in bytes)
                {
                    lengths.Add((int)b);
                }
                if (i + 1 < input.Length)
                    lengths.Add(44); /* ',' */

            }
            /* add the suffix lengths */
            lengths.Add(17);
            lengths.Add(31);
            lengths.Add(73);
            lengths.Add(47);
            lengths.Add(23);
            return lengths;
        }
    }
}
