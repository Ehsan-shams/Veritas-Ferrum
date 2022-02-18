using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GraphManagerPro : MonoBehaviour
{
    private static GraphManagerPro _graphManager;

    public static GraphManagerPro graphManager
    {
        get
        {
            if (_graphManager == null)
            {
                _graphManager = FindObjectOfType<GraphManagerPro>();
                if (_graphManager == null)
                {
                    GameObject o = new GameObject("GraphManager");
                    _graphManager = o.AddComponent<GraphManagerPro>();
                }
            }

            return _graphManager;
        }
    }
    
    public GameObject envParent;
    
    [HideInInspector]public List<EnvPrefab> Nodes;
    
    private int countX;
    private int countY;

    public EnvDataBasePro EnvDB
    {
        get
        {
            return EnvDataBasePro.DataBasePro;
        }
    }

    private void Initialize()
    {
        if (Nodes == null)
        {
            foreach (var e in FindObjectsOfType<EnvPrefab>())
            {
                DestroyImmediate(e.gameObject);
            }

            Nodes = new List<EnvPrefab>();
        }

        foreach (EnvPrefab e in Nodes)
        {
            DestroyImmediate(e.gameObject);
        }

        Nodes.Clear();

        if (!envParent)
        {
            envParent = new GameObject("Environment Parent");
        }
    }
    
    public void Scatter(int CountX, int CountY,int type, int cat)
    {
        Initialize();

        EnvPrefab source=EnvDB.BaseEnvironment;
        Vector3 pos = Vector3.zero;

        countX = CountX;
        countY = CountY;

        for (int i = 0; i < countX; i++)
        {
            pos = Vector3.forward * source.Size.x*i;
            
            for (int j = 0; j < countY; j++)
            {
                pos.x = j * source.Size.y;
                EnvPrefab envPrefab = PrefabUtility.InstantiatePrefab(source, envParent.transform) as EnvPrefab;
                if (envPrefab != null)
                {
                    envPrefab.transform.localPosition = pos;
                    Nodes.Add(envPrefab);
                    envPrefab.X = i;
                    envPrefab.Y = j;
                }
            }
        }

        EnvironmentCategory category= EnvDB.GetCategory(type, cat);
        
        SetEnvPrefab(category);
        
        SetNeighbors();
    }

    private void SetEnvPrefab(EnvironmentCategory category)
    {
        foreach (EnvPrefab env in Nodes)
        {
            env.SetCategory(category);
            env.SetPrefab(category.BasicPrefab);
        }
    }
    private void SetNeighbors()
    {
        for (int i = 0; i < countX; i++)
        {
            for (int j = 0; j < countY; j++)
            {
                EnvPrefab p = Nodes.FirstOrDefault(e=> e.X==i && e.Y==j);

                if (i < Nodes.Count - 1)
                {
                    p.north = Nodes.FirstOrDefault(e=> e.X==i+1 && e.Y==j);
                    
                    if (j < countX - 1)
                    {
                        p.eastN = Nodes.FirstOrDefault(e=> e.X==i+1 && e.Y==j+1);
                    }   
                    if (j >0)
                    {
                        p.westN = Nodes.FirstOrDefault(e=> e.X==i+1 && e.Y==j-1);
                    }
                }

                if (i > 0)
                {
                    p.south = Nodes.FirstOrDefault(e=> e.X==i-1 && e.Y==j);
                    if (j < countX - 1)
                    {
                        p.eastS = Nodes.FirstOrDefault(e=> e.X==i-1 && e.Y==j+1);
                    }   
                    if (j >0)
                    {
                        p.westS = Nodes.FirstOrDefault(e=> e.X==i-1 && e.Y==j-1);
                    }
                }

                p.east = j < countY - 1 ? Nodes.FirstOrDefault(e=> e.X==i && e.Y==j+1) : null;
                p.west = j > 0 ? Nodes.FirstOrDefault(e=> e.X==i && e.Y==j-1) : null;
            }
        }

        //Nodes.ForEach(e=>e.SetDesc());

    }

    public void ChangePrefabs(int cat, int type, List<EnvPrefab> envs)
    {
        EnvironmentCategory category= EnvDB.GetCategory(type, cat);

        foreach (EnvPrefab env in envs)
        {
            env.SetCategory(category);
            env.SetPrefab(category.BasicPrefab);
        }

        foreach (EnvPrefab env in envs)
        {
            env.SetTypeNew();
        }
        
    }
}
