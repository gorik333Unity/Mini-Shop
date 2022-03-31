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

        public void SetItemPlacementBehaviour(IItemPlacement itemPlacement)
        {
            _itemPlacement = itemPlacement;
        }

        public void SetCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("Less than 0");

            _maxCapacity = capacity;
        }

        public bool TryAddItem(Item item, out Vector3 localPosition)
        {
            localPosition = Vector3.zero;

            if (_item.Contains(item) || _item.Count + 1 > _maxCapacity)
                return false;

            bool isPlaced = TryPlaceItem(item, out Vector3 placePosition); // 1

            localPosition = placePosition;

            if (isPlaced)
            {
                _item.Add(item); // 2
            }

            return isPlaced;
        }

        public Item TryGetItem(out Item item)
        {
            item = null;

            try
            {
                item = _item[0];

                return item;
            }
            catch (Exception e) { } // catch all expected exception types

            return item;
        }

        public bool TryRemoveItem(Item item)
        {
            if (_item.Contains(item))
            {
                _item.Remove(item);

                return true;
            }

            return false;
        }

        private bool TryPlaceItem(Item addedItem, out Vector3 localPosition)
        {
            bool isPlaced;
            int itemsCount = _item.Count;

            addedItem.transform.parent = _itemsParent;

            if (itemsCount <= 0)
            {
                isPlaced = _itemPlacement.TryPlaceItem(addedItem, null, itemsCount, out Vector3 placePositionFirst);
                localPosition = placePositionFirst;

                return isPlaced;
            }

            Item lastItem = _item[itemsCount - 1];
            isPlaced = _itemPlacement.TryPlaceItem(addedItem, lastItem, itemsCount, out Vector3 placePosition);
            localPosition = placePosition;

            return isPlaced;
        }
    }
}
