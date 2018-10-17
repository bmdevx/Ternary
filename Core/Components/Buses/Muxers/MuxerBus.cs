using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ternary.Components.Buses.Muxers
{
    public class MuxerBus<T> : MuxerBusBase, IBusComponentOutput<T> where T : ITernaryDataType, new()
    {
        public event ComponentBusTriggeredEvent<T> BusOutput;
        
        public T OutputState { get; protected set; }
        
        public T AInputState { get; protected set; }
        public T BInputState { get; protected set; }
        public T CInputState { get; protected set; }

        public string ComponentName { get; internal set; }


        public MuxerBus(Trit selectState = Trit.Neu) : base(selectState)
        {
            //AInputState = inputStateA;
            //BInputState = inputStateB;
            //CInputState = inputStateC;
        }


        public void AInput(object sender, T data)
        {
            AInputState = data;

            if (SelectState == Trit.Neg)
                InvokeOutput(sender ?? this, data);
        }

        public void BInput(object sender, T data)
        {
            BInputState = data;

            if (SelectState == Trit.Neu)
                InvokeOutput(sender ?? this, data);
        }

        public void CInput(object sender, T data)
        {
            CInputState = data;

            if (SelectState == Trit.Pos)
                InvokeOutput(sender ?? this, data);
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

        protected void InvokeOutput(object sender, T output)
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
