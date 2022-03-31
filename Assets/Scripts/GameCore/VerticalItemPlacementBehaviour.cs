using UnityEngine;

namespace Game.Core
{
    public class VerticalItemPlacementBehaviour : MonoBehaviour, IItemPlacement
    {
        private Transform _keepItemsPoint;

        public VerticalItemPlacementBehaviour(Transform keepItemsPoint)
        {
            _keepItemsPoint = keepItemsPoint;
        }

        public bool TryPlaceItem(Item addedItem, Item lastItem, int itemCount, out Vector3 placePosition)
        {
            if (lastItem == null)
            {
                addedItem.transform.localPosition = _keepItemsPoint.localPosition;
                placePosition = _keepItemsPoint.localPosition;

                return true;
            }

            placePosition = lastItem.transform.localPosition;
            placePosition.y += addedItem.Height;

            return true;
        }
    }
}
