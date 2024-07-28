
namespace StardropTools.FiniteStateMachines
{
    public interface IStateExit
    {
        void ExitState();
    }

    public interface IStateExit<T>
    {
        void ExitState(T data);
    }

    public interface IStateExit<T1, T2>
    {
        void ExitState(T1 data1, T2 data2);
    }
}
