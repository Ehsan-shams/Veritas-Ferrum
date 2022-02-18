using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScatterPro : EditorWindow
{
    public EnvDataBasePro EnvDb;

    private List<EnvPrefab> envs;
    public int CountX = 10, CountY = 10;
    private GraphManagerPro graphManager;
    private int _type;
    private int _category;

    [MenuItem("Tools/ScatterToolPro")]
    static void Init()
    {
        var window = GetWindowWithRect<ScatterPro>(new Rect(0, 0, 265, 200));
        window.Show();
    }

    void OnGUI()
    {
        CountX = EditorGUILayout.IntField("Count X", CountX);
        CountY = EditorGUILayout.IntField("Count Y", CountY);

        if (!graphManager)
        {
            graphManager = GraphManagerPro.graphManager;
        }

        if (!EnvDb)
        {
            EnvDb = graphManager.EnvDB;
        }

        _type = EditorGUILayout.Popup("Type", _type, EnvDb.Types);
        _category = EditorGUILayout.Popup("Category", _category, EnvDb.Categoreis(_type));

        if (GUILayout.Button("Scatter"))
        {
            bool confirm = true;
            if (graphManager && graphManager.Nodes?.Count > 0)
            {
                confirm = EditorUtility.DisplayDialog(
                    "Recreate scene",
                    "All modules will be deleted.\nAre you sure you want to continue?",
                    "Continue",
                    "Cancel");
            }

            if (!confirm)
                return;

            graphManager.Scatter(CountX, CountY, _type, _category);
        }

        if (GUILayout.Button("Change Prefab"))
        {
            List<EnvPrefab> envs = new List<EnvPrefab>();
            foreach (Object o in Selection.objects)
            {
                GameObject go = o as GameObject;
                EnvPrefab e = go.GetComponent(typeof(EnvPrefab)) as EnvPrefab;
                if (e == null)
                {
                    e = go.GetComponentInParent(typeof(EnvPrefab)) as EnvPrefab;
                }

                if (go != null && e != null && !envs.Contains(e))
                {
                    envs.Add(e as EnvPrefab);
                }
            }

            graphManager.ChangePrefabs(
                _category,
                _type,
                envs);
        }

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Rotate Prefabs"))
        {
            List<EnvPrefab> envs = new List<EnvPrefab>();
            foreach (Object o in Selection.objects)
            {
                GameObject go = o as GameObject;
                Component e = go.GetComponent(typeof(EnvPrefab));
                if (e == null)
                {
                    e = go.GetComponentInParent(typeof(EnvPrefab));
                }

                if (e != null)
                {
                    envs.Add(e as EnvPrefab);
                }
            }

            foreach (EnvPrefab e in envs)
            {
                e.Rotate();
            }
        }
        
        if (GUILayout.Button("Mirror Z"))
        {
            List<EnvPrefab> envs = new List<EnvPrefab>();
            foreach (Object o in Selection.objects)
            {
                GameObject go = o as GameObject;
                Component e = go.GetComponent(typeof(EnvPrefab));
                if (go != null && e != null)
                {
                    envs.Add(e as EnvPrefab);
                }
            }

            foreach (EnvPrefab e in envs)
            {
                e.MirrorZ();
            }
        }

        if (GUILayout.Button("Mirror X"))
        {
            List<EnvPrefab> envs = new List<EnvPrefab>();
            foreach (Object o in Selection.objects)
            {
                GameObject go = o as GameObject;
                Component e = go.GetComponent(typeof(EnvPrefab));
                if (go != null && e != null)
                {
                    envs.Add(e as EnvPrefab);
                }
            }

            foreach (EnvPrefab e in envs)
            {
                e.MirrorX();
            }
        }

        EditorGUILayout.EndHorizontal();
    }
}