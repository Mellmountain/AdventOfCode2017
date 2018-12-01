using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day23
{
class Part2
    {
        static void Main()
        {
            /*  asm instructions above translate to something like this.. 
                we check if d * e = b up to c and if we find out that b can be written
                as the product of two numbers we increment h by one.
                So we are basically counting the number of none prime between b and c by 
                increments of 17 by b.
            */
            //int a = 1, b = 65, c = 0, d = 0, e = 0, f = 0, g = 0, h = 0;
            //if (a != 0)
            //{
            //    b *= 100 + 100000;
            //    c = b + 17000;
            //}
            //while (true)
            //{
            //    f = 1;
            //    d = 2;
            //    while (d != b)
            //    {
            //        e = 2;
            //        while (e != b)
            //        {
            //            g = d * e - b;
            //            if (g == 0)
            //                f = 0;
            //            e++;
            //        }
            //        d++;
            //    }
            //    if (f == 0)
            //        h++;
            //    if (b == c)
            //        break;
            //    b += 17;
            //}
            int a = 1, b = 65, c = 0, h = 0;
            if (a != 0)
            {
                b *= 100;
                b += 100000;
                c = b + 17000;
            }
            Console.WriteLine("Primes between " + b + " and " + c + " :");
            while(b <= c)
            {
                if (!isPrime(b))
                {
                    //Console.Write(b + "," );
                    h++;
                }
                b += 17;
            }
            Console.WriteLine(h + " composite numbers");
            Console.WriteLine(1000 - h + " primes");
            Console.ReadLine();
        }
        /* https://en.wikipedia.org/wiki/Primality_test */
        private static bool isPrime(int n)
        {
            if (n <= 1)
                return false;
            if (n <= 3)
                return true;
            if (n % 2 == 0 || n % 3 == 0)
                return false;

            int i = 5;
            while(i * i <= n)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;
                i += 6;
            }
            return true;
        }
    }
}
