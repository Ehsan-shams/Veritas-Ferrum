using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScatterTool : EditorWindow
{
    public EnvDataBase EnvDb;
    private List<EnvPrefab> envs;
    public EnvPrefab source;
    
    [MenuItem("Tools/ScatterTool")]
    static void Init()
    {
        var window = GetWindowWithRect<ScatterTool>(new Rect(0, 0, 265, 200));
        window.Show();
    }

    void OnGUI()
    {
        source = EditorGUILayout.ObjectField("Prefab",source, typeof(EnvPrefab), true) as EnvPrefab;
        EnvDb = EditorGUILayout.ObjectField("Data Base",EnvDb, typeof(EnvDataBase), true) as EnvDataBase;

        CountX = EditorGUILayout.IntField("Count X", CountX);
        CountY = EditorGUILayout.IntField("Count Y", CountY);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select Environment Prefabs"))
        {
            List<Object> envs=new List<Object>();
            foreach (Object o in Selection.objects)
            {
                GameObject go = o  as GameObject;
                if (go != null && go.GetComponent(typeof(EnvPrefab))!=null)
                {
                    envs.Add(o);
                }
            }

            Object[] array = envs.ToArray();

            Selection.objects =null;
            Selection.objects =array;
        }
        
        if (GUILayout.Button("Scatter"))
        {
            if (source == null)
                ShowNotification(new GUIContent("No object selected for searching"));
            
            GameObject prefab = EnvDb.Categories[_categoryIndex].EnvironmentPrefabs[_prefabIndex].Prefab;
            GraphManager.graphManager.Scatter(CountX, CountY, source,prefab,EnvDb.Categories[_categoryIndex].Type);
            
        }
        EditorGUILayout.EndHorizontal();
        
        _categoryIndex = EditorGUILayout.Popup( "Category",_categoryIndex, EnvDb.Choices);
        _prefabIndex = EditorGUILayout.Popup("Type",_prefabIndex, EnvDb.Categories[_categoryIndex].Choices);

        if (GUILayout.Button("Change Prefabs"))
        {
            List<EnvPrefab> envs=new List<EnvPrefab>();
            foreach (Object o in Selection.objects)
            {
                GameObject go = o  as GameObject;
                Component e = go.GetComponent(typeof(EnvPrefab));
                if (go != null && e!=null)
                {
                    envs.Add(e as EnvPrefab);
                }
            }
            foreach (EnvPrefab e in envs)
            {
                GraphManager.graphManager.SetEnvPrefab(
                    EnvDb.Categories[_categoryIndex].EnvironmentPrefabs[_prefabIndex].Prefab,
                    EnvDb.Categories[_categoryIndex].Type,
                    e);
                //e.SetPrefab(EnvDb.Categories[_categoryIndex].EnvironmentPrefabs[_prefabIndex].Prefab);
            }
        }
        
        if (GUILayout.Button("Rotate Prefabs"))
        {
            List<EnvPrefab> envs=new List<EnvPrefab>();
            foreach (Object o in Selection.objects)
            {
                GameObject go = o  as GameObject;
                Component e = go.GetComponent(typeof(EnvPrefab));
                if (go != null && e!=null)
                {
                    envs.Add(e as EnvPrefab);
                }
            }
            foreach (EnvPrefab e in envs)
            {
                e.Rotate();
            }

        }
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Mirror Z"))
        {
            List<EnvPrefab> envs=new List<EnvPrefab>();
            foreach (Object o in Selection.objects)
            {
                GameObject go = o  as GameObject;
                Component e = go.GetComponent(typeof(EnvPrefab));
                if (go != null && e!=null)
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
            List<EnvPrefab> envs=new List<EnvPrefab>();
            foreach (Object o in Selection.objects)
            {
                GameObject go = o  as GameObject;
                Component e = go.GetComponent(typeof(EnvPrefab));
                if (go != null && e!=null)
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
    
    int _categoryIndex;
    int _prefabIndex;
    
    public int CountX { get; set; }
    public int CountY { get; set; }
}