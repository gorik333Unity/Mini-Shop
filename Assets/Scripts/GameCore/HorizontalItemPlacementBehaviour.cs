using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Game.Core
{
    public class HorizontalItemPlacementBehaviour : MonoBehaviour, IItemPlacement, IPlacementExpandable
    {
        private readonly Dictionary<Transform, Item> _keepItemsPoint;

        public HorizontalItemPlacementBehaviour(Transform[] keepItemsPoints)
        {
            _keepItemsPoint = new Dictionary<Transform, Item>();

            for (int i = 0; i < keepItemsPoints.Length; i++)
            {
                _keepItemsPoint.Add(keepItemsPoints[i], null);
            }
        }

        public void AddPlacemenst(params Transform[] transforms)
        {
            foreach (var placement in transforms)
            {
                if (!_keepItemsPoint.ContainsKey(placement))
                    _keepItemsPoint.Add(placement, null);
            }
        }

        public void RemovePlacedItem(Item placedItem)
        {
            for (int i = 0; i < _keepItemsPoint.Count; i++)
            {
                var key = _keepItemsPoint.ElementAt(i).Key;

                if (_keepItemsPoint.TryGetValue(key, out Item item))
                {
                    if (item == placedItem)
                    {
                        _keepItemsPoint[key] = null;

                        break;
                    }
                }
            }
        }

        public bool TryPlaceItem(Item addedItem, Item lastItem, int itemCount, out Vector3 placePosition)
        {
            placePosition = default;

            if (itemCount >= _keepItemsPoint.Count || itemCount < 0)
                return false;

            for (int i = 0; i < _keepItemsPoint.Count; i++)
            {
                var key = _keepItemsPoint.ElementAt(i).Key;

                if (_keepItemsPoint.TryGetValue(key, out Item item))
                {
                    if (item == null)
                    {
                        placePosition = key.localPosition;
                        _keepItemsPoint[key] = addedItem;

                        break;
                    }
                }
            }

            return placePosition != default;
        }
    }
}
