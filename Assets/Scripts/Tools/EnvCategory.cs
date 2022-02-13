using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvCategory", menuName = "ScriptableObjects/EnvCategory", order = 1)]
public class EnvCategory : ScriptableObject
{

    public PrefabType Type;
    public List<Env> EnvironmentPrefabs = new List<Env>();
    
    public Env StreetEdge;
    public Env StreetInnerCorner;
    public Env StreetOuterCorner;
    
    public Env ParkEdge;
    public Env ParkInnerCorner;
    public Env ParkOuterCorner;
    
    [Serializable]
    public struct Env
    {
        public string Name;
        public GameObject Prefab;
    }

    public string[] Choices
    {
        get
        {
            return EnvironmentPrefabs.Select(e => e.Name).ToArray();
        }
    }
    
}
public enum PrefabType
{
    Street,Pavement,Park
}
