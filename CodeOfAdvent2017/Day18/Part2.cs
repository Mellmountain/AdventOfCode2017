using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day18
{
    class Part2
    {
        static void Main()
        {
            AutoResetEvent autoRest = new AutoResetEvent(false);
            string[] instructions = File.ReadAllLines("Day18\\Input\\input.txt");
            AOCProgram program0 = new AOCProgram(0, instructions, autoRest);
            AOCProgram program1 = new AOCProgram(1, instructions, autoRest);
            program0.SetPartner(program1);
            program1.SetPartner(program0);

            program0.StartAOCProgram();
            program1.StartAOCProgram();

            while (true)
            {
                if (program0.thread.ThreadState == ThreadState.WaitSleepJoin &&
                    program1.thread.ThreadState == ThreadState.WaitSleepJoin)
                {
                    Console.WriteLine("Both program done/deadlocked... exit");
                    break;
                }
                else
                {
                    Console.WriteLine("MasterThread: Check threads again in 5s");
                    Thread.Sleep(5000);
                }
            }
            Console.WriteLine("Program1, sendcount: " + program1.sendCount);
            Console.WriteLine("Program0, sendcount: " + program0.sendCount);
            Console.WriteLine();
        }
    }

    class AOCProgram
    {
        ConcurrentQueue<long> queue;
        Dictionary<string, Int64> registers;
        AutoResetEvent autoRest;
        string[] instructions;
        int programId;
        public int sendCount = 0;
        

        AOCProgram partner = null;
        public Thread thread;

        public AOCProgram(int programID, string[] instructions, AutoResetEvent autoRest)
        {
            queue = new ConcurrentQueue<long>();
            this.autoRest = autoRest;
            this.instructions = instructions;
            registers = new Dictionary<string, long>();
            registers.Add("a", 0);
            registers.Add("b", 0);
            registers.Add("f", 0);
            registers.Add("i", 0);
            registers.Add("p", programID);
            programId = programID;

            thread = new Thread(new ThreadStart(RunAOCProgram));
            thread.Name = "program" + programID;
        }

        public void StartAOCProgram()
        {
            if(thread != null)
                thread.Start();
        }

        private void RunAOCProgram()
        {
            bool iModified;
            for (int i = 0; i < instructions.Length;)
            {
                iModified = false;
                string[] parts = instructions[i].Split(' ');
                string command = parts[0];

                if (command == "snd")
                {
                    if (partner != null)
                    {
                        /* send register value or value? */
                        int value = 0;
                        if (!Int32.TryParse(parts[1], out value))
                            SendMessage(registers[parts[1]]);
                        else
                            SendMessage((long)value);
                    }
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
                    Console.WriteLine("Program " + programId + " rcv " + parts[1]);
                    long data;
                    while (!queue.TryDequeue(out data))
                    {
                        Console.WriteLine("Failed to dequeue!!");
                        Console.WriteLine("Program " + programId + " waiting to rcv");
                        autoRest.WaitOne();
                    }
                    registers[parts[1]] = data;
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
        }

        public void SendMessage(long message)
        {
            Console.WriteLine("Program " + programId + " sends " + message);
            partner.queue.Enqueue(message);
            sendCount++;
            autoRest.Set();

        }

        public void SetPartner(AOCProgram program)
        {
            this.partner = program;
        }

        private void SetValue(string register, string value)
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

        private void Operation(string command, string register, string value)
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
    }
}
