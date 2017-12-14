using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day13
{
    class Part2_fast
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day13\\Input\\test.txt");
            List<Layer> firewall = Part1.SetupFirewallLayers(input);
            int time = 0;
            while (true)
            {
                if (SendPacketThroughFirewall(firewall, time))
                    break;
                time++;
            }
            Console.WriteLine("Delay: " + time);
            Console.ReadLine();
        }

        private static bool SendPacketThroughFirewall(List<Layer> firewall, int time)
        {            
            for (int packetDepth = 0; packetDepth <= firewall.Last().depth; packetDepth++)
            {
                Layer layer = firewall.Find(lyr => lyr.depth == packetDepth);
                if (layer == null)
                    continue;
                else
                {
                    layer.SetScannerPos(time + packetDepth % (layer.range * 2 - 2));
                    if (layer.ThreatDetected(packetDepth))
                        return false;
                }
            }


            return true;
        }
    }
}
