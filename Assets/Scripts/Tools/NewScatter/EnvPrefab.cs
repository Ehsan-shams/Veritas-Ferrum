using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
public class EnvPrefab : MonoBehaviour
{
    [Multiline] public string NeighborDesc;

    public Vector2 Size;

    public EnvPrefab north;
    public EnvPrefab south;
    public EnvPrefab west;
    public EnvPrefab westS;
    public EnvPrefab westN;
    public EnvPrefab east;
    public EnvPrefab eastS;
    public EnvPrefab eastN;

    public PrefabType Type;

    private PrefabType _oldtype;

    [SerializeField] private EnvironmentCategory _category;

    private bool e, w, n, s, es, en, ws, wn;

    public int X;
    public int Y;

    public void SetPrefab(GameObject prefab)
    {
        if (!prefab)
        {
            return;
        }
        if (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        PrefabUtility.InstantiatePrefab(prefab, transform);
    }

    #region Transform

    public void Rotateto(float deg = 0)
    {
        ResetTransform();
        if (deg != 0) Rotate(deg);
    }

    private void ResetTransform()
    {
        Transform child = transform.GetChild(0);
        if (child != null)
        {
            child.localRotation = Quaternion.identity;
            child.localScale = Vector3.one;
            child.localPosition = Vector3.zero;
        }
    }

    public void Rotate(float deg = 90)
    {
        Transform child=null;
        if (transform.childCount > 0)
            child = transform.GetChild(0);
        if (child != null)
        {
            Bounds bounds = child.GetComponent<Renderer>().bounds;
            Vector3 center = bounds.center;
            child.RotateAround(center, Vector3.up, deg);
        }
    }

    public void MirrorZ()
    {
        Transform child = transform.GetChild(0);
        if (child != null)
        {
            Bounds bounds = child.GetComponent<Renderer>().bounds;
            var localScale = child.localScale;
            localScale = new Vector3(localScale.x, 1, -1 * localScale.z);
            child.localScale = localScale;
            child.Translate(0, 0, localScale.z * bounds.size.z);
        }
    }

    public void MirrorX()
    {
        Transform child = transform.GetChild(0);
        if (child != null)
        {
            Bounds bounds = child.GetComponent<Renderer>().bounds;
            var localScale = child.localScale;
            localScale = new Vector3(-1 * localScale.x, 1, localScale.z);
            child.localScale = localScale;
            child.Translate(localScale.x * bounds.size.x, 0, 0);
        }
    }

    #endregion

    public void SetCategory(EnvironmentCategory cat)
    {
        _category = cat;
        _oldtype = Type;
        Type = cat.Type;
    }
    
    public void SetTypeNew()
    {
        SetEdgeNew();
        if (_oldtype != Type)
        {
            _oldtype = Type;
            if (east) east.SetEdgeNew();
            if (west) west.SetEdgeNew();
            if (north) north.SetEdgeNew();
            if (south) south.SetEdgeNew();

            if (eastN) eastN.SetEdgeNew();
            if (eastS) eastS.SetEdgeNew();
            if (westN) westN.SetEdgeNew();
            if (westS) westS.SetEdgeNew();
        }
    }

    private void SetEdgeNew()
    {
        _category.SetEdges(this);
    }
}