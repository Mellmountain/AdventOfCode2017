using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day25
{


    class Part1
    {
        enum State
        {
            A,
            B,
            C,
            D,
            E,
            F
        };
        static void Main()
        {
            //Begin in state A.
            //Perform a diagnostic checksum after 12481997 steps.

            int[] tape = new int[10000000];
            State currentState = State.A;
            int currentPos = 10000000 / 2;

            int steps = 0;
            while (steps < 12481997)
            {
                switch(currentState)
                {
                    //In state A:
                    //  If the current value is 0:
                    //    - Write the value 1.
                    //    - Move one slot to the right.
                    //    - Continue with state B.
                    //  If the current value is 1:
                    //    - Write the value 0.
                    //    - Move one slot to the left.
                    //    - Continue with state C.
                    case State.A:
                        {
                            if (tape[currentPos] == 0)
                            {
                                tape[currentPos] = 1;
                                currentPos += 1;
                                currentState = State.B;
                            } 
                            else
                            {
                                tape[currentPos] = 0;
                                currentPos -= 1;
                                currentState = State.C;
                            }

                        }
                        break;
                    //In state B:
                    //  If the current value is 0:
                    //    - Write the value 1.
                    //    - Move one slot to the left.
                    //    - Continue with state A.
                    //  If the current value is 1:
                    //    - Write the value 1.
                    //    - Move one slot to the right.
                    //    - Continue with state D.
                    case State.B:
                        {
                            if (tape[currentPos] == 0)
                            {
                                tape[currentPos] = 1;
                                currentPos -= 1;
                                currentState = State.A;
                            }
                            else
                            {
                                currentPos += 1;
                                currentState = State.D;
                            }
                        }
                        break;
                    //In state C:
                    //  If the current value is 0:
                    //    - Write the value 0.
                    //    - Move one slot to the left.
                    //    - Continue with state B.
                    //  If the current value is 1:
                    //    - Write the value 0.
                    //    - Move one slot to the left.
                    //    - Continue with state E.
                    case State.C:
                        {
                            if (tape[currentPos] == 0)
                            {
                                currentPos -= 1;
                                currentState = State.B;
                            }
                            else
                            {
                                tape[currentPos] = 0;
                                currentPos -= 1;
                                currentState = State.E;
                            }
                        }
                        break;
                    //In state D:
                    //  If the current value is 0:
                    //    - Write the value 1.
                    //    - Move one slot to the right.
                    //    - Continue with state A.
                    //  If the current value is 1:
                    //    - Write the value 0.
                    //    - Move one slot to the right.
                    //    - Continue with state B.
                    case State.D:
                        {
                            if (tape[currentPos] == 0)
                            {
                                tape[currentPos] = 1;
                                currentPos += 1;
                                currentState = State.A;
                            }
                            else
                            {
                                tape[currentPos] = 0;
                                currentPos += 1;
                                currentState = State.B;
                            }
                        }
                        break;
                    //In state E:
                    //  If the current value is 0:
                    //    - Write the value 1.
                    //    - Move one slot to the left.
                    //    - Continue with state F.
                    //  If the current value is 1:
                    //    - Write the value 1.
                    //    - Move one slot to the left.
                    //    - Continue with state C.

                    case State.E:
                        {
                            if (tape[currentPos] == 0)
                            {
                                tape[currentPos] = 1;
                                currentPos -= 1;
                                currentState = State.F;
                            }
                            else
                            {
                                currentPos -= 1;
                                currentState = State.C;
                            }
                        }
                        break;
                    //In state F:
                    //  If the current value is 0:
                    //    - Write the value 1.
                    //    - Move one slot to the right.
                    //    - Continue with state D.
                    //  If the current value is 1:
                    //    - Write the value 1.
                    //    - Move one slot to the right.
                    //    - Continue with state A.
                    case State.F:
                        {
                            if (tape[currentPos] == 0)
                            {
                                tape[currentPos] = 1;
                                currentPos += 1;
                                currentState = State.D;
                            }
                            else
                            {
                                currentPos += 1;
                                currentState = State.A;
                            }
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Unkown state!");
                        }
                        break;
                }
                steps++;
            }
            int checksum = 0;
            for(int i = 0; i < tape.Length; i++)
            {
                checksum += tape[i] == 1 ? 1 : 0;
            }

            Console.WriteLine(checksum);
            Console.ReadLine();
        }
    }
}
