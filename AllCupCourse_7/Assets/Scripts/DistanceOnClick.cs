using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DistanceOnClick : MonoBehaviour {
    private void OnMouseDown() {
        OnClick();
    }

    bool vasVisited = false;

    public int[,] adjustedNodes = new int[,] {
        { 0, 1, 1, 0, 0, 0, 0 },
        { 1, 0, 0, 1, 1, 1, 0 },
        { 1, 0, 0, 1, 1, 0, 0 },
        { 0, 1, 1, 0, 1, 0, 1 },
        { 0, 1, 1, 1, 0, 1, 1 },
        { 0, 1, 0, 0, 1, 0, 1 },
        { 0, 0, 0, 1, 1, 1, 0 }
    };
    public List<Transform> ObjectsList;
    public int[] distances = new int[] { 0, 0, 0, 0, 0, 0, 0 };
    private void Awake()
    {
        ObjectsList.Add(GameObject.Find("Gate").transform);
        ObjectsList.Add(GameObject.Find("Quarry").transform);
        ObjectsList.Add(GameObject.Find("Mill").transform);
        ObjectsList.Add(GameObject.Find("ArcherArena").transform);
        ObjectsList.Add(GameObject.Find("Castle").transform);
        ObjectsList.Add(GameObject.Find("Hat").transform);
        ObjectsList.Add(GameObject.Find("Sawmill").transform);
    }

    protected abstract void OnClick();
}
