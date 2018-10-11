using System;
using System.Collections.Generic;
using System.Text;

namespace Ternary.Components.Experimental
{
    public class TryteMemory : IBusComponent
    {
        public string ComponentName { get; internal set; }
        public event ComponentBusTriggeredEvent BusOutput;
        
        public Trit ReadWriteState => _CurrentRegister.ReadWriteState;
        public Tryte Address { get; private set; }
        
        private TryteRegister[,] _Registers = new TryteRegister[27,27];
        private TryteRegister _CurrentRegister;

        
        public TryteMemory()
        {
            for (int i = 0; i < 27; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    TryteRegister register = new TryteRegister();

                    register.BusOutput += BusOutput;

                    _Registers[i, j] = register;
                }
            }
            
            _CurrentRegister = _Registers[0, 0];
        }


        /// <summary>
        /// Enables the Read or Write State.
        /// </summary>
        /// <param name="state">+ = Write, - = Read, 0 = Disabled</param>
        public void ReadWriteEnabled(object sender, Trit state)
        {
            _CurrentRegister.ReadWriteEnabled(sender, state);
        }
        

        public void AddressInput(object sender, Tryte tryte)
        {
            Address = tryte;

            _CurrentRegister = _Registers[
                Address.UpperTribbleValue + 13,
                Address.LowerTribbleValue + 13];
        }

        public void BusInput(object sender, Tryte tryte)
        {
            _CurrentRegister.BusInput(sender, tryte);
        }
    }


}
