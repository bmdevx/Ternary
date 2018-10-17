using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary
{
    public interface ITernaryDataType : IEnumerable<Trit>, IComparable, IFormattable
    {
        int NUMBER_OF_TRITS { get; }
        string DebuggerInfo { get; }

        ITernaryDataType CreateFromTrits(IEnumerable<Trit> trits);

        Trit this[int pin] { get; set; }
    }
}
