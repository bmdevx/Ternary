using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components
{
    public class WireOld : BasicComponentOld
    {
        public WireOld(IEnumerable<IComponentOld> inputs = null, IEnumerable<IComponentOld> outputs = null) : base(inputs, outputs) { }

        protected override Trit Execute(object sender, Trit trit)
        {
            return trit;
        }
    }
}
