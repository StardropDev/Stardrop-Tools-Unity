using UnityEngine;
using System;

namespace StardropTools.Tween
{
    public class TweenBezier : TweenComponentValues<Transform, Vector3>
    {
        private Vector3[] controlPoints;
        private Vector3[] bezierPoints;
        private float smoothness;

        public TweenBezier(Transform targetComponent, Vector3[] controlPoints, float smoothness, float duration)
            : base(targetComponent, controlPoints[0], controlPoints[controlPoints.Length - 1])
        {
            this.controlPoints = controlPoints;
            this.smoothness = smoothness;
            SetDuration(duration);

            bezierPoints = Utils.SmoothCurve(controlPoints, smoothness);
        }

        public TweenBezier(int id, Transform targetComponent, Vector3[] controlPoints, float smoothness, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, Action onCompleteCallback = null, Action<Vector3> onValueChangedCallback = null)
            : base(id, targetComponent, controlPoints[0], controlPoints[controlPoints.Length - 1], duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
            this.controlPoints = controlPoints;
            this.smoothness = smoothness;

            bezierPoints = Utils.SmoothCurve(controlPoints, smoothness);
        }

        protected override Vector3 Interpolate(Vector3 startValue, Vector3 endValue, float percent)
        {
            // Ensure percent is within the range [0, 1]
            percent = Mathf.Clamp01(percent);

            // Interpolate smoothly between the bezier points
            float scaledPercent = percent * (bezierPoints.Length - 1);
            int index = Mathf.Clamp(Mathf.FloorToInt(scaledPercent), 0, bezierPoints.Length - 1);
            int nextIndex = Mathf.Clamp(index + 1, 0, bezierPoints.Length - 1);
            float lerpFactor = scaledPercent - index;

            return Vector3.Lerp(bezierPoints[index], bezierPoints[nextIndex], lerpFactor);
        }

        protected override void ApplyValue(Transform component, Vector3 value)
        {
            component.position = value;
        }
    }
}
