using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ternary.Components
{
    public class MultiWireMerge : MultiIn1OutComponent
    {
        private bool[] InputSets { get; }

        public MultiWireMerge(int numberOfInputs) : base(numberOfInputs)
        {
            InputSets = new bool[numberOfInputs];
        }

        public override void OnInputInvoked(int inputPin, Trit trit, object sender = null)
        {
            InputSets[inputPin] = true;

            if (inputPin >= NUMBER_OF_INPUTS || inputPin < 0)
                throw new IndexOutOfRangeException(PinOutOfRange);

            InputStates[inputPin] = trit;

            if (InputSets.All(iis => iis))
            {
                InvokeOutput(sender, Execute());
                Array.Clear(InputSets, 0, InputSets.Length);
            }
        }

        protected override Trit Execute()
        {
            if (NUMBER_OF_INPUTS == 1)
                return InputStates[0];
            else if (NUMBER_OF_INPUTS == 2)
                return Merge(InputStates[0], InputStates[2]);
            else
            {
                Trit trit = InputStates[0];

                for (int i = 1; i < NUMBER_OF_INPUTS - 1; i++)
                {
                    trit = Merge(trit, InputStates[i]);
                }
                
                return trit;
            }
        }

        private Trit Merge(Trit t1, Trit t2)
        {
            switch (t1)
            {
                case Trit.Neg:
                    {
                        switch (t2)
                        {
                            case Trit.Neg: return Trit.Pos;
                            case Trit.Neu: return Trit.Neg;
                            case Trit.Pos: return Trit.Pos;
                        }
                        break;
                    }
                case Trit.Neu:
                    {
                        switch (t2)
                        {
                            case Trit.Neg: return Trit.Neg;
                            case Trit.Neu: return Trit.Neu;
                            case Trit.Pos: return Trit.Pos;
                        }
                        break;
                    }
                case Trit.Pos:
                    {
                        switch (t2)
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
