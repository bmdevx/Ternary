namespace Ternary.Components.Muxers
{
    public abstract class MuxerBase
    {
        public event ComponentTriggeredEvent SelectInput;

        public Trit SelectState { get; protected set; }


        public MuxerBase(Trit selectState = Trit.Neu)
        {
            SelectInput += SelectInvoked;
            SelectState = selectState;
        }

        
        protected void SelectInvoked(object sender, Trit trit)
        {
            SelectState = trit;
            OnSelectInvoked(sender, trit);
        }

        protected abstract void OnSelectInvoked(object sender, Trit trit);
    }
}
