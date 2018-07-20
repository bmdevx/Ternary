using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ternary.Components.Buses.Muxers
{
    [DebuggerDisplay("{DebuggerInfo}")]
    public class DeMuxerBus : MuxerBusBase, IBusComponentInput
    {
        public event ComponentBusTriggeredEvent AOutput;
        public event ComponentBusTriggeredEvent BOutput;
        public event ComponentBusTriggeredEvent COutput;

        public Tryte InputState { get; protected set; }
        public Tryte AOutputState { get; protected set; }
        public Tryte BOutputState { get; protected set; }
        public Tryte COutputState { get; protected set; }

        internal string DebuggerInfo => ToString();


        public DeMuxerBus(Trit selectState = Trit.Neu, Tryte inputState = new Tryte()) : base(selectState)
        {
            InputState = inputState;
        }


        protected override void OnSelectInvoked(object sender, Trit select)
        {
            BusInput(this, InputState);
        }

        public void BusInput(object sender, Tryte input)
        {
            InputState = input;

            switch (SelectState)
            {
                case Trit.Neg: AOutput?.Invoke(this, AOutputState = input); break;
                case Trit.Neu: BOutput?.Invoke(this, BOutputState = input); break;
                case Trit.Pos: COutput?.Invoke(this, COutputState = input); break;
            }
        }


        public override string ToString()
        {
            return $"[{SelectState.ToSymbol()}] {InputState}>{AOutputState}:{BOutputState}:{COutputState}";
        }
    }
}
