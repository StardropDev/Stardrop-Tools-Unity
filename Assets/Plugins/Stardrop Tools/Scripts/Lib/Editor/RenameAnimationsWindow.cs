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
        private string newName;

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

            HandleDragAndDrop();

            DrawSelectedFbxFiles();

            if (GUILayout.Button("Rename Animations to FBX name"))
            {
                RenameAnimations();
            }

            DrawRenameSection();
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
                foreach (var obj in selectedFbxObjects)
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
                        EditorGUIUtility.PingObject(obj);
                        Selection.activeObject = obj;
                        lastClickedObject = obj;
                        newName = obj.name;
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
                foreach (var obj in selectedFbxObjects)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(AssetPreview.GetAssetPreview(obj) ?? AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(obj)), GUILayout.Width(90), GUILayout.Height(90)))
                    {
                        EditorGUIUtility.PingObject(obj);
                        Selection.activeObject = obj;
                        lastClickedObject = obj;
                        newName = obj.name;
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

        private void DrawRenameSection()
        {
            if (lastClickedObject != null)
            {
                GUILayout.Space(10);
                GUILayout.Label("Rename Selected Object", EditorStyles.boldLabel);
                newName = EditorGUILayout.TextField("New Name", newName);

                if (GUILayout.Button("Apply Rename"))
                {
                    string assetPath = AssetDatabase.GetAssetPath(lastClickedObject);
                    AssetDatabase.RenameAsset(assetPath, newName);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    int index = selectedFbxObjects.IndexOf(lastClickedObject);
                    if (index != -1)
                    {
                        selectedFbxObjects[index] = AssetDatabase.LoadAssetAtPath < Unity
