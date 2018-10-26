using System.Linq;
using Ternary.Components.Buses;
using Ternary.Components.Circuits;
using Ternary.Components.Muxers;
using Ternary.Reflection;

namespace Ternary.Components.Storage
{
    /// <summary>
    /// Tryte Memory with Trort Number of Addresses
    /// </summary>
    public class TrortAddrTryteMemory : IBusComponent<Tryte>
    {
        private const int TRIBBLE_SIZE = 27;
        private const int FOUR_TRIT_SIZE = 81;

        public string ComponentName { get; internal set; }
        public event ComponentBusTriggeredEvent<Tryte> BusOutput;

        public Trit ReadWriteState { get; private set; }
        public Trort Address { get; private set; }

        private TrortAddrTryteRegisterCircuit[,,,] _Addresses = new TrortAddrTryteRegisterCircuit[TRIBBLE_SIZE, TRIBBLE_SIZE, TRIBBLE_SIZE, TRIBBLE_SIZE];

        private Wire[] _XRails = Enumerable.Range(0, FOUR_TRIT_SIZE).Select(c => new Wire()).ToArray();
        private Wire[] _YRails = Enumerable.Range(0, FOUR_TRIT_SIZE).Select(c => new Wire()).ToArray();
        private Wire[] _ZRails = Enumerable.Range(0, FOUR_TRIT_SIZE).Select(c => new Wire()).ToArray();
        private Wire[] _TRails = Enumerable.Range(0, FOUR_TRIT_SIZE).Select(c => new Wire()).ToArray();

        private TritMatchCircuit3[] _XRailMatch = new TritMatchCircuit3[TRIBBLE_SIZE];
        private TritMatchCircuit3[] _YRailMatch = new TritMatchCircuit3[TRIBBLE_SIZE];
        private TritMatchCircuit3[] _ZRailMatch = new TritMatchCircuit3[TRIBBLE_SIZE];
        private TritMatchCircuit3[] _TRailMatch = new TritMatchCircuit3[TRIBBLE_SIZE];

        private DataBus<Tryte> _DataBus = new DataBus<Tryte>();
        private Wire _ReadWriteEnableWire = new Wire();

        public TrortAddrTryteMemory()
        {
            for (int x = 0; x < TRIBBLE_SIZE; x++)
            {
                for (int y = 0; y < TRIBBLE_SIZE; y++)
                {
                    for (int z = 0; z < TRIBBLE_SIZE; z++)
                    {
                        for (int t = 0; t < TRIBBLE_SIZE; t++)
                        {
                            TrortAddrTryteRegisterCircuit regComp =
                                new TrortAddrTryteRegisterCircuit(_DataBus, _ReadWriteEnableWire, _XRails[x], _YRails[y], _ZRails[z], _TRails[t]);

                            regComp.BusOutput += (s, val) => BusOutput?.Invoke(this, val);

                            _Addresses[x, y, z, t] = regComp;
                        }
                    }
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
                        TritMatchCircuit3 matchGate = new TritMatchCircuit3((Trit)i, (Trit)j, (Trit)k);
                        matchGate.Output += muxer.InputSelect;
                        muxer.Output += _XRails[c].Input;
                        _XRailMatch[c] = matchGate;

                        muxer = new Muxer(inputStateA: Trit.Neg, inputStateC: Trit.Pos);
                        matchGate = new TritMatchCircuit3((Trit)i, (Trit)j, (Trit)k);
                        matchGate.Output += muxer.InputSelect;
                        muxer.Output += _YRails[c].Input;
                        _YRailMatch[c] = matchGate;

                        muxer = new Muxer(inputStateA: Trit.Neg, inputStateC: Trit.Pos);
                        matchGate = new TritMatchCircuit3((Trit)i, (Trit)j, (Trit)k);
                        matchGate.Output += muxer.InputSelect;
                        muxer.Output += _ZRails[c].Input;
                        _ZRailMatch[c] = matchGate;

                        muxer = new Muxer(inputStateA: Trit.Neg, inputStateC: Trit.Pos);
                        matchGate = new TritMatchCircuit3((Trit)i, (Trit)j, (Trit)k);
                        matchGate.Output += muxer.InputSelect;
                        muxer.Output += _TRails[c].Input;
                        _TRailMatch[c] = matchGate;

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
        public void AddressInput(object sender, Trort addr)
        {
            Address = new Trort(addr);

            for (int i = 0; i < TRIBBLE_SIZE; i++)
            {
                TritMatchCircuit3 mg = _XRailMatch[i];

                mg.InputA(this, addr.T0);
                mg.InputB(this, addr.T1);
                mg.InputC(this, addr.T2);

                mg = _YRailMatch[i];

                mg.InputA(this, addr.T3);
                mg.InputB(this, addr.T4);
                mg.InputC(this, addr.T5);

                mg = _ZRailMatch[i];

                mg.InputA(this, addr.T6);
                mg.InputB(this, addr.T7);
                mg.InputC(this, addr.T8);

                mg = _TRailMatch[i];

                mg.InputA(this, addr.T9);
                mg.InputB(this, addr.T10);
                mg.InputC(this, addr.T11);
            }
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
