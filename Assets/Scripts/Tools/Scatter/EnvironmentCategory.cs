using UnityEngine;

public abstract class EnvironmentCategory:ScriptableObject
{
    public abstract PrefabType Type { get;}
    public GameObject BasicPrefab;

    public abstract void SetEdges(EnvPrefab envPrefab);
}


public enum PrefabType
{
    Pavement=0,Street=1,Park=2
}