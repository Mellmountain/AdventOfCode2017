using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day13
{
    class Part1
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("Day13\\Input\\input.txt");
            List<Layer> firewall = SetupFirewallLayers(input);

            int severity = 0;
            for(int packetDepth = 0; packetDepth <= firewall.Last().depth; packetDepth++)
            {
                foreach(Layer layer in firewall)
                {
                    if (layer.ThreatDetected(packetDepth))
                        severity += layer.depth * layer.range;
                    layer.MoveScanner();
                }
            }
            Console.WriteLine("Severity: " + severity);
            Console.ReadLine();
        }

        public static List<Layer> SetupFirewallLayers(string[] input)
        {
            List<Layer> firewall = new List<Layer>();
            foreach(string layer in input)
            {
                string[] parts = layer.Split(':');
                firewall.Add(new Layer(Int32.Parse(parts[0]), Int32.Parse(parts[1])));
            }
            return firewall;
        }
    }

    class Layer
    {
        public int depth;
        public int range;
        public int scannerPosition;
        public bool scannerDirectionDown;

        public Layer(int depth, int range)
        {
            this.range = range;
            this.depth = depth;
            scannerDirectionDown = true;
            scannerPosition = 0;
        }

        public Layer(int depth, int range, bool scannerDirection, int scannerPos)
        {
            this.depth = depth;
            this.range = range;
            this.scannerDirectionDown = scannerDirection;
            this.scannerPosition = scannerPos;
        }

        public bool ThreatDetected(int packetDepth)
        {
            return packetDepth == depth && scannerPosition == 0;
        }
        
        public void MoveScanner()
        {
            if (scannerPosition == 0 && !scannerDirectionDown)
                scannerDirectionDown = true;
            if (scannerPosition == range - 1 && scannerDirectionDown)
                scannerDirectionDown = false;
            if (scannerDirectionDown)
                scannerPosition += 1;
            else
                scannerPosition -= 1;
        }

        public void SetScannerPos(int pos)
        {
            scannerPosition = pos;
        }
    }
}
/*
List<Layer> firewall = SetupFirewallLayers(input)

For(int I = 0; I < firewall.Count; i++)
{
                packetPosition = Packet.Move()
                foreach(layer in firewall)  but not safe layers. Layer.depth == 1 is safe
                                {
                                                if(Firewall.Scan(packetPosition))
                                                                severity += layer.Depth* layer.Range
                                                Firewall.Move()
                                }
}

Hur får man scannern att gå fram och tillbaka I en array? Måste nästan bygga ett layer objekt som håller reda på vilket håll scannern går?

Skriv ett Layer objekt som du lägger i en List<Layer> firewall.Layer håller reda på om paketet är fångat samt sköter scannern och dess riktning.

Typ….
Class Layer
{
    Int layer;
    Int depth


Int scannerPosition
                bool scanner


Scan(packetPos)
                                Bool caught = scannerPos == packetpos
                                If(scanDirectionUp)
                                                scanner

                LayerSafe()
                                Return depth == 0;
}

*/