using UnityEngine;

namespace Game.Core
{
    public interface IItemMoveable
    {
        void Move(Item item, Vector3 movePosition);
    }
}
