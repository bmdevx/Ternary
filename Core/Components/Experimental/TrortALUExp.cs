using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ternary.Components.Adders;
using Ternary.Components.Buses.Dyadic;
using Ternary.Components.Buses.Monadic;
using Ternary.Components.Buses.Muxers;
using Ternary.Components.Gates.Dyadic;
using Ternary.Components.Gates.Monadic;
using Ternary.Components.Muxers;
using Ternary.Reflection;
using Ternary.Tools;

namespace Ternary.Components
{
    //Control States ex:
    //012345
    //A, B, Op1, Op2, X, +-1
    //+0+000: A
    //0++000: B
    //+++000: A + B 
    //+++00+: (A + B) + 1
    //+0+00+: A + 1
    //0++00+: B + 1
    //+0+00-: A - 1
    //0++00-: B - 1
    //+-+000: A - B
    //-++000: B - A
    //+-+00-: (A - B) - 1
    //-++00-: (B - A) - 1
    //++++00: MAX A & B
    //+++-00: MIN A & B
    //++0000: A & B
    //++-+00: A XOR B
    //++-000: A OR B
    //++--00: A NOR B

    //      __OPS__
    //  1: +   0   -
    //  + MAX ADD MIN
    //2:0     AND
    //  - XOR OR  NOR

    public class TrortALUExp : IBusComponentOutput<Trort>
    {
        public event ComponentBusTriggeredEvent<Trort> BusOutput;
        public event ComponentTriggeredEvent OverflowOutput;
        public event ComponentTriggeredEvent SignedOutput;

        public Trort BusValue { get; private set; }
        public string ComponentName { get; internal set; }

        /// <summary>
        /// + = Positive Overflow, 0 = No Overflow, - = Negative Overflow
        /// </summary>
        public Trit OverflowState { get; protected set; }
        /// <summary>
        /// + = Positive Number, 0 = Zero, - = Negative Number
        /// </summary>
        public Trit SignedState { get; protected set; }

        #region old
        /// <summary>
        /// + = A Input is Inverted, - = Not Inverted
        /// </summary>
        public Trit AInversionControlState { get; protected set; }
        /// <summary>
        /// + = B Input is Inverted, - = Not Inverted
        /// </summary>
        public Trit BInversionControlState { get; protected set; }
        /// <summary>
        /// + = A Input is set to 0, - = Not Negated
        /// </summary>
        public Trit ANegationControlState { get; protected set; }
        /// <summary>
        /// + = B Input is set to 0, - = Not Negated
        /// </summary>
        public Trit BNegationControlState { get; protected set; }
        /// <summary>
        /// + = MaxBus, 0 = Addition, - = Minbus
        /// </summary>
        public Trit FowleanControlState { get; protected set; }
        #endregion

        private TrortAdder adder = new TrortAdder();

        private MaxBus<Trort> maxBus = new MaxBus<Trort>();
        private MinBus<Trort> minBus = new MinBus<Trort>();

        #region old
        private MuxerBus<Trort> muxerBusA = new MuxerBus<Trort>(Trit.Neg);
        private MuxerBus<Trort> muxerBusB = new MuxerBus<Trort>(Trit.Neg);
        private MuxerBus<Trort> muxerBus2 = new MuxerBus<Trort>(Trit.Neg);
        private MuxerBus<Trort> muxerBusF = new MuxerBus<Trort>(Trit.Neg);

        private InverterBus<Trort> inverterBusA = new InverterBus<Trort>();
        private InverterBus<Trort> inverterBusB = new InverterBus<Trort>();
        private InverterBus<Trort> inverterBusOutput = new InverterBus<Trort>();

        private MaxGate maxGateA = new MaxGate(inputStateA: Trit.Neg, inputStateB: Trit.Neg);
        private MaxGate maxGateB = new MaxGate(inputStateA: Trit.Neg, inputStateB: Trit.Neg);

        private ShiftDownGate shiftDownGateA = new ShiftDownGate(inputState: Trit.Neg);
        private ShiftDownGate shiftDownGateB = new ShiftDownGate(inputState: Trit.Neg);

        private Muxer[] muxers = Create.NewTrortSizedArray(c => new Muxer(inputStateC: Trit.Pos, inputStateA: Trit.Neg));
        #endregion old


        DeMuxer dmA, dmB, dmOp1, dmOp2, dm4, dmAS;


        
        public TrortALUExp()
        {
            #region old
            inverterBusA.BusOutput += muxerBusA.BInput;
            inverterBusB.BusOutput += muxerBusB.BInput;

            maxGateA.Output += muxerBusA.InputSelect;
            maxGateB.Output += muxerBusB.InputSelect;

            shiftDownGateA.Output += maxGateA.BInput;
            shiftDownGateB.Output += maxGateA.BInput;

            muxerBusA.BusOutput += adder.ABusInput;
            muxerBusA.BusOutput += maxBus.ABusInput;
            muxerBusA.BusOutput += minBus.ABusInput;
            
            muxerBusB.BusOutput += adder.BBusInput;
            muxerBusB.BusOutput += maxBus.BBusInput;
            muxerBusB.BusOutput += minBus.BBusInput;

            adder.CarryOut += InvokeOverflowOutput;

            minBus.BusOutput += muxerBus2.AInput;
            adder.BusOutput += muxerBus2.BInput;
            maxBus.BusOutput += muxerBus2.CInput;

            muxerBus2.BusOutput += muxerBusF.AInput;
            muxerBus2.BusOutput += muxerBusF.BInput;
            muxerBus2.BusOutput += inverterBusOutput.BusInput;

            inverterBusOutput.BusOutput += muxerBusF.CInput;
            
            for (int i = 0; i < muxers.Length - 1; i++)
            {
                muxers[i].Output += muxers[i + 1].BInput;
            }

            muxers[4].Output += InvokeSignedOutput;

            muxerBusF.BusOutput += (s, trort) =>
            {
                for (int i = 0; i < muxers.Length; i++)
                {
                    muxers[i].InputSelect(s, trort[i + 1]);
                }

                muxers[0].BInput(s, trort[0]);

                InvokeOutput(s, trort);
            };
            #endregion


            dmA = new DeMuxer(Trit.Neu, Trit.Pos);
            dmB = new DeMuxer(Trit.Neu, Trit.Pos);
            dmOp1 = new DeMuxer(Trit.Neu, Trit.Pos);
            dmOp2 = new DeMuxer(Trit.Neu, Trit.Pos);
            dm4 = new DeMuxer(Trit.Neu, Trit.Pos);
            dmAS = new DeMuxer(Trit.Neu, Trit.Pos);







#if DEBUG
            ComponentTools.SetComponentNames(this);
#endif
        }

        [Obsolete]
        public void AInversionInput(object sender, Trit trit)
        {
            if (trit == Trit.Neu)
                throw new Exception("Inversion Input can not be Neu");

            AInversionControlState = trit;
            shiftDownGateA.Input(this, trit);
        }

        [Obsolete]
        public void BInversionInput(object sender, Trit trit)
        {
            if (trit == Trit.Neu)
                throw new Exception("Inversion Input can not be Neu");

            BInversionControlState = trit;
            shiftDownGateB.Input(this, trit);
        }


        [Obsolete]
        public void ANegationInput(object sender, Trit trit)
        {
            if (trit == Trit.Neu)
                throw new Exception("Negation Input can not be Neu");

            ANegationControlState = trit;
            maxGateA.AInput(this, trit);
        }

        [Obsolete]
        public void BNegationInput(object sender, Trit trit)
        {
            if (trit == Trit.Neu)
                throw new Exception("Negation Input can not be Neu");
            
            BNegationControlState = trit;
            maxGateB.AInput(this, trit);
        }


        [Obsolete]
        public void FowleanControlInput(object sender, Trit trit)
        {
            FowleanControlState = trit;
            muxerBus2.InputSelect(this, trit);
        }



        public void Control(object sender, Tryte control)
        {
            dmA.InputSelect(this, control[0]);
            dmB.InputSelect(this, control[1]);
            dmOp1.InputSelect(this, control[2]);
            dmOp2.InputSelect(this, control[3]);
            dm4.InputSelect(this, control[4]);
            dmAS.InputSelect(this, control[5]);
        }


        public void ABusInput(object sender, Trort trort)
        {
            muxerBusA.AInput(this, trort);
            inverterBusA.BusInput(this, trort);
        }

        public void BBusInput(object sender, Trort trort)
        {
            muxerBusB.AInput(this, trort);
            inverterBusB.BusInput(this, trort);
        }


        protected void InvokeOutput(object sender, Trort output)
        {
            BusValue = output;
            BusOutput?.Invoke(this, output);
        }

        protected void InvokeOverflowOutput(object sender, Trit overflow)
        {
            OverflowState = overflow;
            OverflowOutput?.Invoke(this, OverflowState);
        }

        protected void InvokeSignedOutput(object sender, Trit sign)
        {
            SignedState = sign;
            SignedOutput?.Invoke(this, SignedState);
        }
    }
}
