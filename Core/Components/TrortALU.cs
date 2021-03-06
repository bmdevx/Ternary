﻿using System;
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
    //Notes: The negation and inversion signals should always be -1 or 1, never 0

    //TODO: Combine Inversion and Negation: + Inverted, 0 = Not Inverted, - = Negated
    
    public class TrortALU : IBusComponentOutput<Trort>
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

        private TrortAdder adder = new TrortAdder();

        private MaxBus<Trort> maxBus = new MaxBus<Trort>();
        private MinBus<Trort> minBus = new MinBus<Trort>();
        
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



        public TrortALU()
        {
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



#if DEBUG
            ComponentTools.SetComponentNames(this);
#endif
        }


        public void AInversionInput(object sender, Trit trit)
        {
            if (trit == Trit.Neu)
                throw new Exception("Inversion Input can not be Neu");

            AInversionControlState = trit;
            shiftDownGateA.Input(this, trit);
        }

        public void BInversionInput(object sender, Trit trit)
        {
            if (trit == Trit.Neu)
                throw new Exception("Inversion Input can not be Neu");

            BInversionControlState = trit;
            shiftDownGateB.Input(this, trit);
        }


        public void ANegationInput(object sender, Trit trit)
        {
            if (trit == Trit.Neu)
                throw new Exception("Negation Input can not be Neu");

            ANegationControlState = trit;
            maxGateA.AInput(this, trit);
        }

        public void BNegationInput(object sender, Trit trit)
        {
            if (trit == Trit.Neu)
                throw new Exception("Negation Input can not be Neu");
            
            BNegationControlState = trit;
            maxGateB.AInput(this, trit);
        }


        public void FowleanControlInput(object sender, Trit trit)
        {
            FowleanControlState = trit;
            muxerBus2.InputSelect(this, trit);
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
