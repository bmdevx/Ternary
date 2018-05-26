using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ternary.Components
{
    public class CombinerOld : IComponentOld
    {
        public event ComponentTriggeredEvent ComponentTriggered;

        protected IComponentOld _Input1;
        public IComponentOld Input1
        {
            get { return _Input1; }
            set
            {
                if (_Input1 != null)
                    _Input1.ComponentTriggered -= OnInput1Triggered;

                _Input1 = value;
                if (_Input1 != null)
                    _Input1.ComponentTriggered += OnInput1Triggered;
            }
        }

        protected IComponentOld _Input2;
        public IComponentOld Input2
        {
            get { return _Input2; }
            set
            {
                if (_Input2 != null)
                    _Input2.ComponentTriggered -= OnInput2Triggered;

                _Input2 = value;
                if (_Input2 != null)
                    _Input2.ComponentTriggered += OnInput2Triggered;
            }
        }

        public IComponentOld Output { get; set; }


        public IEnumerable<IComponentOld> Inputs => new IComponentOld[] { _Input1, _Input2 };

        public IEnumerable<IComponentOld> Outputs => new IComponentOld[] { Output };


        public Trit Input1State { get; protected set; }

        public Trit Input2State { get; protected set; }


        public CombinerOld(Trit input1State, Trit input2State)
        {
            Input1State = input1State;
            Input2State = input2State;
        }

        public CombinerOld(IComponentOld input1, IComponentOld input2) : this(input1, input2, Trit.Neu, Trit.Neu) { }

        public CombinerOld(IComponentOld input1 = null, IComponentOld input2 = null, Trit input1State = Trit.Neu, Trit input2State = Trit.Neu)
        {
            Input1 = input1;
            Input2 = input2;
            Input1State = input1State;
            Input2State = input2State;
        }


        public void AddInput1(IMultiIOComponentOld component, int index)
        {
            component.ComponentTriggeredEvents[index] += OnInput1Triggered;
            Input1 = component;
        }

        public void AddInput2(IMultiIOComponentOld component, int index)
        {
            component.ComponentTriggeredEvents[index] += OnInput2Triggered;
            Input2 = component;
        }

        bool is1Set = false, is2Set = false;


        protected void OnInput1Triggered(object sender, Trit trit)
        {
            Input1State = trit;

            is1Set = true;

            if (is2Set)
                Trigger();
        }

        protected void OnInput2Triggered(object sender, Trit trit)
        {
            Input2State = trit;

            is2Set = true;

            if (is1Set)
                Trigger();
        }

        protected void Trigger()
        {
            is1Set = false;
            is2Set = false;
            ComponentTriggered?.Invoke(this, Execute());
        }

        protected Trit Execute()
        {
            switch (Input1State)
            {
                case Trit.Neg:
                    {
                        switch (Input2State)
                        {
                            case Trit.Neg: return Trit.Pos;
                            case Trit.Neu: return Trit.Neg;
                            case Trit.Pos: return Trit.Pos;
                        }
                        break;
                    }
                case Trit.Neu:
                    {
                        switch (Input2State)
                        {
                            case Trit.Neg: return Trit.Neg;
                            case Trit.Neu: return Trit.Neu;
                            case Trit.Pos: return Trit.Pos;
                        }
                        break;
                    }
                case Trit.Pos:
                    {
                        switch (Input2State)
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
