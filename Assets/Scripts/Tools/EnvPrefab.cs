using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
public class EnvPrefab : MonoBehaviour
{
    [Multiline]
    public string NeighborDesc;
   
    public Vector2 Size;
    
    public EnvPrefab north;
    public EnvPrefab south;
    public EnvPrefab west;
    public EnvPrefab east;

    public PrefabType Type;

    private PrefabType _oldtype;
    

    public void SetPrefab(GameObject prefab)
    {
        if (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        PrefabUtility.InstantiatePrefab(prefab, transform);
    }
    public void Rotate()
    {
        Transform child = transform.GetChild(0);
        if (child!=null)
        {
            Bounds bounds = child.GetComponent<Renderer>().bounds;
            Vector3 center = bounds.center;
            child.RotateAround(center,Vector3.up, 90);
        }
    }
    public void MirrorZ()
    {
        Transform child = transform.GetChild(0);
        if (child!=null)
        {
            Bounds bounds = child.GetComponent<Renderer>().bounds;
            var localScale = child.localScale;
            localScale=new Vector3(localScale.x,1,-1*localScale.z);
            child.localScale = localScale;
            child.Translate(0,0,localScale.z*bounds.size.z);
        }
    }
    public void MirrorX()
    {
        Transform child = transform.GetChild(0);
        if (child!=null)
        {
            Bounds bounds = child.GetComponent<Renderer>().bounds;
            var localScale = child.localScale;
            localScale=new Vector3(-1*localScale.x,1,localScale.z);
            child.localScale = localScale;
            child.Translate(localScale.x*bounds.size.x,0,0);
        }
    }

    public void SetDesc()
    {
        string desc = NeighborDesc;
        NeighborDesc = "";
        if (east != null && east.Type==PrefabType.Street) NeighborDesc += "east street\n";
        if (west != null && west.Type==PrefabType.Street) NeighborDesc += "west street\n";
        if (north != null && north.Type==PrefabType.Street) NeighborDesc += "north street\n";
        if (south != null && south.Type==PrefabType.Street) NeighborDesc += "south street\n";
    }

    public void SetType(PrefabType type)
    {
        _oldtype = Type;
        Type = type;
        if (_oldtype!=type)
        {
            if (east != null) east.SetDesc();
            if (west != null) west.SetDesc();
            if (north != null) north.SetDesc();
            if (south != null) south.SetDesc();
        }
    }
}