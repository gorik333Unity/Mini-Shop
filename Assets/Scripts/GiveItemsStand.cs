using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Scriptable;

public class GiveItemsStand : Entity
{
    [SerializeField]
    private StandCharacteristicsData[] _standData;

    [SerializeField]
    private Transform _standModelParent;

    [SerializeField]
    private ItemKeeper _itemKeeper;

    private Transform[] _itemsPlacementPoint;

    public override void IncreaseLevel()
    {
        m_currentLevel++;

        SetUpDataComponents();
    }

    public override void InitializeBehaviours()
    {
        _itemKeeper.SetItemPlacementBehaviour(new HorizontalItemPlacementBehaviour(_itemsPlacementPoint));
    }

    public override void SetLevel(int level)
    {
        throw new NotImplementedException();
    }

    private void Awake()
    {
        SetUpDataComponents();
    }

    private void Start()
    {
        InitializeBehaviours();
    }

    protected override void SetUpDataComponents()
    {
        int desiredLevel = m_currentLevel;

        if (desiredLevel >= _standData.Length)
        {
            desiredLevel = _standData.Length - 1;

            Debug.LogWarning("Max level reached");
        }

        UpdateStandVisual(_standData[desiredLevel].StandVisual);
        _itemKeeper.SetCapacity(_standData[desiredLevel].ItemsCapacity);
    }

    private void UpdateStandVisual(GameObject visual)
    {
        GameObject spawned = Instantiate(visual, transform.position, visual.transform.rotation, _standModelParent);
        Point[] points = spawned.GetComponentInChildren<PlacementPoint>().ItemPoint;

        var pointsTransform = new List<Transform>();

        foreach (var point in points)
            pointsTransform.Add(point.transform);

        _itemsPlacementPoint = pointsTransform.ToArray();
    }
}
