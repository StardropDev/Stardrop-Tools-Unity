
using UnityEngine;

namespace StardropTools
{
    public class WorldObject : BaseComponent
    {
        public Transform Parent
        {
            get => Transform.parent;
            set => Transform.SetParent(value);
        }

        public Vector3 Position
        {
            get => Transform.position;
            set => Transform.position = value;
        }

        public Vector3 LocalPosition
        {
            get => Transform.localPosition;
            set => Transform.localPosition = value;
        }

        public Quaternion Rotation
        {
            get => Transform.rotation;
            set => Transform.rotation = value;
        }

        public Quaternion LocalRotation
        {
            get => Transform.localRotation;
            set => Transform.localRotation = value;
        }

        public Vector3 EulerAngles
        {
            get => Transform.eulerAngles;
            set => Transform.eulerAngles = value;
        }

        public Vector3 LocalEulerAngles
        {
            get => Transform.localEulerAngles;
            set => Transform.localEulerAngles = value;
        }

        public Vector3 Scale
        {
            get => Transform.localScale;
            set => Transform.localScale = value;
        }

        public Vector3 Forward => Transform.forward;
        public Vector3 Backward => -Transform.forward;

        public Vector3 Right => Transform.right;
        public Vector3 Left => -Transform.right;

        public Vector3 Up => Transform.up;
        public Vector3 Down => -Transform.up;



        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            Transform.SetPositionAndRotation(position, rotation);
        }

        public void SetLocalPositionAndRotation(Vector3 localPosition, Quaternion localRotation)
        {
            Transform.SetLocalPositionAndRotation(localPosition, localRotation);
        }



        public void LookAt(Vector3 target)
        {
            Transform.LookAt(target);
        }

        public void LookAt(Transform target)
        {
            Transform.LookAt(target);
        }
    }
}