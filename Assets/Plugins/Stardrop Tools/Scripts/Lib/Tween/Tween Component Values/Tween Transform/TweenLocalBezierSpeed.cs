using System;
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Tween for local position movement along a bezier curve with speed control.
    /// </summary>
    public class TweenLocalBezierSpeed : TweenComponentValues<Transform, Vector3>
    {
        private Vector3[] controlPoints;
        private Vector3[] bezierPoints;
        private float smoothness;
        private float speed;
        private float distance;
        private float traveledDistance;

        public TweenLocalBezierSpeed(Transform targetComponent, Vector3[] controlPoints, float smoothness, float speed)
            : base(targetComponent, controlPoints[0], controlPoints[controlPoints.Length - 1])
        {
            this.controlPoints = controlPoints;
            this.smoothness = smoothness;
            this.speed = speed;

            bezierPoints = Utils.SmoothCurve(controlPoints, smoothness);
            CalculateTotalDistance();

            // Set pseudo-duration based on total distance and speed
            float pseudoDuration = distance / speed;
            SetDuration(pseudoDuration);
        }

        public TweenLocalBezierSpeed(int id, Transform targetComponent, Vector3[] controlPoints, float smoothness, float speed, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, Action onCompleteCallback = null, Action<Vector3> onValueChangedCallback = null)
            : base(id, targetComponent, controlPoints[0], controlPoints[controlPoints.Length - 1], 0, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {
            this.controlPoints = controlPoints;
            this.smoothness = smoothness;
            this.speed = speed;

            bezierPoints = Utils.SmoothCurve(controlPoints, smoothness);
            CalculateTotalDistance();

            // Set pseudo-duration based on total distance and speed
            float pseudoDuration = distance / speed;
            SetDuration(pseudoDuration);
        }

        private void CalculateTotalDistance()
        {
            distance = 0;
            for (int i = 0; i < bezierPoints.Length - 1; i++)
            {
                distance += Vector3.Distance(bezierPoints[i], bezierPoints[i + 1]);
            }
        }

        protected override Vector3 Interpolate(Vector3 startValue, Vector3 endValue, float percent)
        {
            traveledDistance = Mathf.Clamp(traveledDistance + speed * Time.deltaTime, 0, distance);
            float totalPercent = traveledDistance / distance;

            // Ensure totalPercent is within the range [0, 1]
            totalPercent = Mathf.Clamp01(totalPercent);

            // Interpolate smoothly between the bezier points
            float scaledPercent = totalPercent * (bezierPoints.Length - 1);
            int index = Mathf.Clamp(Mathf.FloorToInt(scaledPercent), 0, bezierPoints.Length - 1);
            int nextIndex = Mathf.Clamp(index + 1, 0, bezierPoints.Length - 1);
            float lerpFactor = scaledPercent - index;

            return Vector3.Lerp(bezierPoints[index], bezierPoints[nextIndex], lerpFactor);
        }

        protected override void ApplyValue(Transform component, Vector3 value)
        {
            component.localPosition = value;
        }

        protected override void Loop()
        {
            base.Loop();
            traveledDistance = 0;
        }

        protected override void PingPong()
        {
            base.PingPong();
            traveledDistance = 0;
            Array.Reverse(bezierPoints);
        }
    }
}
