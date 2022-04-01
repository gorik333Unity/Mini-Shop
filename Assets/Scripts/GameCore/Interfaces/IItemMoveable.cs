using UnityEngine;

namespace Game.Core
{
    public interface IItemMoveable
    {
        void Move(ItemKeeper giver, ItemKeeper receiver, Vector3 movePosition);
    }
}
