using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ternary.Components.Buses.Muxers
{
    public class DeMuxerBus<T> : MuxerBusBase, IBusComponentInput<T> where T : ITernaryDataType, new()
    {
        public event ComponentBusTriggeredEvent<T> AOutput;
        public event ComponentBusTriggeredEvent<T> BOutput;
        public event ComponentBusTriggeredEvent<T> COutput;

        public T InputState { get; protected set; }
        public T AOutputState { get; protected set; }
        public T BOutputState { get; protected set; }
        public T COutputState { get; protected set; }


        public DeMuxerBus(Trit selectState = Trit.Neu) : base(selectState)
        {
            //InputState = inputState;
        }


        protected override void OnSelectInvoked(object sender, Trit select)
        {
            BusInput(this, InputState);
        }

        public void BusInput(object sender, T input)
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
