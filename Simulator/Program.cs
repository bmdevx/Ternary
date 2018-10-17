using System;
using Ternary;
using Ternary.Components;

namespace Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            TryteALU alu = new TryteALU ();
            alu.BusOutput += (s, t) =>
            {
                Console.WriteLine($"[{t.ToString("s")}] {t}");
            };
            
            while (true)
            {
                Action<Tryte, int> DisplayPrevTryte = (t, offset) =>
                {
                    Console.SetCursorPosition(offset, Console.CursorTop - 1);
                    Console.WriteLine($"{t.ToString("s")} {t}");
                };


                Console.Write("Trit S: ");
                if (!TritEx.TryParse(Console.ReadLine(), out Trit to))
                    break;

                Console.Write("Tryte A: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte ta))
                    break;

                DisplayPrevTryte(ta, 9);

                Console.Write("Tryte B: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte tb))
                    break;

                DisplayPrevTryte(tb, 9);


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
