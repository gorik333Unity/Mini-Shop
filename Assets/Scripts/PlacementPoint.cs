using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementPoint : MonoBehaviour
{
    private Point[] _itemPoint;

    private void Awake()
    {
        _itemPoint = GetComponentsInChildren<Point>();
    }

    public Point[] ItemPoint { get { return _itemPoint; } }
}
