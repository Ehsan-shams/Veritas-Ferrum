using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GraphManager
{
    public static GraphManager graphManager = new GraphManager();

    public List<List<EnvPrefab>> Nodes = new List<List<EnvPrefab>>();

    public void Scatter(int CountX, int CountY, EnvPrefab source, GameObject prefab, PrefabType EnvType)
    {
        List<Object> envs = new List<Object>();
        Nodes.Clear();

        Vector3 pos = Vector3.zero;

        for (int i = 0; i < CountX; i++)
        {
            List<EnvPrefab> row = new List<EnvPrefab>();
            Nodes.Add(row);
            pos.x = 0;
            pos = pos + Vector3.forward * source.Size.x;

            for (int j = 0; j < CountY; j++)
            {
                pos = pos + Vector3.right * source.Size.y;
                EnvPrefab envPrefab = PrefabUtility.InstantiatePrefab(source) as EnvPrefab;
                //EnvPref envPref = Instantiate(source, pos, Quaternion.identity);
                envPrefab.transform.localPosition = pos;
                envs.Add(envPrefab);
                row.Add(envPrefab);
            }
        }

        foreach (EnvPrefab env in envs)
        {
            SetEnvPrefab(prefab, EnvType, env);
        }

        SetNeighbers();
    }

    public void SetEnvPrefab(GameObject prefab, PrefabType EnvType, EnvPrefab env)
    {
        env.SetType( EnvType);
        env.SetPrefab(prefab);
    }

    private void SetNeighbers()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            for (int j = 0; j < Nodes[i].Count; j++)
            {
                EnvPrefab p = Nodes[i][j];
                
                p.north = i < Nodes.Count-1 ? Nodes[i + 1][j] : null;
                p.south = i > 0 ? Nodes[i - 1][j] : null;
                p.east = j < Nodes[i].Count-1 ? Nodes[i][j + 1] : null;
                p.west = j > 0 ? Nodes[i][j - 1] : null;
            }
        }

        for (int i = 0; i < Nodes.Count; i++)
        {
            for (int j = 0; j < Nodes[i].Count; j++)
            {
                EnvPrefab p = Nodes[i][j];
                p.SetDesc();
            }
        }


    }
    
}