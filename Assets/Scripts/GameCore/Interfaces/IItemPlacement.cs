using UnityEngine;

namespace Game.Core
{
    public interface IItemPlacement
    {
        bool TryPlaceItem(Item addedItem, Item lastItem, int itemCount, out Vector3 placePosition);
    }
}
