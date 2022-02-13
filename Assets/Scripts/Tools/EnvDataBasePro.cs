using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvDataBasePro", menuName = "ScriptableObjects/EnvDataBasePro", order = 3)]
public class EnvDataBasePro : EnvDataBase
{
    public static EnvDataBasePro DataBasePro
    {
        get
        {
            string guids = AssetDatabase.FindAssets("t:" + typeof(EnvDataBasePro).Name).First();
            string path = AssetDatabase.GUIDToAssetPath(guids);
            return AssetDatabase.LoadAssetAtPath<EnvDataBasePro>(path);
        }
    }
    
    
}