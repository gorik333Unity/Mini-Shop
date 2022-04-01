using UnityEngine;

namespace Game.Core
{
    public interface IItemMoveable
    {
        bool Move(ItemKeeper giver, ItemKeeper receiver, Vector3 movePosition);
    }
}
