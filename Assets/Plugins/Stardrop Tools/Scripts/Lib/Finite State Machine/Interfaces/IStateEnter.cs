
namespace StardropTools.FiniteStateMachines
{
    public interface IStateEnter
    {
        void EnterState();
    }

    public interface IStateEnter<T>
    {
        void EnterState(T data);
    }

    public interface IStateEnter<T1, T2>
    {
        void EnterState(T1 data1, T2 data2);
    }
}
