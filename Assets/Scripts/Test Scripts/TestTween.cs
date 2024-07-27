using StardropTools;
using StardropTools.Tween;
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
    [SerializeField] float bezierSmoothness = 10f;
    [SerializeField] float bezierHeight = 5f;
    [SerializeField] float bezierSpeed = 1f;  // Speed for the Bezier speed tween

    private TweenPosition tweenPosition;
    private TweenEulerAngles tweenEulerAngles;
    private TweenBezier tweenBezier;
    private TweenBezierSpeed tweenBezierSpeed;

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

    [NaughtyAttributes.Button("Create Bezier Tween")]
    public void CreateBezierTween()
    {
        if (target == null)
        {
            Debug.LogWarning("Target transform is not assigned.");
            return;
        }

        // Generate Bezier control points
        Vector3 startPosition = target.position;
        Vector3 endPosition = new Vector3(
            Random.Range(-distance, distance),
            Random.Range(-distance, distance),
            Random.Range(-distance, distance)
        );

        var bezierControlPoints = new Vector3[3];
        bezierControlPoints[0] = startPosition;
        bezierControlPoints[1] = (startPosition + endPosition) / 2 + Vector3.up * bezierHeight;
        bezierControlPoints[2] = endPosition;

        tweenBezier = new TweenBezier(target, bezierControlPoints, bezierSmoothness, duration)
            .SetEaseType(easeType)
            .SetLoopType(loopType, 0)
            .AsBezier;

        tweenBezier.Play(() =>
        {
            print("Bezier Tween complete!");
        });
    }

    [NaughtyAttributes.Button("Create Bezier Speed Tween")]
    public void CreateBezierSpeedTween()
    {
        if (target == null)
        {
            Debug.LogWarning("Target transform is not assigned.");
            return;
        }

        // Generate Bezier control points
        Vector3 startPosition = target.position;
        Vector3 endPosition = new Vector3(
            Random.Range(-distance, distance),
            Random.Range(-distance, distance),
            Random.Range(-distance, distance)
        );

        var bezierControlPoints = new Vector3[3];
        bezierControlPoints[0] = startPosition;
        bezierControlPoints[1] = (startPosition + endPosition) / 2 + Vector3.up * bezierHeight;
        bezierControlPoints[2] = endPosition;

        tweenBezierSpeed = new TweenBezierSpeed(target, bezierControlPoints, bezierSmoothness, bezierSpeed)
            .SetEaseType(easeType)
            .SetLoopType(loopType, 0)
            .AsBezierSpeed;

        tweenBezierSpeed.Play(() =>
        {
            print("Bezier Speed Tween complete!");
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

        if (tweenBezier != null && tweenBezier.IsPlaying)
        {
            print($"Bezier Tween - Time: {tweenBezier?.DeltaTime}, Percent: {tweenBezier?.Percent}, Eased Percent: {tweenBezier?.EasedPercent}, Current Value: {tweenBezier?.CurrentValue}");
        }

        if (tweenBezierSpeed != null && tweenBezierSpeed.IsPlaying)
        {
            print($"Bezier Speed Tween - Time: {tweenBezierSpeed?.DeltaTime}, Percent: {tweenBezierSpeed?.Percent}, Eased Percent: {tweenBezierSpeed?.EasedPercent}, Current Value: {tweenBezierSpeed?.CurrentValue}");
        }
    }
}
