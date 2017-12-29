using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day23
{
    class Part1
    {
        static Dictionary<string, Int64> registers = new Dictionary<string, Int64>();
        static void Main()
        {
            registers.Add("a", 0);
            registers.Add("b", 0);
            registers.Add("c", 0);
            registers.Add("d", 0);
            registers.Add("e", 0);
            registers.Add("f", 0);
            registers.Add("g", 0);
            registers.Add("h", 0);

            string[] instructions = File.ReadAllLines("Day23\\Input\\input.txt");
            int multiplied = 0;
            bool iModified;
            for (int i = 0; i < instructions.Length;)
            {
                iModified = false;
                string[] parts = instructions[i].Split(' ');
                string command = parts[0];

                if (command == "set")
                {
                    string register = parts[1];
                    string value = parts[2];
                    SetValue(register, value);
                }
                else if (command == "sub")
                {
                    string register = parts[1];
                    string value = parts[2];
                    Operation(command, register, value);
                }
                else if (command == "mul")
                {
                    string register = parts[1];
                    string value = parts[2];
                    Operation(command, register, value);
                    multiplied++;
                }
                
                else if (command == "jnz")
                {
                    int condition = 0;
                    if (!Int32.TryParse(parts[1], out condition))
                        condition = (int)registers[parts[1]];

                    if (condition != 0)
                    {
                        int value = 0;
                        if (Int32.TryParse(parts[2], out value))
                            i += value;
                        else
                            i += (int)registers[parts[2]];
                        iModified = true;
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command!");
                }
                if (!iModified)
                {
                    i++;
                    iModified = false;
                }
            }
            Console.WriteLine(multiplied);
            Console.ReadLine();
        }

        private static void Operation(string command, string register, string value)
        {
            int ivalue = 0;
            bool isRegister = true;
            if (Int32.TryParse(value, out ivalue))
                isRegister = false;

            switch (command)
            {
                case "sub":
                    {
                        if (isRegister)
                            registers[register] -= registers[value];
                        else
                            registers[register] -= ivalue;
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
                default:
                    Console.WriteLine("Unkown operation!");
                    break;
            }
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
    }

    

    

}

