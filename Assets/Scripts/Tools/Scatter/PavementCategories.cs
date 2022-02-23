using UnityEngine;

[CreateAssetMenu(fileName = "PavementCategories", menuName = "ScriptableObjects/PavementCategories", order = 2)]
public class PavementCategories : EnvironmentCategory
{
    public override PrefabType Type => PrefabType.Pavement;
    public GameObject EdgeStreet;
    public GameObject InnerCornerStreet;
    public GameObject OuterCornerStreet;

    public override void SetEdges(EnvPrefab env)
    {
        bool e = env.east != null && env.east.Type == PrefabType.Street;
        bool w = env.west != null && env.west.Type == PrefabType.Street;
        bool n = env.north != null && env.north.Type == PrefabType.Street;
        bool s = env.south != null && env.south.Type == PrefabType.Street;
        bool en = env.eastN != null && env.eastN.Type == PrefabType.Street;
        bool es = env.eastS != null && env.eastS.Type == PrefabType.Street;
        bool wn = env.westN != null && env.westN.Type == PrefabType.Street;
        bool ws = env.westS != null && env.westS.Type == PrefabType.Street;

        if (e)
        {
            env.SetPrefab(EdgeStreet);
            env.Rotateto(90);
        }

        if (w)
        {
            env.SetPrefab(EdgeStreet);
            env.Rotateto(-90);
        }

        if (n)
        {
            env.SetPrefab(w || e ? InnerCornerStreet : EdgeStreet);
            float r = 0;
            if (w) r = -90;
            env.Rotateto(r);
        }

        if (s)
        {
            env.SetPrefab(w || e ? InnerCornerStreet : EdgeStreet);
            float r = 180;
            if (e) r = 90;
            env.Rotateto(r);
        }

        if (e || w || n || s)
            return;

        if (en)
        {
            env.SetPrefab(OuterCornerStreet);
            env.Rotateto(0);
            return;
        }

        if (es)
        {
            env.SetPrefab(OuterCornerStreet);
            env.Rotateto(90);
            return;
        }

        if (wn)
        {
            env.SetPrefab(OuterCornerStreet);
            env.Rotateto(-90);
            return;
        }

        if (ws)
        {
            env.SetPrefab(OuterCornerStreet);
            env.Rotateto(180);
            return;
        }

        env.SetPrefab(BasicPrefab);
    }
}