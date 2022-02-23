using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ParkCategories", menuName = "ScriptableObjects/GeneralCategory", order = 1)]
public class GeneralCategory : ScriptableObject
{
    public List<PrefabKey> prefabs;
    public void SetEdges(EnvPrefab envPrefab)
    {
    }
}

[Serializable]
public class PrefabKey
{
    public int KeySize = 1;
    public List<int> KeyString;
    public EnvKey Key;
    public GameObject prefab;

    public PrefabType Type;
}



public class EnvKey
{
    private List<PrefabType> key;
    private int size = 3;

    public EnvKey(int s)

    {
        size = s;
        key = new List<PrefabType>(s * s);
    }


    public bool MatchKey(EnvPrefab e)
    {
        var keyString = GetEnvKey(e);

        return false;
    }

    public List<int> GetEnvKey(EnvPrefab e)
    {
        List<int> keyString = new List<int>();
        List<EnvPrefab> nodes = GraphManagerPro.GraphManager.Nodes;
        for (var i = e.X - size; i <=e.X + size; i++)
        {
            for (var j = e.Y - size; j <= e.Y + size; j++)
            {
                EnvPrefab prefab = nodes.FirstOrDefault(n => n.X == i && n.Y == j);
                if (!prefab)
                {
                    keyString.Add(-1);
                    continue;
                }
                Debug.Log($"{i} , {j} , {prefab.Type}");


                keyString.Add((int) prefab.Type);
            }
        }

        return keyString;
    }
}