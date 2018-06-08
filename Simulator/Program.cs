using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ternary;
using Ternary.Components;
using Ternary.Components.Gates;

namespace Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Wire wire1 = new Wire();
            Wire wire2 = new Wire();

            SumGate sumGate = new SumGate(wire1, wire2);

            sumGate.Output += (s, t) =>
            {
                Console.WriteLine($"Sender: {s.GetType().Name} - Trit: {t.GetDescription()}");
            };

            while (true)
            {
                Console.Write("Wire1: ");
                if (TritEx.TryParse(Console.ReadLine(), out Trit trit))
                {
                    wire1.Input(wire1, trit);
                }
                else
                    break;

                Console.Write("Wire2: ");
                if (TritEx.TryParse(Console.ReadLine(), out trit))
                {
                    wire2.Input(wire2, trit);
                }
                else
                    break;
            }

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
    }

    public static class Tools
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            string description = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                        {
                            // we're only getting the first description we find
                            // others will be ignored
                            description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        }

                        break;
                    }
                }
            }

            return description;
        }

    }
}
