
namespace StardropTools.FiniteStateMachines
{
    public interface IBaseState<TStateData> : IState<TStateData>, IStateUpdate<float, TStateData>
    {
        int ID { get; }
        string Name { get; }

        bool ChangeState(int stateID, TStateData data);
        bool ChangeState(string stateName, TStateData data);
    }
}
