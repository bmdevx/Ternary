namespace Ternary.Components
{
    #region Trit
    public delegate void ComponentTriggeredEvent(object sender, Trit trit);

    public interface IComponentInput
    {
        void Input(object sender, Trit trit);
    }

    public interface IComponentOutput
    {
        event ComponentTriggeredEvent Output;
    }

    public interface IComponent : IComponentInput, IComponentOutput { }


    public interface IMultiInComponent
    {
        ComponentTriggeredEvent[] Inputs { get; }
    }

    public interface IMultiOutComponent
    {
        ComponentTriggeredEvent[] Outputs { get; }
    }

    public interface IMultiIOComponent : IMultiInComponent, IMultiOutComponent { }
    #endregion

    #region Tryte
    public delegate void ComponentBusTriggeredEvent(object sender, Tryte tryte);

    public interface IBusComponentInput
    {
        void BusInput(object sender, Tryte tryte);
    }

    public interface IBusComponentOutput
    {
        event ComponentBusTriggeredEvent BusOutput;
    }

    public interface IBusComponent : IBusComponentInput, IBusComponentOutput { }


    public interface IMultiBusInComponent
    {
        ComponentBusTriggeredEvent[] BusInputs { get; }
    }

    public interface IMultiBusOutComponent
    {
        ComponentBusTriggeredEvent[] BusOutputs { get; }
    }

    public interface IMultiBusIOComponent : IMultiBusInComponent, IMultiBusOutComponent { }
    #endregion
}
