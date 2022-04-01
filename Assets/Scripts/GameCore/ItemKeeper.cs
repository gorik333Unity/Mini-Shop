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

        public Action<Item> OnAdded;
        public Action<Item> OnTook;

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

        public bool CanAddItem(Item item, out Vector3 localPosition)
        {
            localPosition = Vector3.zero;

            if (_item.Contains(item) || _item.Count + 1 > _maxCapacity)
                return false;

            bool isPlaced = TryPlaceItem(item, out Vector3 placePosition);

            localPosition = placePosition;

            return isPlaced;
        }

        public Item CanGetItem(out Item item)
        {
            item = null;

            try
            {
                item = _item[_item.Count - 1];

                return item;
            }
            catch (Exception e) { } // catch all expected exception types

            return item;
        }

        public void AddItem(Item item)
        {
            if (_item.Contains(item))
                throw new ArgumentException("Item is already in list");

            _item.Add(item);
            OnAdded?.Invoke(item);
        }

        public Item GetItem()
        {
            if (_item.Count == 0)
                throw new ArgumentNullException("Item list is empty");

            Item getItem = _item[_item.Count - 1];

            _itemPlacement.RemovePlacedItem(getItem);
            _item.Remove(getItem);
            OnTook?.Invoke(getItem);

            return getItem;
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

        public bool TryClearKeeper()
        {
            if (_item.Count == 0)
                return false;

            for (int i = 0; i < _item.Count; i++)
                Destroy(_item[i].gameObject);

            _item.Clear();

            return true;
        }

        public int GetItemsPrice()
        {
            int price = 0;

            foreach (var item in _item)
            {
                price += item.Price;
            }

            return price;
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
