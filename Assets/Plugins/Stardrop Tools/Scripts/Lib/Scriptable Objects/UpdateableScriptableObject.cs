
using UnityEngine;

namespace StardropTools
{
    public abstract class UpdateableScriptableObject : ScriptableObject, IUpdateable
    {
        public UpdateData updateData;
        public float updateDuration;

        public void StartUpdate()
        {
            LoopManager.AddToUpdate(this, updateData.updateFrequency, updateDuration, updateData.updateType);
        }

        public void StopUpdate()
        {
            LoopManager.RemoveFromUpdate(this);
        }

        public abstract void HandleUpdate();
    }
}