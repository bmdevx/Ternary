namespace Ternary.Components.Muxers
{
    public abstract class MuxerBase
    {
        public Trit SelectState { get; protected set; }


        public MuxerBase(Trit selectState = Trit.Neu)
        {
            SelectState = selectState;
        }

        
        public void InputSelect(object sender, Trit trit)
        {
            SelectState = trit;
            OnSelectInvoked(sender, trit);
        }

        protected abstract void OnSelectInvoked(object sender, Trit trit);
    }
}
