using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day13
{
    class Part2
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day13\\Input\\input.txt");
            int delay = 0;
            List<Layer> firewall = new List<Layer>();
            firewall = Part1.SetupFirewallLayers(input);

            while (true)
            {
                List<Layer> new_state;
                if (delay > 0)
                    new_state = GetNextFirewallState(ref firewall);
                else
                    new_state = firewall.ConvertAll(layer => new Layer(layer.depth, layer.range, layer.scannerDirectionDown, layer.scannerPosition));

                /* fw state should be copied here since SendPackageThroughFW change it */
                if (SendPacketThroughFirewall(ref new_state))
                    break;
                delay++;
            }
            Console.WriteLine("Delay: " + delay);
            Console.ReadLine();
        }


        private static List<Layer> GetNextFirewallState(ref List<Layer> firewall)
        {
            foreach (Layer layer in firewall)
            {
                layer.MoveScanner();
            }

            return firewall.ConvertAll(layer => new Layer(layer.depth, layer.range, layer.scannerDirectionDown, layer.scannerPosition));
        }

        private static bool SendPacketThroughFirewall(ref List<Layer> firewall)
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
