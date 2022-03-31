using UnityEngine;
using System.Collections.Generic;
using System;

namespace Game.Core
{
    public class ItemKeeper : MonoBehaviour
    {
        [SerializeField]
        private Transform _itemsParent;

        private readonly List<Item> _item = new List<Item>();

        private int _maxCapacity;
        private IItemPlacement _itemPlacement;

        public void SetCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("Less than 0");

            _maxCapacity = capacity;
        }

        public bool TryAddItem(Item item)
        {
            if (_item.Contains(item) || _item.Count + 1 > _maxCapacity)
                return false;

            OnAddItem(item); // 1
            _item.Add(item); // 2

            return true;
        }

        public Item TryGetItem(out Item item)
        {
            item = null;

            try
            {
                item = _item[0];
                return item;
            }
            catch (Exception e) { Debug.LogError(e.StackTrace); } // catch all expected exception types

            return item;
        }

        private void OnAddItem(Item addedItem)
        {
            int itemsCount = _item.Count;

            if (itemsCount < 0)
            {
                _itemPlacement.OnAddItem(addedItem, null, itemsCount);

                return;
            }

            Item lastItem = _item[itemsCount - 1];

            addedItem.transform.parent = _itemsParent;

            _itemPlacement.OnAddItem(addedItem, lastItem, itemsCount);
        }
    }

    public interface IItemPlacement
    {
        void OnAddItem(Item addedItem, Item lastItem, int itemCount);
    }

    public class VerticalItemPlacementBehaviour : MonoBehaviour, IItemPlacement
    {
        private Transform _keepItemsPoint;

        public VerticalItemPlacementBehaviour(Transform keepItemsPoint)
        {
            _keepItemsPoint = keepItemsPoint;
        }

        public void OnAddItem(Item addedItem, Item lastItem, int itemCount)
        {
            Vector3 lastPosition = lastItem.transform.localPosition;
            Vector3 newPosition = lastPosition;
            newPosition.y += addedItem.Height;
            addedItem.transform.localPosition = newPosition;
        }
    }
}
