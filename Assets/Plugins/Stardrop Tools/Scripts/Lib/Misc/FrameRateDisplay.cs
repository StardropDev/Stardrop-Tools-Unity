using UnityEngine;

public class FrameRateDisplay : MonoBehaviour
{
    private float deltaTime = 0.0f;

    [SerializeField] private int fontSize = 20;
    [SerializeField] private TextAnchor alignment = TextAnchor.UpperLeft;
    [SerializeField] private Color textColor = Color.white;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        float x = 0, y = 0;

        switch (alignment)
        {
            case TextAnchor.UpperLeft:
                x = 0;
                y = 0;
                break;
            case TextAnchor.UpperRight:
                x = w - fontSize * 10;
                y = 0;
                break;
            case TextAnchor.LowerLeft:
                x = 0;
                y = h - fontSize * 2;
                break;
            case TextAnchor.LowerRight:
                x = w - fontSize * 10;
                y = h - fontSize * 2;
                break;
            case TextAnchor.UpperCenter:
                x = (w - fontSize * 10) / 2;
                y = 0;
                break;
            case TextAnchor.MiddleCenter:
                x = (w - fontSize * 10) / 2;
                y = (h - fontSize * 2) / 2;
                break;
            case TextAnchor.LowerCenter:
                x = (w - fontSize * 10) / 2;
                y = h - fontSize * 2;
                break;
            case TextAnchor.MiddleLeft:
                x = 0;
                y = (h - fontSize * 2) / 2;
                break;
            case TextAnchor.MiddleRight:
                x = w - fontSize * 10;
                y = (h - fontSize * 2) / 2;
                break;
        }

        Rect rect = new Rect(x, y, fontSize * 10, fontSize * 2);
        style.alignment = alignment;
        style.fontSize = fontSize;
        style.normal.textColor = textColor;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
