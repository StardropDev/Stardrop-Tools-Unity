#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(fileName = "TweenComponentValueGenerator", menuName = "Stardrop Tools/Tween/Tween Component Value Generator")]
    public class TweenComponentValueGenerator : ScriptableObject
    {
        [Header("Configuration")]
        [SerializeField] private string directory = "Assets/Plugins/Stardrop Tools/Scripts/Lib/Tween/Generated Tweens";
        [SerializeField] private string classTitle = "TransformPosition";
        [SerializeField] private string component = "Transform";
        [SerializeField] private string valueType = "Vector3";
        [SerializeField] private string property = "position";

        [ContextMenu("Generate Tween Script")]
        [NaughtyAttributes.Button("Generate Tween Script")]
        public void GenerateTweenScript()
        {
            string className = $"Tween{classTitle}";
            string filePath = Path.Combine(directory, $"{className}.cs");

            string scriptContent = $@"
using UnityEngine;

namespace StardropTools
{{
    public class {className} : TweenComponentValues<{component}, {valueType}>
    {{
        public {className}({component} targetComponent, {valueType} endValue) : base(targetComponent, endValue)
        {{
            startValue = targetComponent.{property};
        }}

        public {className}({component} targetComponent, {valueType} startValue, {valueType} endValue)
            : base(targetComponent, startValue, endValue)
        {{
        }}

        public {className}(int id, {component} targetComponent, {valueType} startValue, {valueType} endValue)
            : base(id, targetComponent, startValue, endValue)
        {{
        }}

        public {className}(int id, {component} targetComponent, {valueType} startValue, {valueType} endValue, float duration, float delay = 0, EaseType easeType = EaseType.EaseInOutSine, LoopType loopType = LoopType.None, int loopCount = 0, AnimationCurve animationCurve = null, System.Action onCompleteCallback = null, System.Action<{valueType}> onValueChangedCallback = null)
            : base(id, targetComponent, startValue, endValue, duration, delay, easeType, loopType, loopCount, animationCurve, onCompleteCallback, onValueChangedCallback)
        {{
        }}

        protected override {valueType} Interpolate({valueType} startValue, {valueType} endValue, float percent)
        {{
            return {GetLerpMethod(valueType)}(startValue, endValue, percent);
        }}

        protected override void ApplyValue({component} component, {valueType} value)
        {{
            component.{property} = value;
        }}
    }}
}}
";

            Directory.CreateDirectory(directory);
            File.WriteAllText(filePath, scriptContent);
            AssetDatabase.Refresh();

            Debug.Log($"Tween script generated at {filePath}");
        }

        private string GetLerpMethod(string valueType)
        {
            switch (valueType)
            {
                case "float":
                    return "Mathf.LerpUnclamped";
                case "Vector2":
                    return "Vector2.LerpUnclamped";
                case "Vector3":
                    return "Vector3.LerpUnclamped";
                case "Vector4":
                    return "Vector4.LerpUnclamped";
                case "Quaternion":
                    return "Quaternion.SlerpUnclamped";
                case "Color":
                    return "Color.LerpUnclamped";
                default:
                    throw new System.ArgumentException($"Unsupported value type: {valueType}");
            }
        }
    }
}
#endif
