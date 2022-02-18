using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "StreetCategories", menuName = "ScriptableObjects/StreetCategories", order = 3)]
public class StreetCategories : EnvironmentCategory
{
    public override PrefabType Type => PrefabType.Street;

    public GameObject StreetCross;
    public GameObject PavementCross;
    public GameObject OuterCornerStreet;

    public List<Direction> Directions;

    public override void SetEdges(EnvPrefab env)
    {
        #region Check Near

        bool e = env.east != null && env.east.Type == PrefabType.Street;
        bool w = env.west != null && env.west.Type == PrefabType.Street;
        bool n = env.north != null && env.north.Type == PrefabType.Street;
        bool s = env.south != null && env.south.Type == PrefabType.Street;

        bool en = env.eastN != null && env.eastN.Type == PrefabType.Pavement;
        bool es = env.eastS != null && env.eastS.Type == PrefabType.Pavement;
        bool wn = env.westN != null && env.westN.Type == PrefabType.Pavement;
        bool ws = env.westS != null && env.westS.Type == PrefabType.Pavement;

        #endregion

        #region Find Direction

        Directions = new List<Direction>();
        
        {
            if (e)
            {
                if (env.east.east != null && env.east.east.Type == PrefabType.Street || w)
                    if (!Directions.Contains(Direction.Horizontal))
                        Directions.Add(Direction.Horizontal);
            }
            else if (w)
            {
                if (env.west.west != null && env.west.west.Type == PrefabType.Street)
                    if (!Directions.Contains(Direction.Horizontal))
                        Directions.Add(Direction.Horizontal);
            }

            if (n)
            {
                if (env.north.north != null && env.north.north.Type == PrefabType.Street || s)
                    if (!Directions.Contains(Direction.Vertical))
                        Directions.Add(Direction.Vertical);
            }
            else if (s)
            {
                if (env.south.south != null && env.south.south.Type == PrefabType.Street)
                    if (!Directions.Contains(Direction.Vertical))
                        Directions.Add(Direction.Vertical);
            }
        }

        #endregion

        if (Directions.Count > 1)
        {
            env.SetPrefab(StreetCross);

            if (en)
            {
                if (e) env.east.SetPrefab(PavementCross);
                if (n) env.north.SetPrefab(PavementCross);
            }

            if (wn)
            {
                if (w) env.west.SetPrefab(PavementCross);
                if (n) env.north.SetPrefab(PavementCross);
            }

            if (es)
            {
                if (e) env.east.SetPrefab(PavementCross);
                if (s) env.south.SetPrefab(PavementCross);
            }

            if (ws)
            {
                if (w) env.west.SetPrefab(PavementCross);
                if (s) env.south.SetPrefab(PavementCross);
            }
        }

        if (Directions[0] == Direction.Horizontal)
        {
            env.Rotate(90);
        }
    }
}

public enum Direction
{
    Horizontal = 0,
    Vertical = 1
}