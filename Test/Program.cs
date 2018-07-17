using System;
using System.Diagnostics;
using Ternary;
using Ternary.Components.Adders;
using Ternary.Components.Buses.Monadic;

namespace Test
{
    class Program
    {
        static TryteAdder tryteAdder = new TryteAdder(), tryteAdderForDiff = new TryteAdder();
        static InverterBus inverterBus = new InverterBus();


        static void Main(string[] args)
        {
            TryteTest();

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }


        static void TritTest()
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
                    halfAdder.AInput(null, trit);
                }
                else
                    break;

                Console.Write("Input B: ");
                if (TritEx.TryParse(Console.ReadLine(), out trit))
                {
                    halfAdder.BInput(null, trit);
                    Console.WriteLine();
                }
                else
                    break;
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        static void TryteTest()
        {
            tryteAdder.BusOutput += (s, sum) => {
                Console.WriteLine($"Sum: [{sum.ToString("s")}] {sum}");
            };

            tryteAdder.CarryOut += (s, carry) => {
                Console.WriteLine($"Sum Carry: {carry.ToSymbol()}");
            };


            tryteAdderForDiff.BusOutput += (s, sum) => {
                Console.WriteLine($"Diff: [{sum.ToString("s")}] {sum}");
            };

            tryteAdderForDiff.CarryOut += (s, carry) => {
                Console.WriteLine($"Diff Carry: {carry.ToSymbol()}");
            };

            inverterBus.BusOutput += tryteAdderForDiff.BBusInput;



            while (true)
            {
                Console.Write("Tryte 1: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte t1))
                    break;

                DisplayPrev(t1);

                Console.Write("Tryte 2: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte t2))
                    break;

                DisplayPrev(t2);

                Console.WriteLine();

                Sum(t1, t2);
                Diff(t1, t2);
                //Multi(t1, t2);
                //Div(t1, t2);
                //Mod(t1, t2);
                //Greater(t1, t2);
                //Lesser(t1, t2);

                Console.WriteLine();
            }
        }



        static void DisplayPrev(Tryte t)
        {
            Console.SetCursorPosition(15, Console.CursorTop - 1);
            Console.WriteLine($" {t}");
        }


        static void Sum(Tryte t1, Tryte t2)
        {
            tryteAdder.ABusInput(null, t1);
            tryteAdder.BBusInput(null, t2);

            //Tryte sum = t1 + t2;
            //Console.WriteLine($"Sum: [{sum.ToString("s")}] {sum}");
        }

        static void Diff(Tryte t1, Tryte t2)
        {
            tryteAdderForDiff.ABusInput(null, t1);
            inverterBus.BusInput(null, t2);

            //Tryte diff = t1 - t2;
            //Console.WriteLine($"Diff: [{diff.ToString("s")}] {diff}");
        }

        static void Multi(Tryte t1, Tryte t2)
        {
            Tryte multi = t1 * t2;
            Console.WriteLine($"Multi: [{multi.ToString("s")}] {multi}");
        }

        static void Div(Tryte t1, Tryte t2)
        {
            Tryte div = t1 / t2;
            Console.WriteLine($"Div: [{div.ToString("s")}] {div}");
        }

        static void Mod(Tryte t1, Tryte t2)
        {
            Tryte mod = t1 % t2;
            Console.WriteLine($"Mod: [{mod.ToString("s")}] {mod}");
        }

        static void Greater(Tryte t1, Tryte t2)
        {
            Console.WriteLine($"Greater: {t1 > t2}");
        }

        static void Lesser(Tryte t1, Tryte t2)
        {
            Console.WriteLine($"Lesser: {t1 < t2}");
        }
    }
}
