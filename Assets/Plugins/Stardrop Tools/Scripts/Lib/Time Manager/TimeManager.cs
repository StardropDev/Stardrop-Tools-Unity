
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public static class TimeManager
    {
        private static Dictionary<string, float> timeScales = new Dictionary<string, float>();

        public static void SetTimeScale(string key, float timeScale)
        {
            if (timeScales.ContainsKey(key))
            {
                timeScales[key] = timeScale;
            }
            else
            {
                timeScales.Add(key, timeScale);
            }
        }

        public static void RemoveTimeScale(string key)
        {
            if (timeScales.ContainsKey(key))
            {
                timeScales.Remove(key);
            }
        }

        public static float GetTimeScale(string key)
        {
            if (timeScales.ContainsKey(key))
            {
                return timeScales[key];
            }
            else
            {
                return 1f;
            }
        }

        public static float GetDeltaTime(string key)
        {
            return Time.deltaTime * GetTimeScale(key);
        }

        public static float GetFixedDeltaTime(string key)
        {
            return Time.fixedDeltaTime * GetTimeScale(key);
        }
    }
}
