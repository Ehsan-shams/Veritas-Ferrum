using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvDataBasePro", menuName = "Environments/EnvDataBasePro", order = 0)]
public class EnvDataBasePro : ScriptableObject
{
    public EnvPrefab BaseEnvironment;
    public static EnvDataBasePro DataBasePro
    {
        get
        {
            string guids = AssetDatabase.FindAssets("t:" + typeof(EnvDataBasePro).Name).First();
            string path = AssetDatabase.GUIDToAssetPath(guids);
            return AssetDatabase.LoadAssetAtPath<EnvDataBasePro>(path);
        }
    }

    public string[] Types
    {
        get
        {
            return new[]
            {
                PrefabType.Pavement.ToString(), 
                PrefabType.Street.ToString(), 
                PrefabType.Park.ToString()
            };
        }
    }


    public List<StreetCategories> Streets;
    public List<PavementCategories> Pavements;
    public List<ParkCategories> Parks;


    public string[] Categoreis(int type)
    {
        PrefabType t = (PrefabType) type;
        switch (t)
        {
            case PrefabType.Street:
                return Streets.Select(s => s.name).ToArray();
            case PrefabType.Pavement:
                return Pavements.Select(s => s.name).ToArray();
            case PrefabType.Park:
                return Parks.Select(s => s.name).ToArray();
        }
        return new string[] { };
    }

    public EnvironmentCategory GetCategory(int type, int cat)
    {
        PrefabType t = (PrefabType) type;
        switch (t)
        {
            case PrefabType.Street:
                return Streets[cat];
            case PrefabType.Pavement:
                return Pavements[cat];
            case PrefabType.Park:
                return Parks[cat];
        }

        return null;
    }
}