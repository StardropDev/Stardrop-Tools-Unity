
namespace StardropTools.FiniteStateMachines
{
    public interface IStateFixedUpdate
    {
        void FixedUpdateState();
    }

    public interface IStateFixedUpdate<T>
    {
        void FixedUpdateState(T data);
    }
}
