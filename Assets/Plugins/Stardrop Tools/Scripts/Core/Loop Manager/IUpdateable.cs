
namespace StardropTools
{
    public interface IUpdateable
    {
        void StartUpdate();

        void HandleUpdate();

        void StopUpdate();
    }
}
