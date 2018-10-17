using System.Collections;
using System.Collections.Generic;

namespace Ternary.Components
{
    public interface IComponentBase
    {
        string ComponentName { get; }
    }


    #region Trit
    public delegate void ComponentTriggeredEvent(object sender, Trit trit);

    public interface IComponentInput
    {
        void Input(object sender, Trit trit);
    }

    public interface IComponentOutput : IComponentBase
    {
        event ComponentTriggeredEvent Output;
    }

    public interface IComponent : IComponentInput, IComponentOutput { }


    public interface IMultiInComponent
    {
        ComponentTriggeredEvent[] Inputs { get; }
    }

    public interface IMultiOutComponent : IComponentBase
    {
        ComponentTriggeredEvent[] Outputs { get; }
    }

    public interface IMultiIOComponent : IMultiInComponent, IMultiOutComponent { }
    #endregion

    #region Bus
    public delegate void ComponentBusTriggeredEvent<T>(object sender, T tryte) where T : ITernaryDataType;

    public interface IBusComponentInput<T> where T : ITernaryDataType
    {
        void BusInput(object sender, T data);
    }

    public interface IBusComponentOutput<T> : IComponentBase where T : ITernaryDataType
    {
        event ComponentBusTriggeredEvent<T> BusOutput;
    }

    public interface IBusComponent<T> : IBusComponentInput<T>, IBusComponentOutput<T> where T : ITernaryDataType { }


    public interface IMultiBusInComponent<T> where T : ITernaryDataType
    {
        ComponentBusTriggeredEvent<T>[] BusInputs { get; }
    }

    public interface IMultiBusOutComponent<T> : IComponentBase where T : ITernaryDataType
    {
        ComponentBusTriggeredEvent<T>[] BusOutputs { get; }
    }

    public interface IMultiBusIOComponent<T> : IMultiBusInComponent<T>, IMultiBusOutComponent<T> where T : ITernaryDataType { }
    #endregion
}
