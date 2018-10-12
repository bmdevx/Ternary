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
            ALU alu = new ALU();
            alu.BusOutput += (s, t) =>
            {
                Console.WriteLine($"{t.ToString()} \tSigned:{alu.SignedState}");
            };

            //alu.SignedOutput += (s, t) =>
            //{
            //    Console.WriteLine($"Signed State: {t.ToString()}");
            //};

            while (true)
            {
                Action<Tryte> DisplayPrev = (t) =>
                {
                    Console.SetCursorPosition(18, Console.CursorTop - 1);
                    Console.WriteLine($" {t}");
                };


                Console.Write("Trit S: ");
                if (!TritEx.TryParse(Console.ReadLine(), out Trit to))
                    break;

                Console.Write("Tryte A: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte ta))
                    break;

                DisplayPrev(ta);

                Console.Write("Tryte B: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte tb))
                    break;

                DisplayPrev(tb);


                Console.WriteLine();

                alu.ABusInput(null, ta);
                alu.BBusInput(null, tb);
                alu.FowleanControlInput(null, to);


                Console.WriteLine();
            }


            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
    }
}
