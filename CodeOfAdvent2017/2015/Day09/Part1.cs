using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Utils;

namespace AdventOfCode._2015.Day09
{
    class Part1
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("2015\\Day09\\Input\\input.txt");
            List<City> cities = new List<City>();
            foreach (string route in input)
            {
                string[] parts = route.Split(' ');
                int distance = Int32.Parse(parts[4]);
                City city1 = new City(parts[0]);
                City city2 = new City(parts[2]);

                city1.AddNeighbour(city2, distance);
                city2.AddNeighbour(city1, distance);

                if (!cities.Exists(c => c.name == city1.name))
                    cities.Add(city1);
                else
                {
                    City city = cities.First(c => c.name == city1.name);
                    city.AddNeighbour(city2, distance);
                }

                if (!cities.Exists(c => c.name == city2.name))
                    cities.Add(city2);
                else
                {
                    City city = cities.First(c => c.name == city2.name);
                    city.AddNeighbour(city1, distance);
                }
            }
            AdventOfCode.Utils.Graphs.Algorithms algs = new Utils.Graphs.Algorithms();
            var permutations = algs.Permutate<City>(cities, cities.Count);
            int minDistance = Int32.MaxValue;
            int maxDistance = Int32.MinValue;
            foreach (var permutation in permutations)
            {
                int distance = 0;
                for(int i = 0; i < permutation.Count(); i++)
                {
                    City city = permutation.ElementAt(i);
                    if(i != permutation.Count() - 1)
                        distance += city.distances[permutation.ElementAt(i + 1).name];
                    string padd = i != permutation.Count() - 1 ? " -> " : "";
                    Console.Write(city.name + padd);
                }
                if (distance < minDistance)
                    minDistance = distance;
                if (distance > maxDistance)
                    maxDistance = distance;
                Console.Write(" (" + distance + ")");
                Console.WriteLine();
            }

            Console.WriteLine("Minimum distance = " + minDistance);
            Console.WriteLine("Maximum distance = " + maxDistance);
            Console.ReadLine();
        }

        internal class City
        {
            public string name;
            public Dictionary<string, int> distances;

            public City(string name)
            {
                this.name = name;
                distances = new Dictionary<string, int>();
            }

            public void AddNeighbour(City city, int distance)
            {
                if (!distances.ContainsKey(city.name))
                    distances.Add(city.name, distance);
            }
        }
    }
}
