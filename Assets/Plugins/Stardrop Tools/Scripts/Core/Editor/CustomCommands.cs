

namespace StardropTools.CustomCommands
{
#if UNITY_EDITOR

    using System;
    using System.Reflection;
    using UnityEngine;
    using UnityEditor;

    // % = control
    // # = shift
    // & = alt
    // https://docs.unity3d.com/ScriptReference/MenuItem.html

    [InitializeOnLoadAttribute]
    public static class CustomCommands
    {
        public static bool _cmdsActive = true;
        const string customCommands = "Stardrop Tools/Commands/";

        static CustomCommands()
        {
            EditorApplication.playModeStateChanged += StateCommandEnabler;
        }

        static void StateCommandEnabler(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
                EnableCommands(false);
            else if (state == PlayModeStateChange.ExitingPlayMode)
                EnableCommands(true);
        }

        // Enable
        // =============================================== Enable 
        [MenuItem(customCommands + "/Activate Commands %#a")]
        static void EnableCommands()
        {
            if (_cmdsActive == false)
            {
                Debug.Log("<color=green>Commands Activated</color>");
                _cmdsActive = true;
            }

            else if (_cmdsActive == true)
            {
                Debug.Log("<color=orange>Commands Deactivated</color>");
                _cmdsActive = false;
            }
        }

        #region Aditional Enable & Disable Commands
        public static void EnableCommands(bool val)
        {
            if (val == true && _cmdsActive == false)
            {
                Debug.Log("<color=green>Commands Activated</color>");
                _cmdsActive = true;
            }

            else if (val == false && _cmdsActive == true)
            {
                Debug.Log("<color=orange>Commands Deactivated</color>");
                _cmdsActive = false;
            }
        }
        #endregion //enable disable


        // Activate or Deactivate Objects
        // =============================================== Activate or Deactivate Objects
        [MenuItem(customCommands + "/Activate or Deactivate _a")]
        static void ActivateSelection()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                int _active = 0;
                int _objectCount = 0;

                // Count how many objects there are
                foreach (GameObject go in objs)
                {
                    if (go.activeInHierarchy == true)
                        _active++;

                    _objectCount++;
                }

                // Get Active Percent
                float _activePercent = _active * 100 / _objectCount;
                //Debug.Log("Activate = " + _activePercent + "%");

                // Decide to activate or deactivate
                if (_activePercent >= 50)
                {
                    // Deativate
                    foreach (GameObject go in objs)
                        if (go.activeInHierarchy == true)
                        {
                            Undo.RecordObject(go, "Disabled MeshRenderer");
                            go.SetActive(false);
                        }
                }

                else
                {
                    // Activate
                    foreach (GameObject go in objs)
                        if (go.activeInHierarchy == false)
                        {
                            Undo.RecordObject(go, "Disabled MeshRenderer");
                            go.SetActive(true);
                        }
                }
            }
        }


        // % = control
        // # = shift
        // & = alt
        // Reset Position
        // =============================================== Reset Position
        [MenuItem(customCommands + "/Reset World Position &s")]
        static void ResetPos()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                foreach (GameObject go in objs)
                {
                    // reset RectsTransform
                    if (go.GetComponent<RectTransform>() == true)
                    {
                        Undo.RecordObject(go.transform, "Reset object position");
                        go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    }

                    // reset Transform
                    else
                    {
                        Undo.RecordObject(go.transform, "Reset object position");
                        go.transform.position = Vector3.zero;
                    }
                }
            }
        }


        // % = control
        // # = shift
        // & = alt
        // Reset Position
        // =============================================== Reset Position
        [MenuItem(customCommands + "/Reset Local Position #s")]
        static void ResetLocalPosi()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                foreach (GameObject go in objs)
                {
                    // reset RectsTransform
                    if (go.GetComponent<RectTransform>() == true)
                    {
                        Undo.RecordObject(go.transform, "Reset object position");
                        go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    }

                    // reset Transform
                    else
                    {
                        Undo.RecordObject(go.transform, "Reset object position");
                        go.transform.localPosition = Vector3.zero;
                    }
                }
            }
        }


        // % = control
        // # = shift
        // & = alt
        // Reset Rotation
        // =============================================== Reset Rotation
        [MenuItem(customCommands + "/Reset World Rotation #r")]
        static void ResetRotation()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                foreach (GameObject go in objs)
                {
                    // reset RectsTransform
                    if (go.GetComponent<RectTransform>() == true)
                    {
                        Undo.RecordObject(go.transform, "Reset object Rotation");
                        go.GetComponent<RectTransform>().rotation = Quaternion.identity;
                    }

                    // reset Transform
                    else
                    {
                        Undo.RecordObject(go.transform, "Reset object Rotation");
                        go.transform.rotation = Quaternion.identity;
                    }
                }
            }
        }


        // % = control
        // # = shift
        // & = alt
        // Reset Scale
        // =============================================== Reset Scale
        [MenuItem(customCommands + "/Reset Scale &r")]
        static void ResetScale()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                foreach (GameObject go in objs)
                {
                    if (go.GetComponent<RectTransform>() == true)
                    {
                        Undo.RecordObject(go.transform, "Reset object local scale");
                        go.GetComponent<RectTransform>().localScale = Vector3.one;
                    }

                    else
                    {
                        Undo.RecordObject(go.transform, "Reset object local scale");
                        go.transform.localScale = Vector3.one;
                    }
                }
            }
        }


        // Enable & Disable MeshRenderer
        // =============================================== Enable & Disable MeshRenderer
        [MenuItem(customCommands + "/Enable or Disable MeshRenderer _d")]
        static void EnableMeshRenderer()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                int _enabled = 0;
                int _objectCount = 0;

                // Count how many objects there are
                foreach (GameObject go in objs)
                {
                    if (go.GetComponent<MeshRenderer>() == true)
                    {
                        if (go.GetComponent<MeshRenderer>().enabled == true)
                            _enabled++;

                        _objectCount++;
                    }
                }

                if (_objectCount == 0)
                    return;

                // Get Active Percent
                float _enablePercent = _enabled * 100 / _objectCount;
                //Debug.Log("Enabled = " + _enablePercent + "%");

                // Decide to activate or deactivate
                if (_enablePercent >= 50)
                {
                    // Disable
                    foreach (GameObject go in objs)
                    {
                        if (go.GetComponent<MeshRenderer>() == true)
                        {
                            MeshRenderer _meshRend = go.GetComponent<MeshRenderer>();

                            if (_meshRend.enabled == true)
                            {
                                Undo.RecordObject(_meshRend, "Disabled MeshRenderer");
                                _meshRend.enabled = false;
                            }
                        }
                    }
                }

                else
                {
                    // Enable
                    foreach (GameObject go in objs)
                    {
                        if (go.GetComponent<MeshRenderer>() == true)
                        {
                            MeshRenderer _meshRend = go.GetComponent<MeshRenderer>();

                            if (_meshRend.enabled == false)
                            {
                                Undo.RecordObject(_meshRend, "Enabled MeshRenderer");
                                _meshRend.enabled = true;
                            }
                        }
                    }
                }
            }
        }



        // Enable & Disable Colliders
        // =============================================== Enable & Disable Colliders
        [MenuItem(customCommands + "/Enable or Disable Colliders #c")] // c
        static void EnableColliders()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                int _enabled = 0;
                int _objectCount = 0;

                // Count how many objects there are
                foreach (GameObject go in objs)
                {
                    if (go.GetComponent<Collider>() == true)
                    {
                        if (go.GetComponent<Collider>().enabled == true)
                            _enabled++;

                        _objectCount++;
                    }
                }

                if (_objectCount == 0)
                    return;

                // Get Active Percent
                float _enablePercent = _enabled * 100 / _objectCount;
                //Debug.Log("Enabled = " + _enablePercent + "%");

                // Disable
                if (_enablePercent >= 50)
                {
                    foreach (GameObject go in objs)
                    {
                        if (go.GetComponent<Collider>() == true)
                        {
                            Collider _collider = go.GetComponent<Collider>();

                            if (_collider.enabled == true)
                            {
                                Undo.RecordObject(_collider, "Disabled Collider");
                                _collider.enabled = false;
                            }
                        }
                    }
                }

                // Enable
                else
                {
                    foreach (GameObject go in objs)
                    {
                        if (go.GetComponent<MeshRenderer>() == true)
                        {
                            Collider _collider = go.GetComponent<Collider>();

                            if (_collider.enabled == false)
                            {
                                Undo.RecordObject(_collider, "Enabled Collider");
                                _collider.enabled = true;
                            }
                        }
                    }
                }
            }
        }



        // Set Selected Image Opacity all different
        // =============================================== Image Opacity Chain
        [MenuItem(customCommands + "/Image Opacity Chain #i")] // shift + i
        static void ImageOpacityChain()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                int _objectCount = 0;

                // Count how many objects there are
                foreach (GameObject go in objs)
                {
                    if (go.GetComponent<UnityEngine.UI.Image>() == true)
                        go.GetComponent<UnityEngine.UI.Image>().enabled = true;

                    else
                        go.AddComponent<UnityEngine.UI.Image>();

                    _objectCount++;
                }

                float a = .75f;
                float d = a / (float)_objectCount;

                int counter = 0;
                foreach (GameObject go in objs)
                {
                    var img = go.GetComponent<UnityEngine.UI.Image>();

                    if (img == true && img.enabled == true)
                    {
                        Color color = img.color;
                        color.a = a;
                        a -= d;

                        img.color = color;

                        img.gameObject.SetActive(false);
                        img.gameObject.SetActive(true);
                        counter++;
                    }
                }

                Debug.Log("<color=cyan> Opacity Chain Complete </color>");
            }
        }


        // Inverse XYZ Positions of selected objects
        // =============================================== Inverse Axis
        [MenuItem(customCommands + "/Inverse X Axis #x")] // shift + x
        static void InverseX()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                foreach (GameObject go in objs)
                {
                    Undo.RecordObject(go.transform, "Inverse Axis");

                    var position = go.transform.position;
                    position.x *= -1;
                    go.transform.position = position;
                }
            }
        }


        [MenuItem(customCommands + "/Inverse Y Axis #y")] // shift + y
        static void InverseY()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                foreach (GameObject go in objs)
                {
                    Undo.RecordObject(go.transform, "Inverse Axis");

                    var position = go.transform.position;
                    position.y *= -1;
                    go.transform.position = position;
                }
            }
        }


        [MenuItem(customCommands + "/Inverse Z Axis #z")] // shift + z
        static void InverseZ()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                foreach (GameObject go in objs)
                {
                    Undo.RecordObject(go.transform, "Inverse Axis");
                    
                    var position = go.transform.position;
                    position.z *= -1;
                    go.transform.position = position;
                }
            }
        }


        [MenuItem(customCommands + "/Delete Object Children %#d")] // Ctrl + Shift + Del
        static void DeleteChildrenOfSelecteddObject()
        {
            if (Selection.activeTransform != null && _cmdsActive)
            {
                GameObject[] objs;
                objs = Selection.gameObjects;

                foreach (GameObject go in objs)
                {
                    Undo.RecordObject(go.transform, "Delete Children");

                    foreach (Transform child in go.transform)
                        GameObject.DestroyImmediate(child.gameObject);
                }
            }
        }

        // Method to rename animations to the name of the FBX file
        [MenuItem(customCommands + "/Rename Animations to FBX Name %&r", validate = false)] // ctrl + alt + r
        static void RenameAnimationsToFBXName()
        {
            if (Application.isPlaying)
            {
                Debug.LogWarning("This command cannot be executed in play mode.");
                return;
            }

            UnityEngine.Object[] selectedObjects = Selection.objects;
            foreach (var obj in selectedObjects)
            {
                if (obj is GameObject)
                {
                    string assetPath = AssetDatabase.GetAssetPath(obj);
                    if (assetPath.EndsWith(".fbx", StringComparison.OrdinalIgnoreCase))
                    {
                        string fbxName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
                        ModelImporter modelImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;
                        if (modelImporter != null)
                        {
                            ModelImporterClipAnimation[] clipAnimations = modelImporter.clipAnimations;

                            if (clipAnimations.Length == 0)
                            {
                                clipAnimations = modelImporter.defaultClipAnimations;
                            }

                            bool updated = false;
                            foreach (var clip in clipAnimations)
                            {
                                if (clip.name != fbxName)
                                {
                                    clip.name = fbxName;
                                    updated = true;
                                }
                            }

                            if (updated)
                            {
                                modelImporter.clipAnimations = clipAnimations;
                                AssetDatabase.WriteImportSettingsIfDirty(assetPath);
                                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                                Debug.Log($"Renamed animation to {fbxName} for {assetPath}");
                            }
                        }
                    }
                }
            }

            AssetDatabase.Refresh();
        }

        // taken from https://forum.unity.com/threads/shortcut-key-for-lock-inspector.95815/
        [MenuItem(customCommands + "/Toggle Inspector Lock #w")] // shift + w
        static void ToggleInspectorLock() // Inspector must be inspecting something to be locked
        {
            EditorWindow inspectorToBeLocked = EditorWindow.mouseOverWindow; // "EditorWindow.focusedWindow" can be used instead

            if (inspectorToBeLocked != null && inspectorToBeLocked.GetType().Name == "InspectorWindow")
            {
                Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
                PropertyInfo propertyInfo = type.GetProperty("isLocked");
                bool value = (bool)propertyInfo.GetValue(inspectorToBeLocked, null);
                propertyInfo.SetValue(inspectorToBeLocked, !value, null);
                inspectorToBeLocked.Repaint();

                Debug.Log($"<Color=white>Inspector Locked:</color> <color=cyan>{!value}</color>");
            }
        }

        [MenuItem(customCommands + "/Toggle Inspector Debug #%w")] // shift + ctrl + W
        static void ToggleDebugMode()
        {
            EditorWindow inspectorWindow = EditorWindow.mouseOverWindow; // "EditorWindow.focusedWindow" can be used instead

            if (inspectorWindow != null && inspectorWindow.GetType().Name == "InspectorWindow")
            {
                // Access the InspectorWindow type
                Type inspectorType = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");

                // Get the "mode" field, which determines the current mode (Normal or Debug)
                FieldInfo modeField = inspectorType.GetField("m_InspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
                int mode = (int)modeField.GetValue(inspectorWindow);

                // Toggle the mode between 0 (Normal) and 1 (Debug)
                int newMode = mode == 0 ? 1 : 0;
                modeField.SetValue(inspectorWindow, newMode);

                // Call the SetMode method to apply the change
                MethodInfo setModeMethod = inspectorType.GetMethod("SetMode", BindingFlags.NonPublic | BindingFlags.Instance);
                setModeMethod.Invoke(inspectorWindow, new object[] { newMode });

                inspectorWindow.Repaint();

                Debug.LogFormat($"<Color=white>Inspector Debug Mode:</Color> <Color=magenta>{newMode == 1}</Color>");
            }
        }
    }
#endif
}