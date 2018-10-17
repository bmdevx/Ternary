using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ternary.Components.Buses;
using Ternary.Components.Gates.Dyadic;
using Ternary.Components.Gates.Monadic;
using Ternary.Components.Muxers;
using Ternary.Reflection;

namespace Ternary.Components.Experimental
{
    public class TryteMemory : IBusComponent<Tryte>
    {
        public const int TRIBBLE_SIZE = 27;

        public string ComponentName { get; internal set; }
        public event ComponentBusTriggeredEvent<Tryte> BusOutput;

        public Trit ReadWriteState { get; private set; }
        public Tryte Address { get; private set; }

        private RegisterCircuit[,] _Addresses = new RegisterCircuit[TRIBBLE_SIZE, TRIBBLE_SIZE];

        private Wire[] _XRails = Enumerable.Range(0, TRIBBLE_SIZE).Select(c => new Wire()).ToArray();
        private Wire[] _YRails = Enumerable.Range(0, TRIBBLE_SIZE).Select(c => new Wire()).ToArray();

        private AddressMatchGate[] _XRailMatch = new AddressMatchGate[TRIBBLE_SIZE];
        private AddressMatchGate[] _YRailMatch = new AddressMatchGate[TRIBBLE_SIZE];

        private DataBus<Tryte> _DataBus = new DataBus<Tryte>();
        private Wire _ReadWriteEnableWire = new Wire();

        public TryteMemory()
        {
            for (int x = 0; x < TRIBBLE_SIZE; x++)
            {
                for (int y = 0; y < TRIBBLE_SIZE; y++)
                {
                    RegisterCircuit regComp = new RegisterCircuit(_DataBus, _ReadWriteEnableWire, _XRails[x], _YRails[y]);

                    regComp.BusOutput += (s, t) => BusOutput?.Invoke(this, t);

                    _Addresses[x, y] = regComp;
                }
            }

            int c = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    for (int k = -1; k < 2; k++)
                    {
                        Muxer muxer = new Muxer(inputStateA: Trit.Neg, inputStateC: Trit.Pos);
                        AddressMatchGate matchGate = new AddressMatchGate((Trit)i, (Trit)j, (Trit)k);
                        matchGate.Output += muxer.InputSelect;
                        muxer.Output += _XRails[c].Input;
                        _XRailMatch[c] = matchGate;

                        muxer = new Muxer(inputStateA: Trit.Neg, inputStateC: Trit.Pos);
                        matchGate = new AddressMatchGate((Trit)i, (Trit)j, (Trit)k);
                        matchGate.Output += muxer.InputSelect;
                        muxer.Output += _YRails[c].Input;
                        _YRailMatch[c] = matchGate;

                        c++;
                    }
                }
            }

#if DEBUG
            ComponentTools.SetComponentNames(this);
#endif
        }


        /// <summary>
        /// Enables the Read or Write State.
        /// </summary>
        /// <param name="state">+ = Write, - = Read, 0 = Disabled</param>
        public void ReadWriteEnabled(object sender, Trit state)
        {
            ReadWriteState = state;

            _ReadWriteEnableWire.Input(this, state);
        }

        /// <summary>
        /// Sets the Address that will be written / read from
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="addr">Address to select</param>
        public void AddressInput(object sender, Tryte addr)
        {
            Address = new Tryte(addr);

            for (int i = 0; i < TRIBBLE_SIZE; i++)
            {
                AddressMatchGate mg = _XRailMatch[i];

                mg.InputA(this, addr.T0);
                mg.InputB(this, addr.T1);
                mg.InputC(this, addr.T2);

                mg = _YRailMatch[i];

                mg.InputA(this, addr.T3);
                mg.InputB(this, addr.T4);
                mg.InputC(this, addr.T5);
            }

            //_XRails[Address.LowerTribbleValue + 13].Input(this, Trit.Neu);
            //_YRails[Address.MiddleTribbleValue + 13].Input(this, Trit.Neu);

            //Address = addr;

            //_XRails[Address.LowerTribbleValue + 13].Input(this, Trit.Pos);
            //_YRails[Address.MiddleTribbleValue + 13].Input(this, Trit.Pos);
        }

        /// <summary>
        /// Sets the Value that will be written to a Register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tryte">Value to be written</param>
        public void BusInput(object sender, Tryte tryte)
        {
            _DataBus.BusInput(this, tryte);
        }
    }
}
