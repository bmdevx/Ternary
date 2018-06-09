namespace Ternary.Components
{
    public class WireMerge : Basic2In1OutComponent
    {
        private bool is1Set = false, is2Set = false;


        public WireMerge(IComponentOutput component1, IComponentOutput component2, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu) :
            base(component1, component2, input1State, input2State) { }

        public WireMerge(ComponentTriggeredEvent input1 = null, ComponentTriggeredEvent input2 = null, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu) :
            base(input1, input2, input1State, input2State) { }
        

        private void InvokeOutput()
        {
            InvokeOutput(this, Execute(InputStateA, InputStateB));
            is1Set = is2Set = false;
        }


        protected override void OnInputAInvoked(object sender, Trit trit)
        {
            InputStateA = trit;

            is1Set = true;

            if (is2Set)
                InvokeOutput();
        }

        protected override void OnInputBInvoked(object sender, Trit trit)
        {
            InputStateB = trit;

            is2Set = true;

            if (is1Set)
                InvokeOutput();
        }


        public override void Input(Trit input1State, Trit input2State, object sender = null)
        {
            base.Input(input1State, input2State, sender);
            is1Set = is2Set = false;
        }
        

        protected override Trit Execute(Trit input1State, Trit input2State)
        {
            switch (input1State)
            {
                case Trit.Neg:
                    {
                        switch (input2State)
                        {
                            case Trit.Neg: return Trit.Pos;
                            case Trit.Neu: return Trit.Neg;
                            case Trit.Pos: return Trit.Pos;
                        }
                        break;
                    }
                case Trit.Neu:
                    {
                        switch (input2State)
                        {
                            case Trit.Neg: return Trit.Neg;
                            case Trit.Neu: return Trit.Neu;
                            case Trit.Pos: return Trit.Pos;
                        }
                        break;
                    }
                case Trit.Pos:
                    {
                        switch (input2State)
                        {
                            case Trit.Neg: return Trit.Pos;
                            case Trit.Neu: return Trit.Pos;
                            case Trit.Pos: return Trit.Neg;
                        }
                        break;
                    }
            }

            return Trit.Neu;
        }
    }
}
