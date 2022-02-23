using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ParkCategories", menuName = "ScriptableObjects/ParkCategories", order = 1)]
public class ParkCategories:EnvironmentCategory
{
    public override PrefabType Type => PrefabType.Park;
    
    public GameObject Edge;
    public GameObject innerCorner;
    public GameObject OuterCorner;
    public override void SetEdges(EnvPrefab env)
    {
        bool e  = env.east  != null && env.east.Type ==  PrefabType.Pavement;
        bool w  = env.west  != null && env.west.Type ==  PrefabType.Pavement;
        bool n  = env.north != null && env.north.Type == PrefabType.Pavement;
        bool s  = env.south != null && env.south.Type == PrefabType.Pavement;
        bool en = env.eastN != null && env.eastN.Type == PrefabType.Pavement;
        bool es = env.eastS != null && env.eastS.Type == PrefabType.Pavement;
        bool wn = env.westN != null && env.westN.Type == PrefabType.Pavement;
        bool ws = env.westS != null && env.westS.Type == PrefabType.Pavement;

        if (e)
        {
            env.SetPrefab(Edge);
            env.Rotateto(90);
        }

        if (w)
        {
            env.SetPrefab(Edge);
            env.Rotateto(-90);
        }

        if (n)
        {
            env.SetPrefab(w || e ? innerCorner : Edge);
            float r = 0;
            if (w) r = -90;
            env.Rotateto(r);
        }

        if (s)
        {
            env.SetPrefab(w || e ? innerCorner : Edge);
            float r = 180;
            if (e) r = 90;
            env.Rotateto(r);
        }

        if (e || w || n || s)
            return;

        if (en)
        {
            env.SetPrefab(OuterCorner);
            env.Rotateto(0);
            return;
        }

        if (es)
        {
            env.SetPrefab(OuterCorner);
            env.Rotateto(90);
            return;
        }

        if (wn)
        {
            env.SetPrefab(OuterCorner);
            env.Rotateto(-90);
            return;
        }

        if (ws)
        {
            env.SetPrefab(OuterCorner);
            env.Rotateto(180);
            return;
        }

        env.SetPrefab(BasicPrefab);
    }
}