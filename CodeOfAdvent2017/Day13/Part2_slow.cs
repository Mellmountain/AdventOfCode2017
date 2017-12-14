using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day13
{
    class Part2_slow
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day13\\Input\\input.txt");
            int delay = 0;
            while (true)
            {                
                List<Layer> firewall = Part1.SetupFirewallLayers(input);
                SetupFireWall(firewall, delay);

                if (SendPacketThroughFirewall(firewall))
                    break;
                delay++;
            }
            Console.WriteLine("Delay: " + delay);
            Console.ReadLine();
        }
        
        /// <summary>
        /// The time it takes to set up FW gets bad when delay gets large
        /// </summary>        
        private static void SetupFireWall(List<Layer> firewall, int delay)
        {
            while(delay != 0)
            {
                foreach(Layer layer in firewall)
                {
                    layer.MoveScanner();
                }
                delay--;
            }
        }

        private static bool SendPacketThroughFirewall(List<Layer> firewall)
        {
            for (int packetDepth = 0; packetDepth <= firewall.Last().depth; packetDepth++)
            {
                foreach (Layer layer in firewall)
                {
                    if (layer.ThreatDetected(packetDepth))
                        return false;
                    layer.MoveScanner();
                }
            }

            return true;
        }
    }
}
