using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ternary.Components.Muxers;

namespace Ternary.Components.Adders
{
    public class FullAdder : IAdder
    {
        private Muxer[] aMuxers = new Muxer[]
        {
            new Muxer(Trit.Neu, Trit.Neu, Trit.Pos, Trit.Neg),
            new Muxer(Trit.Neu, Trit.Pos, Trit.Neg, Trit.Neu),
            new Muxer(Trit.Neu, Trit.Neg, Trit.Neg, Trit.Neu),
            new Muxer(Trit.Neu, Trit.Neg, Trit.Neu, Trit.Neu),
            new Muxer(Trit.Neu, Trit.Neu, Trit.Neu, Trit.Pos),
            new Muxer(Trit.Neu, Trit.Neu, Trit.Pos, Trit.Pos)
        };

        private Muxer[] bMuxers = new Muxer[]
        {
            new Muxer(),
            new Muxer(),
            new Muxer(),
            new Muxer(),
            new Muxer(),
            new Muxer()
        };

        private Muxer[] cMuxers = new Muxer[]
        {
            new Muxer(),
            new Muxer()
        };


        public Trit CarryInState { get; protected set; }
        private Wire _WireInCarry = new Wire();


        public FullAdder()
        {
            foreach (Muxer mux in aMuxers)
            {
               _WireInA += mux.InputSelect;
            }
            
            foreach (Muxer mux in bMuxers)
            {
                _WireInB += mux.InputSelect;
            }

            foreach (Muxer mux in cMuxers)
            {
                _WireInCarry += mux.InputSelect;
            }

            Muxer m = bMuxers[0];

            aMuxers[0].Output += m.AInput;
            aMuxers[1].Output += m.BInput;
            _WireInA += m.CInput;

            m = bMuxers[1];
            aMuxers[1].Output += m.AInput;
            _WireInA += m.BInput;
            aMuxers[0].Output += m.CInput;

            m = bMuxers[2];
            _WireInA += m.AInput;
            aMuxers[0].Output += m.BInput;
            aMuxers[1].Output += m.CInput;

            m = bMuxers[3];
            aMuxers[2].Output += m.AInput;
            aMuxers[3].Output += m.BInput;
            
            m = bMuxers[4];
            aMuxers[3].Output += m.AInput;
            aMuxers[4].Output += m.CInput;
            
            m = bMuxers[5];
            aMuxers[4].Output += m.BInput;
            aMuxers[5].Output += m.CInput;
            
            m = cMuxers[0];
            bMuxers[0].Output += m.AInput;
            bMuxers[1].Output += m.BInput;
            bMuxers[2].Output += m.CInput;
            m.Output += InvokeSumOutput;
            
            m = cMuxers[1];
            bMuxers[3].Output += m.AInput;
            bMuxers[4].Output += m.BInput;
            bMuxers[5].Output += m.CInput;
            m.Output += InvokeCarryOutput;
        }


        public void InputCarry(object sender, Trit trit)
        {
            CarryInState = trit;
            _WireInCarry.Input(sender, trit);
        }
    }
}
