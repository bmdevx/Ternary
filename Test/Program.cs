using System;
using System.Linq;
using Ternary;
using Ternary.Components.Adders;
using Ternary.Components.Buses;
using Ternary.Components.Buses.Monadic;
using Ternary.Components.Experimental;

namespace Test
{
    class Program
    {
        static TryteAdder tryteAdder = new TryteAdder(), tryteAdderForDiff = new TryteAdder();
        static InverterBus<Tryte> tryteInverterBus = new InverterBus<Tryte>();

        static TrortAdder trortAdder = new TrortAdder(), trortAdderForDiff = new TrortAdder();
        static InverterBus<Trort> trortInverterBus = new InverterBus<Trort>();



        static void Main(string[] args)
        {
            TrortMemoryTest();

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }



        static void TryteMemoryTest()
        {
            TryteMemory memory = new TryteMemory();

            Func<Tryte, string> t2s = (tryte) =>
            {
                return String.Join("", tryte.LowerTribble.Select(t => t.ToSymbol())) + ":" +
                    String.Join("", tryte.UpperTribble.Select(t => t.ToSymbol()));
            };

            memory.BusOutput += (s, t) =>
            {
                Console.WriteLine($" Address   Value");
                Console.WriteLine($"[{t2s(memory.Address)}]: {t.ToString()}");
            };
            
            while (true)
            {
                Console.Write("Address: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte addr))
                    break;

                Console.SetCursorPosition(9, Console.CursorTop - 1);
                Console.WriteLine($"      [{addr.ToString("s")}] {addr}");

                Console.Write("Action: ");
                if (!TritEx.TryParse(Console.ReadLine(), out Trit act))
                    break;

                Console.SetCursorPosition(8, Console.CursorTop - 1);
                Console.WriteLine($"       {act.ToSymbol()}        {(act == Trit.Pos ? "Write" : act == Trit.Neg ? "Read" : "Disable")}");

                if (act == Trit.Pos)
                {
                    Console.Write("Storage Value: ");
                    if (!Tryte.TryParse(Console.ReadLine(), out Tryte store))
                        break;

                    Console.SetCursorPosition(15, Console.CursorTop - 1);
                    Console.WriteLine($"[{store.ToString("S")}] {store}");
                    
                    memory.AddressInput(null, addr);
                    memory.BusInput(null, store);
                    memory.ReadWriteEnabled(null, act);
                    memory.ReadWriteEnabled(null, Trit.Neu);
                }
                else if (act == Trit.Neg)
                {
                    memory.AddressInput(null, addr);
                    memory.ReadWriteEnabled(null, act);
                    memory.ReadWriteEnabled(null, Trit.Neu);
                }

                Console.WriteLine();
            }
            
        }

        static void TrortMemoryTest()
        {
            STrortAddrTryteMemory trortAddrMemory = new STrortAddrTryteMemory();

            Func<Trort, string> t2s = (tryte) =>
            {
                return String.Join("", tryte.LowerTryte.LowerTribble.Select(t => t.ToSymbol())) + ":" +
                    String.Join("", tryte.LowerTryte.UpperTribble.Select(t => t.ToSymbol())) + ":" +
                    String.Join("", tryte.UpperTryte.LowerTribble.Select(t => t.ToSymbol())) + ":" +
                    String.Join("", tryte.UpperTryte.UpperTribble.Select(t => t.ToSymbol()));
            };

            trortAddrMemory.BusOutput += (s, t) =>
            {
                Console.WriteLine($"     Address       Value");
                Console.WriteLine($"[{t2s(trortAddrMemory.Address)}]: {t.ToString()}");
            };

            while (true)
            {
                Console.Write("Address: ");
                if (!Trort.TryParse(Console.ReadLine(), out Trort addr))
                    break;

                Console.SetCursorPosition(9, Console.CursorTop - 1);
                Console.WriteLine($"      [{addr.ToString("s")}] {addr}");

                Console.Write("Action: ");
                if (!TritEx.TryParse(Console.ReadLine(), out Trit act))
                    break;

                Console.SetCursorPosition(8, Console.CursorTop - 1);
                Console.WriteLine($"       {act.ToSymbol()}              {(act == Trit.Pos ? "Write" : act == Trit.Neg ? "Read" : "Disable")}");

                if (act == Trit.Pos)
                {
                    Console.Write("Storage Value: ");
                    if (!Tryte.TryParse(Console.ReadLine(), out Tryte store))
                        break;

                    Console.SetCursorPosition(15, Console.CursorTop - 1);
                    Console.WriteLine($"[{store.ToString("S")}]       {store}");

                    trortAddrMemory.AddressInput(null, addr);
                    trortAddrMemory.BusInput(null, store);
                    trortAddrMemory.ReadWriteEnabled(null, act);
                    trortAddrMemory.ReadWriteEnabled(null, Trit.Neu);
                }
                else if (act == Trit.Neg)
                {
                    trortAddrMemory.AddressInput(null, addr);
                    trortAddrMemory.ReadWriteEnabled(null, act);
                    trortAddrMemory.ReadWriteEnabled(null, Trit.Neu);
                }

                Console.WriteLine();
            }

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

            tryteInverterBus.BusOutput += tryteAdderForDiff.BBusInput;



            while (true)
            {
                Console.Write("Tryte 1: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte t1))
                    break;

                DisplayPrev(t1, 9);

                Console.Write("Tryte 2: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte t2))
                    break;

                DisplayPrev(t2, 9);

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

        static void TrortTest()
        {
            trortAdder.BusOutput += (s, sum) => {
                Console.WriteLine($"Sum: [{sum.ToString("s")}] {sum}");
            };

            trortAdder.CarryOut += (s, carry) => {
                Console.WriteLine($"Sum Carry: {carry.ToSymbol()}");
            };


            trortAdderForDiff.BusOutput += (s, sum) => {
                Console.WriteLine($"Diff: [{sum.ToString("s")}] {sum}");
            };

            trortAdderForDiff.CarryOut += (s, carry) => {
                Console.WriteLine($"Diff Carry: {carry.ToSymbol()}");
            };

            trortInverterBus.BusOutput += trortAdderForDiff.BBusInput;



            while (true)
            {
                Console.Write("Trort A: ");
                if (!Trort.TryParse(Console.ReadLine(), out Trort t1))
                    break;

                DisplayPrev(t1, 9);

                Console.Write("Trort B: ");
                if (!Trort.TryParse(Console.ReadLine(), out Trort t2))
                    break;

                DisplayPrev(t2, 9);

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


        static void DisplayPrev(Tryte t, int offset)
        {
            Console.SetCursorPosition(offset, Console.CursorTop - 1);
            Console.WriteLine($"{t.ToString("s")} {t}");
        }

        static void DisplayPrev(Trort t, int offset)
        {
            Console.SetCursorPosition(offset, Console.CursorTop - 1);
            Console.WriteLine($"{t.ToString("s")} {t}");
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
            tryteInverterBus.BusInput(null, t2);

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

        
        static void Sum(Trort t1, Trort t2)
        {
            trortAdder.ABusInput(null, t1);
            trortAdder.BBusInput(null, t2);

            //Tryte sum = t1 + t2;
            //Console.WriteLine($"Sum: [{sum.ToString("s")}] {sum}");
        }

        static void Diff(Trort t1, Trort t2)
        {
            trortAdderForDiff.ABusInput(null, t1);
            trortInverterBus.BusInput(null, t2);

            //Tryte diff = t1 - t2;
            //Console.WriteLine($"Diff: [{diff.ToString("s")}] {diff}");
        }
    }
}
