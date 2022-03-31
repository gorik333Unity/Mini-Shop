using UnityEngine;
using System.Collections.Generic;

namespace Game.Core
{
    public class HorizontalItemPlacementBehaviour : MonoBehaviour, IItemPlacement, IPlacementExpandable
    {
        private readonly List<Transform> _keepItemsPoint;

        public HorizontalItemPlacementBehaviour(Transform[] keepItemsPoints)
        {
            _keepItemsPoint = new List<Transform>(keepItemsPoints);
        }

        public void AddPlacemenst(params Transform[] transforms)
        {
            foreach (var item in transforms)
                _keepItemsPoint.Add(item);
        }

        public bool TryPlaceItem(Item addedItem, Item lastItem, int itemCount, out Vector3 placePosition)
        {
            placePosition = Vector3.zero;

            if (itemCount >= _keepItemsPoint.Count || itemCount < 0)
                return false;

            placePosition = _keepItemsPoint[itemCount].position;

            return true;
        }
    }
}
