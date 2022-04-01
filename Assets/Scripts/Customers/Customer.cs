using System;
using UnityEngine;
using UnityEngine.AI;
using Game.Core;

namespace Game.Customers
{
    public abstract class Customer : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent _agent;

        [SerializeField]
        private ItemKeeper _itemKeeper;

        public Action<Customer> OnFull;

        protected int m_currentLevel;

        public ItemKeeper ItemKeeper { get { return _itemKeeper; } }

        public NavMeshAgent Agent { get { return _agent; } }

        public abstract void IncreaseLevel();

        public abstract void InitializeBehaviours();

        public abstract void ClearPurchases();

        public abstract int GetPurchasesPrice();

        protected abstract void SetUpDataComponents();
    }
}
