using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ternary;
using Ternary.Components;
using Ternary.Components.Chips;
using Ternary.Components.Gates;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Tryte 1: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte t1))
                    break;

                Console.Write("Tryte 2: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte t2))
                    break;

                Sum(t1, t2);
                Diff(t1, t2);
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        static void Sum(Tryte t1, Tryte t2)
        {
            Tryte sum = t1 + t2;
            Console.WriteLine($"Sum: [{sum.ToString("s")}] {sum}\n");
        }

        static void Diff(Tryte t1, Tryte t2)
        {
            Tryte diff = t1 - t2;
            Console.WriteLine($"Diff: [{diff.ToString("s")}] {diff}\n");
        }
    }
}
