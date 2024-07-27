
using UnityEngine;

namespace StardropTools
{
    public class UIObject : WorldObject
    {
        protected RectTransform cachedRectTransform;

        public RectTransform RectTransform => cachedRectTransform != null ? cachedRectTransform : cachedRectTransform = GetComponent<RectTransform>();
    }
}