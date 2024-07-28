
namespace StardropTools.FiniteStateMachines
{
    public interface IStateInput
    {
        void HandleInput();
    }

    public interface IStateInput<T>
    {
        void HandleInput(T data);
    }

    public interface IStateInput<T1, T2>
    {
        void HandleInput(T1 data1, T2 data2);
    }
}
