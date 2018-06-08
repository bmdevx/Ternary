namespace Ternary.Components.Muxers
{
    public class DeMuxer : MuxerBase, IComponentInput
    {
        public event ComponentTriggeredEvent AOutput;
        public event ComponentTriggeredEvent BOutput;
        public event ComponentTriggeredEvent COutput;

        public Trit InputState { get; protected set; }


        public DeMuxer(Trit selectState = Trit.Neu, Trit inputState = Trit.Neu) : base(selectState)
        {
            InputState = inputState;
        }
        

        protected override void OnSelectInvoked(object sender, Trit trit)
        {
            Input(this, trit);
        }
        
        public void Input(object sender, Trit trit)
        {
            switch (SelectState)
            {
                case Trit.Neg: AOutput?.Invoke(this, trit); break;
                case Trit.Neu: BOutput?.Invoke(this, trit); break;
                case Trit.Pos: COutput?.Invoke(this, trit); break;
            }
        }
    }
}
