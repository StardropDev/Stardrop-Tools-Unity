using StardropTools;
using UnityEngine;

public class TestTween : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float duration = 0.5f;
    [SerializeField] float delay = 0;
    [SerializeField] EaseType easeType = EaseType.EaseInOutSine;
    [SerializeField] LoopType loopType = LoopType.None;
    [SerializeField] float distance = 8f;  // Editable distance for random position
    [SerializeField] Vector3 startEulerAngles = Vector3.zero;
    [SerializeField] Vector3 endEulerAngles = new Vector3(0, 180, 0);

    private TweenPosition tweenPosition;
    private TweenEulerAngles tweenEulerAngles;

    [NaughtyAttributes.Button("Create Position Tween")]
    public void CreatePositionTween()
    {
        if (target == null)
        {
            Debug.LogWarning("Target transform is not assigned.");
            return;
        }

        // Generate a random position around the world origin within the specified distance
        Vector3 randomPosition = new Vector3(
            Random.Range(-distance, distance),
            Random.Range(-distance, distance),
            Random.Range(-distance, distance)
        );

        tweenPosition = new TweenPosition(target, randomPosition)
            .SetDurationAndDelay(duration, delay)
            .SetEaseType(easeType)
            .SetLoopType(loopType, 0)
            .AsPosition;

        tweenPosition.SetOnValueChangedCallback(value =>
        {
            if (target != null)
                target.position = value;

            print($"Current Position: {value}");
        });

        tweenPosition.Play(() =>
        {
            print("Position Tween complete!");
        });
    }

    [NaughtyAttributes.Button("Create Euler Angles Tween")]
    public void CreateEulerAnglesTween()
    {
        if (target == null)
        {
            Debug.LogWarning("Target transform is not assigned.");
            return;
        }

        tweenEulerAngles = new TweenEulerAngles(target, startEulerAngles, endEulerAngles)
            .SetDurationAndDelay(duration, delay)
            .SetEaseType(easeType)
            .SetLoopType(loopType, 0)
            .AsEulerAngles;

        tweenEulerAngles.SetOnValueChangedCallback(value =>
        {
            if (target != null)
                target.eulerAngles = value;

            print($"Current Euler Angles: {value}");
        });

        tweenEulerAngles.Play(() =>
        {
            print("Euler Angles Tween complete!");
        });
    }

    private void Update()
    {
        if (tweenPosition != null && tweenPosition.IsPlaying)
        {
            print($"Position Tween - Time: {tweenPosition?.DeltaTime}, Percent: {tweenPosition?.Percent}, Eased Percent: {tweenPosition?.EasedPercent}, Current Value: {tweenPosition?.CurrentValue}");
        }

        if (tweenEulerAngles != null && tweenEulerAngles.IsPlaying)
        {
            print($"Euler Angles Tween - Time: {tweenEulerAngles?.DeltaTime}, Percent: {tweenEulerAngles?.Percent}, Eased Percent: {tweenEulerAngles?.EasedPercent}, Current Value: {tweenEulerAngles?.CurrentValue}");
        }
    }
}
