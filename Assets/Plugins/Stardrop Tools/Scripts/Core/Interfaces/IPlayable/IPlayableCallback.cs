
namespace StardropTools
{
    public interface IPlayableCallback
    {
        void Play(System.Action onCompleteCallback);
    }

    public interface IPlayableCallback<T>
    {
        T Play(System.Action onCompleteCallback);
    }
}
