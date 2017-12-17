using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode2017.Day16
{

    class Part1
    {
        static char[] programs = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
        [STAThread]
        static void Main()
        {
            string input = File.ReadAllText("Day16\\Input\\input.txt");
            string copy = input;
            string[] danceMoves = copy.Split(',');
            foreach (string move in danceMoves)
            {
                char typeOfMove = move[0];
                string partners = move.Remove(0, 1);
                string[] moves = partners.Split('/');

                if (typeOfMove == 's')
                    Spin(Int32.Parse(moves[0]));
                else if (typeOfMove == 'x')
                    Exchange(Int32.Parse(moves[0]), Int32.Parse(moves[1]));
                else if (typeOfMove == 'p')
                    Partner(moves[0], moves[1]);
                else
                    Console.WriteLine("Unknown dance move!");
            }


            string result = "";
            foreach (char c in programs)
                result += c;

            Console.WriteLine(result);
            Clipboard.SetText(result);
            Console.ReadLine();
        }

        public static void Partner(string program1, string program2)
        {
            int posProgram1 = 0;
            int posProgram2 = 0;
            for (int i = 0; i < programs.Length; i++)
            {
                if (programs[i] == program1[0])
                    posProgram1 = i;
                if (programs[i] == program2[0])
                    posProgram2 = i;
            }
            Exchange(posProgram1, posProgram2);
        }

        public static void Exchange(int pos1, int pos2)
        {
            char temp = programs[pos1];
            programs[pos1] = programs[pos2];
            programs[pos2] = temp;
        }

        public static void Spin(int numberOfPrograms)
        {
            if (numberOfPrograms > programs.Length)
                throw new FormatException("WTF!");

            List<char> temp = new List<char>();
            int i = programs.Length - 1;
            int loop = numberOfPrograms;
            while (loop != 0)
            {
                temp.Insert(0, programs[i]);
                loop--;
                i--;
            }

            for (int j = 0; j < programs.Length - numberOfPrograms; j++)
            {
                temp.Add(programs[j]);
            }
            programs = temp.ToArray();
        }
    }
}
