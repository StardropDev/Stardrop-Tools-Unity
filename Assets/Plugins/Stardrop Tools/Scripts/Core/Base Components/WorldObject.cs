using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Base class for all Transform intensive objects that exist in the world.
    /// </summary>
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

        public void LookAt(Vector3 target, Vector3 worldUp)
        {
            Transform.LookAt(target, worldUp);
        }

        public void LookAt(Transform target, Vector3 worldUp)
        {
            Transform.LookAt(target, worldUp);
        }

        private void SmoothLookAt(Transform observer, Vector3 direction, float speed)
        {
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                observer.rotation = Quaternion.Slerp(observer.rotation, targetRotation, speed * Time.deltaTime);
            }
        }

        public void SetParent(Transform parent, bool worldPositionStays)
        {
            Transform.SetParent(parent, worldPositionStays);
        }

        public void Translate(Vector3 translation, Space space = Space.Self)
        {
            Transform.Translate(translation, space);
        }

        public void Rotate(Vector3 eulerAngles, Space space = Space.Self)
        {
            Transform.Rotate(eulerAngles, space);
        }

        public void ResetTransform()
        {
            Transform.position = Vector3.zero;
            Transform.rotation = Quaternion.identity;
            Transform.localScale = Vector3.one;
        }

        public void CopyTransform(Transform source)
        {
            Transform.position = source.position;
            Transform.rotation = source.rotation;
            Transform.localScale = source.localScale;
        }

        public Transform GetChild(int index)
        {
            return Transform.GetChild(index);
        }

        public Transform GetChildByName(string name)
        {
            foreach (Transform child in Transform)
            {
                if (child.name == name)
                {
                    return child;
                }
            }
            return null;
        }
    }
}
