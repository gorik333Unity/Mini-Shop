using UnityEngine;
using Game.Core;
using Game.Scriptable;

namespace Game.Customers
{
    public class CommonCustomer : Customer
    {
        [SerializeField]
        private Transform _keepItemsPoint;

        [SerializeField]
        private CustomerCharacteristicsData[] _customerData;

        private int _itemsCount;
        private int _maxCapacity;

        public override void InitializeBehaviours()
        {
            ItemKeeper.SetItemPlacementBehaviour(new VerticalItemPlacementBehaviour(_keepItemsPoint));
        }

        public override void IncreaseLevel()
        {
            m_currentLevel++;
            SetUpDataComponents();
        }

        public override void ClearPurchases()
        {
            ItemKeeper.TryClearKeeper();
        }

        public override int GetPurchasesPrice()
        {
            return ItemKeeper.GetItemsPrice();
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

            ItemKeeper.SetCapacity(_customerData[desiredLevel].ItemsCapacity);
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
            ItemKeeper.OnAdded += ItemKeeper_OnAdded;
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
}
