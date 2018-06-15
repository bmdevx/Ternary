using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ternary;
using Ternary.Components;
using Ternary.Components.Adders;

namespace Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            HalfAdder halfAdder = new HalfAdder();

            halfAdder.CarryOutput += (s, t) =>
            {
                Console.WriteLine($"  Carry: {t.ToSymbol()}");
            };

            halfAdder.SumOutput += (s, t) =>
            {
                Console.WriteLine($"  Sum: {t.ToSymbol()}");
            };
            

            while (true)
            {
                Console.Write("Input A: ");
                if (TritEx.TryParse(Console.ReadLine(), out Trit trit))
                {
                    halfAdder.InputA(null, trit);
                }
                else
                    break;

                Console.Write("Input B: ");
                if (TritEx.TryParse(Console.ReadLine(), out trit))
                {
                    halfAdder.InputB(null, trit);
                    Console.WriteLine();
                }
                else
                    break;
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
    }
}
