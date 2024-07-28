
namespace StardropTools.FiniteStateMachines
{
    public interface IState : IStateEnter, IStateExit
    {

    }

    public interface IState<T> : IStateEnter<T>, IStateExit<T>
    {

    }

    public interface IState<T1, T2> : IStateEnter<T1, T2>, IStateExit<T1, T2>
    {

    }
}
