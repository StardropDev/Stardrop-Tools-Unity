using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StardropTools.CustomEditorWindows
{
    public class RenameAnimationsWindow : EditorWindow
    {
        private List<string> selectedFbxPaths = new List<string>();
        private List<UnityEngine.Object> selectedFbxObjects = new List<UnityEngine.Object>();
        private Vector2 scrollPosition;
        private bool isGridView = true;

        private UnityEngine.Object lastClickedObject;
        private string fbxName;
        private List<string> clipNames = new List<string>();
        private List<bool> clipLoops = new List<bool>();

        [MenuItem("Stardrop Tools/Open Rename Animations Window %#&a")] // ctrl + shift + alt + a
        public static void OpenWindow()
        {
            RenameAnimationsWindow window = GetWindow<RenameAnimationsWindow>("Rename Animations");
            window.Show();
        }

        private void OnEnable()
        {
            RefreshSelection();
        }

        private void OnGUI()
        {
            GUILayout.Label("Selected FBX Files", EditorStyles.boldLabel);

            if (GUILayout.Button("Refresh Selection"))
            {
                RefreshSelection();
            }

            if (GUILayout.Button(isGridView ? "Switch to List View" : "Switch to Grid View"))
            {
                isGridView = !isGridView;
            }

            if (GUILayout.Button("Clear Selection"))
            {
                ClearSelection();
            }

            HandleDragAndDrop();

            DrawSelectedFbxFiles();

            if (GUILayout.Button("Rename Animation Clips to FBX name"))
            {
                RenameAnimations();
            }

            DrawSelectedAnimationInfo();
        }

        private void DrawSelectedFbxFiles()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(300));
            if (isGridView)
            {
                int itemWidth = 90; // Width of each item
                int columns = Mathf.Max(1, Mathf.FloorToInt(position.width / itemWidth)); // Calculate number of columns based on window width

                int currentColumn = 0;

                GUILayout.BeginHorizontal();
                foreach (var obj in new List<UnityEngine.Object>(selectedFbxObjects))
                {
                    if (currentColumn >= columns)
                    {
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        currentColumn = 0;
                    }

                    GUILayout.BeginVertical(GUILayout.Width(itemWidth));
                    if (GUILayout.Button(AssetPreview.GetAssetPreview(obj) ?? AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(obj)), GUILayout.Width(itemWidth), GUILayout.Height(90)))
                    {
                        if (Event.current.button == 1) // Right click
                        {
                            RemoveObject(obj);
                            Event.current.Use();
                        }
                        else
                        {
                            EditorGUIUtility.PingObject(obj);
                            Selection.activeObject = obj;
                            lastClickedObject = obj;
                            fbxName = obj.name;
                            UpdateClipNamesAndLoops(obj, false); // Set clipNames and clipLoops without updating them
                        }
                    }
                    GUIStyle labelStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
                    {
                        wordWrap = true,
                        clipping = TextClipping.Clip,
                    };
                    GUILayout.Label(obj.name, labelStyle, GUILayout.Width(itemWidth), GUILayout.Height(20));
                    GUILayout.EndVertical();

                    currentColumn++;
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                foreach (var obj in new List<UnityEngine.Object>(selectedFbxObjects))
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(AssetPreview.GetAssetPreview(obj) ?? AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(obj)), GUILayout.Width(90), GUILayout.Height(90)))
                    {
                        if (Event.current.button == 1) // Right click
                        {
                            RemoveObject(obj);
                            Event.current.Use();
                        }
                        else
                        {
                            EditorGUIUtility.PingObject(obj);
                            Selection.activeObject = obj;
                            lastClickedObject = obj;
                            fbxName = obj.name;
                            UpdateClipNamesAndLoops(obj, false); // Set clipNames and clipLoops without updating them
                        }
                    }
                    GUILayout.Label(obj.name, GUILayout.Width(200));
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();
        }

        private void RefreshSelection()
        {
            UnityEngine.Object[] selectedObjects = Selection.objects;
            foreach (var obj in selectedObjects)
            {
                string assetPath = AssetDatabase.GetAssetPath(obj);
                if (assetPath.EndsWith(".fbx", StringComparison.OrdinalIgnoreCase))
                {
                    if (!selectedFbxPaths.Contains(assetPath))
                    {
                        selectedFbxPaths.Add(assetPath);
                        selectedFbxObjects.Add(obj);
                    }
                }
            }
        }

        private void ClearSelection()
        {
            selectedFbxPaths.Clear();
            selectedFbxObjects.Clear();
            ClearClipData();
        }

        private void RemoveObject(UnityEngine.Object obj)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            selectedFbxPaths.Remove(assetPath);
            selectedFbxObjects.Remove(obj);

            // If the removed object was the last clicked object, clear the clip data
            if (lastClickedObject == obj)
            {
                ClearClipData();
            }
        }

        private void ClearClipData()
        {
            lastClickedObject = null;
            fbxName = string.Empty;
            clipNames.Clear();
            clipLoops.Clear();
        }

        private void RenameAnimations()
        {
            foreach (var assetPath in selectedFbxPaths)
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
                    for (int i = 0; i < clipAnimations.Length; i++)
                    {
                        if (clipAnimations[i].name != fbxName || clipAnimations[i].loopTime != clipLoops[i])
                        {
                            clipAnimations[i].name = fbxName;
                            clipAnimations[i].loopTime = clipLoops[i];
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

            AssetDatabase.Refresh();
        }

        private void HandleDragAndDrop()
        {
            Event evt = Event.current;
            Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
            GUI.Box(dropArea, "Drag and Drop FBX Files Here");

            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!dropArea.Contains(evt.mousePosition))
                        return;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        foreach (var draggedObject in DragAndDrop.objectReferences)
                        {
                            string assetPath = AssetDatabase.GetAssetPath(draggedObject);
                            if (assetPath.EndsWith(".fbx", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!selectedFbxPaths.Contains(assetPath))
                                {
                                    selectedFbxPaths.Add(assetPath);
                                    selectedFbxObjects.Add(draggedObject);
                                }
                            }
                        }
                    }
                    Event.current.Use();
                    break;
            }
        }

        private void DrawSelectedAnimationInfo()
        {
            if (lastClickedObject != null)
            {
                GUILayout.Space(10);
                GUILayout.Label("Selected Animation Info", EditorStyles.boldLabel);
                GUILayout.Label($"FBX Name: {fbxName}");

                for (int i = 0; i < clipNames.Count; i++)
                {
                    clipNames[i] = EditorGUILayout.TextField($"Clip Name {i + 1}: ", clipNames[i]);
                    bool newLoopValue = EditorGUILayout.Toggle($"Loop Clip {i + 1}: ", clipLoops[i]);
                    if (newLoopValue != clipLoops[i])
                    {
                        clipLoops[i] = newLoopValue;
                        UpdateClipNamesAndLoops(lastClickedObject, true); // Update clip names and loops when toggle is changed
                    }
                }

                if (GUILayout.Button("Update Clip Names and Loops"))
                {
                    UpdateClipNamesAndLoops(lastClickedObject, true); // Update clip names and loops when button is clicked
                    GUI.FocusControl(null); // Lose focus from all controls
                }
            }
        }

        private void UpdateClipNamesAndLoops(UnityEngine.Object obj, bool updateNames)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            ModelImporter modelImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;
            if (modelImporter != null)
            {
                ModelImporterClipAnimation[] clipAnimations = modelImporter.clipAnimations;

                if (clipAnimations.Length == 0)
                {
                    clipAnimations = modelImporter.defaultClipAnimations;
                }

                if (updateNames)
                {
                    if (clipAnimations.Length == clipNames.Count)
                    {
                        for (int i = 0; i < clipAnimations.Length; i++)
                        {
                            clipAnimations[i].name = clipNames[i];
                            clipAnimations[i].loopTime = clipLoops[i];
                        }
                        modelImporter.clipAnimations = clipAnimations;
                        AssetDatabase.WriteImportSettingsIfDirty(assetPath);
                        AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                        Debug.Log($"Updated animation clip names and loop settings for {assetPath}");
                    }
                    else
                    {
                        Debug.LogError("The number of clip names does not match the number of animation clips.");
                    }
                }
                else
                {
                    clipNames.Clear();
                    clipLoops.Clear();
                    foreach (var clip in clipAnimations)
                    {
                        clipNames.Add(clip.name);
                        clipLoops.Add(clip.loopTime);
                    }
                }
            }
        }
    }
}
