using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Buses.Muxers
{
    public abstract class MuxerBusBase
    {
        public Trit SelectState { get; protected set; }


        public MuxerBusBase(Trit selectState = Trit.Neu)
        {
            SelectState = selectState;
        }


        public void InputSelect(object sender, Trit select)
        {
            SelectState = select;
            OnSelectInvoked(sender, select);
        }

        protected abstract void OnSelectInvoked(object sender, Trit select);
    }
}
