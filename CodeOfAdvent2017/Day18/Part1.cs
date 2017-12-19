using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day18
{

    class Part1
    {
        static Dictionary<string, Int64> registers = new Dictionary<string, Int64>();
        static void Main()
        {
            registers.Add("a", 0);
            registers.Add("b", 0);
            registers.Add("f", 0);
            registers.Add("i", 0);
            registers.Add("p", 0);
            string[] instructions = File.ReadAllLines("Day18\\Input\\input.txt");
            bool iModified;
            for (int i = 0; i < instructions.Length;)
            {
                iModified = false;
                string[] parts = instructions[i].Split(' ');
                string command = parts[0];

                if (command == "snd")
                {
                    PlaySound(parts[1]);
                }
                else if (command == "set")
                {
                    string register = parts[1];
                    string value = parts[2];
                    SetValue(register, value);
                }
                else if (command == "add" || command == "mul" || command == "mod")
                {
                    string register = parts[1];
                    string value = parts[2];
                    Operation(command, register, value);
                }
                else if (command == "rcv")
                {
                    if (registers[parts[1]] != 0)
                    {
                        PlayLastSound(parts[1]);
                    }
                        
                }
                else if (command == "jgz")
                {
                    int condition = 0;
                    if (!Int32.TryParse(parts[1], out condition))
                        condition = (int)registers[parts[1]];

                    if (condition > 0)
                    {
                        int value = 0;
                        if (Int32.TryParse(parts[2], out value))
                            i += value;
                        else
                            i += (int)registers[parts[2]];
                        iModified = true;
                    }
                }
                if (!iModified)
                {
                    i++;
                    iModified = false;
                }
            }
            Console.ReadLine();
        }

        private static void PlaySound(string register)
        {
            if (!registers.ContainsKey("lastsound"))
                registers.Add("lastsound", registers[register]);
            else
                registers["lastsound"] = registers[register];
            Console.WriteLine("Played sound from register " + register + " FREQ: " + registers[register]);
        }

        private static void SetValue(string register, string value)
        {
            int ivalue = 0;
            bool isRegister = true;
            if (Int32.TryParse(value, out ivalue))
                isRegister = false;

            if (isRegister)
            {
                registers[register] = registers[value];
            }
            else
                registers[register] = ivalue;
        }

        private static void Operation(string command, string register, string value)
        {
            int ivalue = 0;
            bool isRegister = true;
            if (Int32.TryParse(value, out ivalue))
                isRegister = false;

            switch (command)
            {
                case "add":
                    {
                        if (isRegister)
                            registers[register] += registers[value];
                        else
                            registers[register] += ivalue;
                        break;
                    }
                case "mul":
                    {
                        if (isRegister)
                            registers[register] *= registers[value];
                        else
                            registers[register] *= ivalue;
                        break;
                    }
                case "mod":
                    {
                        if (isRegister)
                            registers[register] %= registers[value];
                        else
                            registers[register] %= ivalue;
                        break;
                    }
                default:
                    Console.WriteLine("Unkown operation!");
                    break;
            }
        }

        private static void PlayLastSound(string register)
        {
            Console.WriteLine("LastSound: " + registers["lastsound"]);        
        }
    }
}
        