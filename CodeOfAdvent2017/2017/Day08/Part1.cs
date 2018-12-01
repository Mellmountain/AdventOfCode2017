using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day08
{
    /// <summary>
    /// http://adventofcode.com/2017/day/8
    /// --- Day 8: I Heard You Like Registers ---
    /// </summary>
    class Part1
    {
        public static string registerToModifyName;
        public static bool addition;
        public static int value;
        public static string conditionRegisterName;
        public static string conditionOperator;
        public static string conditionValue;

        static void Main()
        {
            List<Register> registers = new List<Register>();
            string[] input = File.ReadAllLines("Day8\\Input\\input.txt");

            foreach (string instruction in input)
            {
                ParseInstructions(instruction.Split(' '));

                Register conditionRegister = registers.FirstOrDefault(reg => reg.name == conditionRegisterName);
                Register registerToModify;

                if (registers.Exists(reg => reg.name == registerToModifyName))
                    registerToModify = registers.Find(reg => reg.name == registerToModifyName);
                else
                {
                    registerToModify = new Register(registerToModifyName);
                    registers.Add(registerToModify);
                }

                if (CondtionIsTrue(conditionRegister, conditionOperator, conditionValue))
                    registerToModify.value = addition ? registerToModify.value + value : registerToModify.value - value;
            }

            Console.WriteLine(registers.OrderByDescending(reg => reg.value).First().value);
            Console.ReadLine();
        }

        public static void ParseInstructions(string[] parts)
        {
            registerToModifyName = parts[0];
            addition = parts[1] == "inc" ? true : false;
            value = Int32.Parse(parts[2]);
            conditionRegisterName = parts[4];
            conditionOperator = parts[5];
            conditionValue = parts[6];

        }

        public static bool CondtionIsTrue(Register conditionRegister, string conditionOperator, string conditionValue)
        {
            int value1 = conditionRegister != null ? conditionRegister.value : 0;
            int value2 = Int32.Parse(conditionValue);
            switch (conditionOperator)
            {
                case "<":
                    return value1 < value2;
                case ">":
                    return value1 > value2;
                case "<=":
                    return value1 <= value2;
                case ">=":
                    return value1 >= value2;
                case "!=":
                    return value1 != value2;
                case "==":
                    return value1 == value2;
                default:
                    {
                        Console.WriteLine("Unkown operation!!!");
                        return false;
                    }
            }
        }
    }

    class Register
    {
        public string name;
        public int value;

        public Register(string name)
        {
            this.name = name;
            this.value = 0;
        }
    }
}
