using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class LoopManager : Singleton<LoopManager>
    {
        [NaughtyAttributes.ShowNonSerializedField] private static int updateCount;
        [NaughtyAttributes.ShowNonSerializedField] private static int fixedUpdateCount;
        [NaughtyAttributes.ShowNonSerializedField] private static int lateUpdateCount;

        static List<IUpdateable> updateList;
        static List<IUpdateable> fixedUpdateList;
        static List<IUpdateable> lateUpdateList;

        static Dictionary<IUpdateable, UpdateData> updateData;
        static Dictionary<IUpdateable, float> updateDurations;
        static List<UpdateData> dataToRemove;

        public override void Initialize()
        {
            base.Initialize();

            updateList = new List<IUpdateable>();
            fixedUpdateList = new List<IUpdateable>();
            lateUpdateList = new List<IUpdateable>();
            updateData = new Dictionary<IUpdateable, UpdateData>();
            updateDurations = new Dictionary<IUpdateable, float>();
            dataToRemove = new List<UpdateData>();
        }

        private void Update()
        {
            if (!IsInitialized || updateList.Count == 0)
                return;

            float currentTime = Time.time;
            for (int i = 0; i < updateList.Count; i++)
            {
                var updateable = updateList[i];

                try
                {
                    var data = updateData[updateable];

                    if (data.IsScheduledForRemoval)
                        continue;

                    if (data.nextUpdateTime <= currentTime)
                    {
                        updateable.HandleUpdate();
                        if (data.updateFrequency > 0)
                            data.CalculateNextUpdateTime();

                        if (updateDurations.TryGetValue(updateable, out float duration) && duration > 0)
                        {
                            data.nextUpdateTime = currentTime + duration;
                            updateData[updateable] = data; // Ensure the updated data is stored back
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error in Updateable: " + e.Message);
                }
            }

            RemoveScheduledData();
        }

        private void FixedUpdate()
        {
            if (!IsInitialized || fixedUpdateList.Count == 0)
                return;

            float currentTime = Time.fixedTime;
            for (int i = 0; i < fixedUpdateList.Count; i++)
            {
                var updateable = fixedUpdateList[i];

                try
                {
                    var data = updateData[updateable];

                    if (data.IsScheduledForRemoval)
                        continue;

                    if (data.nextUpdateTime <= currentTime)
                    {
                        updateable.HandleUpdate();
                        if (data.updateFrequency > 0)
                            data.CalculateNextUpdateTime();

                        if (updateDurations.TryGetValue(updateable, out float duration) && duration > 0)
                        {
                            data.nextUpdateTime = currentTime + duration;
                            updateData[updateable] = data; // Ensure the updated data is stored back
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error in FixedUpdateable: " + e.Message);
                }
            }

            RemoveScheduledData();
        }

        private void LateUpdate()
        {
            if (!IsInitialized || lateUpdateList.Count == 0)
                return;

            float currentTime = Time.time;
            for (int i = 0; i < lateUpdateList.Count; i++)
            {
                var updateable = lateUpdateList[i];

                try
                {
                    var data = updateData[updateable];

                    if (data.IsScheduledForRemoval)
                        continue;

                    if (data.nextUpdateTime <= currentTime)
                    {
                        updateable.HandleUpdate();
                        if (data.updateFrequency > 0)
                            data.CalculateNextUpdateTime();

                        if (updateDurations.TryGetValue(updateable, out float duration) && duration > 0)
                        {
                            data.nextUpdateTime = currentTime + duration;
                            updateData[updateable] = data; // Ensure the updated data is stored back
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error in LateUpdateable: " + e.Message);
                }
            }

            RemoveScheduledData();
        }

        public static void AddToUpdate(IUpdateable updateable, float updateFrequency = 0, float updateDuration = 0, UpdateType updateType = UpdateType.Update)
        {
            if (updateData.ContainsKey(updateable))
            {
                Debug.LogWarning("Updateable already exists in the update list.");
                return;
            }

            if (updateFrequency < 0)
                updateFrequency = 0;

            updateData[updateable] = new UpdateData(updateFrequency, updateable, updateType);

            if (updateDuration > 0)
                updateDurations[updateable] = updateDuration;

            if (updateType == UpdateType.Update)
                updateList.Add(updateable);
            else if (updateType == UpdateType.FixedUpdate)
                fixedUpdateList.Add(updateable);
            else if (updateType == UpdateType.LateUpdate)
                lateUpdateList.Add(updateable);

            RefreshListCount();
        }

        public static void RemoveFromUpdate(IUpdateable updateable)
        {
            if (!updateData.ContainsKey(updateable))
            {
                Debug.LogWarning("Updateable does not exist in the update list.");
                return;
            }

            var data = updateData[updateable];
            data.IsScheduledForRemoval = true;

            if (data.updateType == UpdateType.Update)
                updateList.Remove(updateable);
            else if (data.updateType == UpdateType.FixedUpdate)
                fixedUpdateList.Remove(updateable);
            else if (data.updateType == UpdateType.LateUpdate)
                lateUpdateList.Remove(updateable);

            updateDurations.Remove(updateable);
            dataToRemove.Add(data);
            RefreshListCount();
        }

        static void RefreshListCount()
        {
            updateCount = updateList.Count;
            fixedUpdateCount = fixedUpdateList.Count;
            lateUpdateCount = lateUpdateList.Count;
        }

        private void RemoveScheduledData()
        {
            if (dataToRemove.Count == 0)
                return;

            foreach (var data in dataToRemove)
            {
                updateData.Remove(data.updateable);
            }
            dataToRemove.Clear();
        }
    }
}
