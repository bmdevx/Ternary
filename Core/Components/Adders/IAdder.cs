using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Adders
{
    public abstract class IAdder
    {
        public ComponentTriggeredEvent SumOutput;
        public ComponentTriggeredEvent CarryOutput;

        public Trit SumValue { get; protected set; }
        public Trit CarryValue { get; protected set; }
        
        protected Wire _WireInA = new Wire();
        protected Wire _WireInB = new Wire();


        public void AInput(object sender, Trit trit)
        {
            _WireInA.Input(sender, trit);
        }

        public void BInput(object sender, Trit trit)
        {
            _WireInB.Input(sender, trit);
        }


        protected void InvokeSumOutput(object sender, Trit trit)
        {
            SumValue = trit;
            SumOutput?.Invoke(this, trit);
        }

        protected void InvokeCarryOutput(object sender, Trit trit)
        {
            CarryValue = trit;
            CarryOutput?.Invoke(this, trit);
        }
    }
}
