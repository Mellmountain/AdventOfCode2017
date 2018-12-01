using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CodeOfAdvent2017.Day20
{
    class Part1
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

            //Run simulation for 10000 ticks
            int ticks = 0;
            int startValue = particles.Count();
            List<Particle> outOfBounds = new List<Particle>();
            while (particles.Count > 0)
            {
                foreach (Particle particle in particles)
                {
                    if (!particle.Tick())
                        outOfBounds.Add(particle);
                }

                foreach (Particle particle in outOfBounds)
                {
                    particles.Remove(particle);
                }

                outOfBounds.Clear();
                if (particles.Count == 0)
                    break;

                Particle closestToZero = particles.OrderBy(part => part.distanceToZero).First();
                Console.WriteLine("Closest to zero = " + closestToZero.id + " tick: " + ticks + " count (" + particles.Count + "/" + startValue + ")");
                ticks++;
            }
            Console.WriteLine("Done...");
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

    class Particle
    {
        private Point3D position;
        private Point3D acceleration;
        private Point3D velocity;

        public int distanceToZero = 0;
        public int id;

        public Particle(int id, Point3D pos, Point3D vel, Point3D acc)
        {
            this.id = id;
            this.position = pos;
            this.acceleration = acc;
            this.velocity = vel;
        }

        public bool Tick()
        {
            velocity.X += acceleration.X;
            velocity.Y += acceleration.Y;
            velocity.Z += acceleration.Z;

            if (!Validate(velocity))
                return false;

            position.X += velocity.X;
            position.Y += velocity.Y;
            position.Z += velocity.Z;
            if (!Validate(position))
                return false;


            distanceToZero = (int)(Math.Abs(position.X) + Math.Abs(position.Y) + Math.Abs(position.Z));
            return true;
        }

        private bool Validate(Point3D point3d)
        {
            if (point3d.X <= Int32.MinValue || point3d.X >= Int32.MaxValue)
                return false;
            if (point3d.Y <= Int32.MinValue || point3d.Y >= Int32.MaxValue)
                return false;
            if (point3d.Z <= Int32.MinValue || point3d.Z >= Int32.MaxValue)
                return false;
            return true;
        }

        internal bool Collided(Particle particle2)
        {
            return this.position.X == particle2.position.X &&
                this.position.Y == particle2.position.Y &&
                this.position.Z == particle2.position.Z &&
                this.id != particle2.id;
        }
    }
}