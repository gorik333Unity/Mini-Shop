using System;
using UnityEngine;
using UnityEngine.AI;
using Game.Core;
using Game.Scriptable;

public abstract class Customer : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;

    public Action<CommonCustomer> OnFull;

    protected int m_currentLevel;

    public NavMeshAgent Agent { get { return _agent; } }

    public abstract void IncreaseLevel();

    public abstract void InitializeBehaviours();

    public abstract void ClearPurchases();

    protected abstract void SetUpDataComponents();
}

public class CommonCustomer : Customer
{
    [SerializeField]
    private ItemKeeper _itemKeeper;

    [SerializeField]
    private Transform _keepItemsPoint;

    [SerializeField]
    private CustomerCharacteristicsData[] _customerData;

    private int _itemsCount;
    private int _maxCapacity;

    public override void InitializeBehaviours()
    {
        _itemKeeper.SetItemPlacementBehaviour(new VerticalItemPlacementBehaviour(_keepItemsPoint));
    }

    public override void IncreaseLevel()
    {
        m_currentLevel++;
        SetUpDataComponents();
    }

    public override void ClearPurchases()
    {
        _itemKeeper.TryClearKeeper();
    }

    protected override void SetUpDataComponents()
    {
        if (_customerData.Length == 0)
            throw new System.NullReferenceException(nameof(_customerData));

        int desiredLevel = m_currentLevel;

        if (desiredLevel >= _customerData.Length)
        {
            desiredLevel = _customerData.Length - 1;

            Debug.LogWarning("Max level reached");
        }

        _itemKeeper.SetCapacity(_customerData[desiredLevel].ItemsCapacity);
        _maxCapacity = _customerData[desiredLevel].ItemsCapacity;

        Agent.angularSpeed = _customerData[desiredLevel].AngularSpeed;
        Agent.speed = _customerData[desiredLevel].MovementSpeed;
    }

    private void Awake()
    {
        SetUpDataComponents();
    }

    private void Start()
    {
        InitializeBehaviours();
        SetUpActions();
    }

    private void SetUpActions()
    {
        _itemKeeper.OnAdded += ItemKeeper_OnAdded;
    }

    private void ItemKeeper_OnAdded(Item item)
    {
        _itemsCount++;

        if (_itemsCount >= _maxCapacity)
        {
            OnFull?.Invoke(this);

            _itemsCount = 0;
        }
    }
}
