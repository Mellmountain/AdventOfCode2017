using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Day14
{
    class Part1
    {
        static void Main()
        {
            List<Reindeer> stable = new List<Reindeer>();
            bool test = false;
            int ticks = 0;
            int finish = 0;
            if (!test)
            {
                stable.Add(new Reindeer("Rudolph", 22, 8, 165));
                stable.Add(new Reindeer("Cupid", 8, 17, 114));
                stable.Add(new Reindeer("Prancer", 18, 6, 103));
                stable.Add(new Reindeer("Donner", 25, 6, 145));
                stable.Add(new Reindeer("Dasher", 11, 12, 125));
                stable.Add(new Reindeer("Comet", 21, 6, 121));
                stable.Add(new Reindeer("Blitzen", 18, 3, 50));
                stable.Add(new Reindeer("Vixen", 20, 4, 75));
                stable.Add(new Reindeer("Dancer", 7, 20, 119));
                finish = 2503;
            }
            else
            {
                stable.Add(new Reindeer("Comet", 14, 10, 127));
                stable.Add(new Reindeer("Dancer", 16, 11, 162));
                finish = 1000;
            }
            
            while(ticks < finish)
            {
                foreach (Reindeer deer in stable)
                    deer.tick();
                int maxdist = stable.Max(d => d.distance);
                foreach (Reindeer deer in stable.FindAll(d => d.distance == maxdist))
                    deer.score += 1;
                ticks++;
            }

            foreach (Reindeer deer in stable)
                Console.WriteLine(deer.name + "[" + deer.resting + "] (" + deer.distance + ") ** " + deer.score + " **");

            Reindeer scoreWinner = stable.First(d => d.score == stable.Max(e => e.score));
            Reindeer distWinner = stable.First(d => d.distance == stable.Max(e => e.distance));
            Console.WriteLine("Winner by score: " + scoreWinner.name + " **" + scoreWinner.score + " **");
            Console.WriteLine("Winner by distance: " + distWinner.name + " (" + distWinner.distance + "km)");
            Console.ReadLine();
        }

        internal class Reindeer
        {
            public string name;
            public int speed;
            public int duration;
            public int rest;
            public int distance;
            private int ticks;
            public bool resting = false;
            public int score = 0;

            public Reindeer(string name, int speed, int duration, int rest)
            {
                this.name = name;
                this.speed = speed;
                this.duration = duration;
                this.rest = rest;
                this.distance = 0;
                this.ticks = 0;
            }

            public void tick()
            {
                if (!resting)
                {
                    if (ticks == duration)
                        resting = true;
                    else
                        distance += speed;
                }
                else
                {
                    if (ticks == (rest + duration))
                    {
                        ticks = 0;
                        resting = false;
                        distance += speed;
                    }

                }
                ticks++;
            }
        }
    }
}
