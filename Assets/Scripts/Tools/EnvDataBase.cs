using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnvDataBase", menuName = "ScriptableObjects/EnvDataBase", order = 2)]
public class EnvDataBase : ScriptableObject
{
    public static EnvDataBase EnvironmentDb
    {
        get
        {
            string _guids = AssetDatabase.FindAssets("t:" + typeof(EnvDataBase).Name).First();
            string path = AssetDatabase.GUIDToAssetPath(_guids);
            return AssetDatabase.LoadAssetAtPath<EnvDataBase>(path);
        }
    }

    public string[] Choices
    {
        get
        {
            return Categories.Select(c => c.name).ToArray();
        }
    }

    public List<EnvCategory> Categories;

}