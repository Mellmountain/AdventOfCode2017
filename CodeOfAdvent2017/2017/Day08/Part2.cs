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
    class Part2
    {
        static void Main()
        {
            List<Register> registers = new List<Register>();
            string[] input = File.ReadAllLines("Day8\\Input\\input.txt");
            int maxValue = 0;

            foreach (string instruction in input)
            {
                Part1.ParseInstructions(instruction.Split(' '));

                Register conditionRegister = registers.FirstOrDefault(reg => reg.name == Part1.conditionRegisterName);
                Register registerToModify;
                if (registers.Exists(reg => reg.name == Part1.registerToModifyName))
                    registerToModify = registers.Find(reg => reg.name == Part1.registerToModifyName);
                else
                {
                    registerToModify = new Register(Part1.registerToModifyName);
                    registers.Add(registerToModify);
                }

                if (Part1.CondtionIsTrue(conditionRegister, Part1.conditionOperator, Part1.conditionValue))
                    registerToModify.value = Part1.addition ? registerToModify.value + Part1.value : registerToModify.value - Part1.value;

                int localMax = registers.OrderByDescending(reg => reg.value).First().value;
                if (localMax > maxValue)
                    maxValue = localMax;
            }

            Console.WriteLine(maxValue);
            Console.ReadLine();
        }
    }
}
