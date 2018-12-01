using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CodeOfAdvent2017.Day20
{
    class Part2
    {
        static void Main()
        {
            List<Particle> particles = new List<Particle>();
            string[] input = File.ReadAllLines("Day20\\Input\\input.txt");

            for (int i = 0; i < input.Length; i++)
            {
                string formatted = "";
                formatted = input[i].Replace('p', ' ');
                formatted = formatted.Replace("a", String.Empty);
                formatted = formatted.Replace("v", String.Empty);
                formatted = formatted.Replace("=", String.Empty);
                formatted = formatted.Replace("<", String.Empty);
                formatted = formatted.Replace(">", String.Empty);
                string[] parts = formatted.Split(',');

                if (parts.Length != 9)
                    throw new Exception("Bad formatted point detected!");

                Point3D position = Parse3DPoint(parts[0], parts[1], parts[2]);
                Point3D velocity = Parse3DPoint(parts[3], parts[4], parts[5]);
                Point3D acceleration = Parse3DPoint(parts[6], parts[7], parts[8]);

                particles.Add(new Particle(i, position, velocity, acceleration));
            }
            
            List<Particle> outOfBounds = new List<Particle>();
            List<Particle> collided = new List<Particle>();

            int ticks = 0;
            int startValue = particles.Count();
            int removed = 0;
            int destroyed = 0;
            while (particles.Count > 0) /* Run until all is *out-of-scope* or destroyed */
            {
                foreach (Particle particle in particles)
                {
                    if (!particle.Tick())
                        outOfBounds.Add(particle);
                }

                foreach (Particle particle in outOfBounds)
                {
                    particles.Remove(particle);
                    removed++;
                }

                outOfBounds.Clear();
                
                foreach(Particle particle in particles)
                {
                    List<Particle> dead = particles.FindAll(part => part.Collided(particle));
                    foreach (Particle dest in dead)
                    {
                        if(!collided.Contains(dest))
                            collided.Add(dest);
                    }
                }

                foreach(Particle particle in collided)
                {
                    Console.WriteLine("----- Particle {" + particle.id + "} destroyed -----");
                    particles.Remove(particle);
                }

                destroyed += collided.Count;
                collided.Clear();

                if (particles.Count == 0)
                    break;

                Particle closestToZero = particles.OrderBy(part => part.distanceToZero).First();
                Console.WriteLine("Closest to zero = " + closestToZero.id + " tick: " + ticks + " count (" + particles.Count + "/" + startValue + ")");

                ticks++;
            }
            Console.WriteLine("Done...");
            Console.WriteLine("Particles left: " + removed + " Particles destroyed: " + destroyed);
            Console.ReadLine();
        }

        private static Point3D Parse3DPoint(string strX, string strY, string strZ)
        {
            int x, y, z;
            x = Int32.Parse(strX);
            y = Int32.Parse(strY);
            z = Int32.Parse(strZ);
            return new Point3D((double)x, (double)y, (double)z);
        }
    }
}