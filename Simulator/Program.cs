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
            while (true)
            {
                Action<Tryte> DisplayPrev = (t) =>
                {
                    Console.SetCursorPosition(15, Console.CursorTop - 1);
                    Console.WriteLine($" {t}");
                };


                Console.Write("Tryte S: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte to))
                    break;

                DisplayPrev(to);
                
                Console.Write("Tryte A: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte ta))
                    break;

                DisplayPrev(ta);

                Console.Write("Tryte B: ");
                if (!Tryte.TryParse(Console.ReadLine(), out Tryte tb))
                    break;

                DisplayPrev(tb);


                Console.WriteLine();

                //

                Console.WriteLine();
            }


            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
    }
}
