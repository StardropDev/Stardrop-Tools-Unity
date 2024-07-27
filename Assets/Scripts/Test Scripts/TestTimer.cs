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
        //timer?.Stop();

        timer = new Timer(duration, delay, loopType);
        timer.Play(() =>
        {
            print("Timer complete!");
        });

        var anotherTimer = new Timer(duration + 1, 0, loopType);
        anotherTimer.Play(() =>
        {
            print("Another timer complete!");
        });
    }

    private void Update()
    {
        if (timer != null && timer.IsPlaying)
        {
            print($"Time: {timer?.DeltaTime}, Duration: {timer?.DeltaTime}, Percent: {timer?.Percent}");
        }
    }
}
