
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public static class Utils
    {
        /// <summary>
        /// Creates a smooth Curve based on provided anchor points (At least 3 points needed)
        /// </summary>
        public static Vector3[] SmoothCurve(Vector3[] arrayToCurve, float smoothness)
        {
            if (arrayToCurve.Length < 3)
            {
                Debug.Log("Curve requires at least 3 anchor points");
                return null;
            }

            List<Vector3> points;
            List<Vector3> curvedPoints;
            int pointsLength = 0;
            int curvedLength = 0;

            if (smoothness < 1.0f) smoothness = 1.0f;

            pointsLength = arrayToCurve.Length;

            curvedLength = pointsLength * Mathf.RoundToInt(smoothness) - 1;
            curvedPoints = new List<Vector3>(curvedLength);

            float t = 0.0f;
            for (int pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
            {
                t = Mathf.InverseLerp(0, curvedLength, pointInTimeOnCurve);

                points = new List<Vector3>(arrayToCurve);

                for (int j = pointsLength - 1; j > 0; j--)
                {
                    for (int i = 0; i < j; i++)
                        points[i] = (1 - t) * points[i] + t * points[i + 1];
                }

                curvedPoints.Add(points[0]);
            }

            curvedPoints.Add(arrayToCurve[arrayToCurve.Length - 1]);
            return curvedPoints.ToArray();
        }
    }
}
