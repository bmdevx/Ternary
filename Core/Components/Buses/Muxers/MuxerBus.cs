using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ternary.Components.Buses.Muxers
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public class MuxerBus : MuxerBusBase, IBusComponentOutput
    {
        public event ComponentBusTriggeredEvent BusOutput;
        
        public Tryte OutputState { get; protected set; }
        
        public Tryte AInputState { get; protected set; }
        public Tryte BInputState { get; protected set; }
        public Tryte CInputState { get; protected set; }

        internal string DebuggerInfo => ToString();


        public MuxerBus(Trit selectState = Trit.Neu, Tryte inputStateA = new Tryte(),
            Tryte inputStateB = new Tryte(), Tryte inputStateC = new Tryte()) : base(selectState)
        {
            AInputState = inputStateA;
            BInputState = inputStateB;
            CInputState = inputStateC;
        }


        public void AInput(object sender, Tryte tryte)
        {
            AInputState = tryte;

            if (SelectState == Trit.Neg)
                InvokeOutput(sender ?? this, tryte);
        }

        public void BInput(object sender, Tryte tryte)
        {
            BInputState = tryte;

            if (SelectState == Trit.Neu)
                InvokeOutput(sender ?? this, tryte);
        }

        public void CInput(object sender, Tryte tryte)
        {
            CInputState = tryte;

            if (SelectState == Trit.Pos)
                InvokeOutput(sender ?? this, tryte);
        }

        
        protected override void OnSelectInvoked(object sender, Trit select)
        {
            switch (select)
            {
                case Trit.Neg: InvokeOutput(this, AInputState); break;
                case Trit.Neu: InvokeOutput(this, BInputState); break;
                case Trit.Pos: InvokeOutput(this, CInputState); break;
            }
        }

        protected void InvokeOutput(object sender, Tryte output)
        {
            OutputState = output;
            BusOutput?.Invoke(sender, output);
        }


        public override string ToString()
        {
            return $"[{SelectState.ToSymbol()}] {AInputState}:{BInputState}:{CInputState}>{OutputState}";
        }
    }
}
