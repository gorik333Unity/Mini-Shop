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

        public bool TryPlaceItem(Item addedItem, Item lastItem, int itemCount)
        {
            if (lastItem == null)
            {
                addedItem.transform.localPosition = _keepItemsPoint.localPosition;

                return true;
            }

            Vector3 lastPosition = lastItem.transform.localPosition;
            Vector3 newPosition = lastPosition;
            newPosition.y += addedItem.Height;
            addedItem.transform.localPosition = newPosition;

            return true;
        }
    }
}
