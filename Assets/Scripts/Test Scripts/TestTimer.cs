using StardropTools;
using UnityEngine;

public class TestTimer : MonoBehaviour
{
    [SerializeField] float duration = .5f;
    [SerializeField] float delay = 0;
    [SerializeField] LoopType loopType = LoopType.None;

    private Timer timer;

    [NaughtyAttributes.Button("Create Timer")]
    public void CreateTimer()
    {
        timer.Stop();

        timer = new Timer(duration, delay, loopType);
        timer.Play(() =>
        {
            print("Timer complete!");
        },
        (float time) =>
        {
            print($"Timer duration: {timer.Duration}, Time: {time}");
        }
        );

        var anotherTimer = new Timer(duration + 1, 0, loopType);
        anotherTimer.Play(() =>
        {
            print("Another timer complete!");
        });
    }
}
