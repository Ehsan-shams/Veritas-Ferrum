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
        FindDirection(env);

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

        if (Directions.Count==0)
        {
            if (GraphManagerPro.GraphManager.StreetTypes.ContainsKey(env))
                GraphManagerPro.GraphManager.StreetTypes.Remove(env);
            GraphManagerPro.GraphManager.StreetTypes.Add(env, "Unknown");
            return;
        }

        if (Directions.Count > 1)
        {
            if (GraphManagerPro.GraphManager.StreetTypes.ContainsKey(env))
                GraphManagerPro.GraphManager.StreetTypes.Remove(env);
            GraphManagerPro.GraphManager.StreetTypes.Add(env, "Cross");
            env.SetPrefab(StreetCross);

            if (en)
            {
                if (e && env.east.south?.south != null && env.east.south?.south.Type == PrefabType.Pavement)
                    env.east.SetPrefab(PavementCross);
                if (n && env.north.west?.west != null && env.north.west?.west.Type == PrefabType.Pavement)
                {
                    env.north.SetPrefab(PavementCross);
                }
            }

            if (wn)
            {
                if (w && env.west.south?.south != null && env.west.south?.south.Type == PrefabType.Pavement)
                    env.west.SetPrefab(PavementCross);
                if (n && env.north.east?.east != null && env.north.east?.east.Type == PrefabType.Pavement)
                {
                    env.north.SetPrefab(PavementCross);
                }
            }

            if (es)
            {
                if (e && env.east?.north.north != null && env.east.north?.north.Type == PrefabType.Pavement)
                    env.east.SetPrefab(PavementCross);
                if (s && env.south?.west.west != null && env.south.west?.west.Type == PrefabType.Pavement)
                {
                    env.south.SetPrefab(PavementCross);
                }
            }

            if (ws)
            {
                if (w && env.west.north?.north != null && env.west.north?.north.Type == PrefabType.Pavement)
                    env.west.SetPrefab(PavementCross);
                if (s && env.south.east?.east != null && env.south.east?.east.Type == PrefabType.Pavement)
                {
                    env.south.SetPrefab(PavementCross);
                }
            }
            return;
        }

        if (Directions[0] == Direction.Horizontal)
        {
            if (GraphManagerPro.GraphManager.StreetTypes.ContainsKey(env))
                GraphManagerPro.GraphManager.StreetTypes.Remove(env);
            GraphManagerPro.GraphManager.StreetTypes.Add(env, "Horizontal");

            env.Rotateto(90);
        }
        else
        {
            if (GraphManagerPro.GraphManager.StreetTypes.ContainsKey(env))
                GraphManagerPro.GraphManager.StreetTypes.Remove(env);
            GraphManagerPro.GraphManager.StreetTypes.Add(env, "Vertical");

            env.Rotateto(0);
        }
    }

    

    public void FindDirection(EnvPrefab env)
    {
        int i = 1;
        int j = 1;
        Direction d;
        int roadWidth = 5;
        if (i > 1)
            return;
        if (env.east == null || env.west == null)
        {
            d = Direction.Vertical;
            return;
        }

        EnvPrefab hor = env;
        while (hor.east)
        {
            i++;
            hor = hor.east;
        }

        hor = env;
        while (hor.west)
        {
            i++;
            hor = hor.west;
        }

        hor = env;
        while (hor.north)
        {
            j++;
            hor = hor.north;
        }

        hor = env;
        while (hor.south)
        {
            j++;
            hor = hor.south;
        }


        Debug.Log(i);
        Debug.Log(j);
    }
}

public enum Direction
{
    Horizontal = 0,
    Vertical = 1
}