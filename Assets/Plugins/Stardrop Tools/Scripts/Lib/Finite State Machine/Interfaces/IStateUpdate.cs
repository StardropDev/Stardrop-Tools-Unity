
namespace StardropTools.FiniteStateMachines
{
    public interface IStateUpdate
    {
        void UpdateState();
    }

    public interface IStateUpdate<T>
    {
        void UpdateState(T data);
    }

    public interface IStateUpdate<T1, T2>
    {
        void UpdateState(T1 data1, T2 data2);
    }

    public interface IStateUpdate<T1, T2, T3>
    {
        void UpdateState(T1 data1, T2 data2, T3 data3);
    }
}
