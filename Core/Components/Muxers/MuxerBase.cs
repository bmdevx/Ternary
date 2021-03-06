﻿namespace Ternary.Components.Muxers
{
    public abstract class MuxerBase
    {
        public Trit SelectState { get; protected set; }


        public MuxerBase(Trit selectState = Trit.Neu)
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
