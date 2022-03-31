using UnityEngine;
using DG.Tweening;

namespace Game.Core
{
    public class ItemJumpMoveBehaviour : MonoBehaviour, IItemMoveable
    {
        public void Move(Item item, Vector3 movePosition)
        {
            var itemTransform = item.transform;

            itemTransform.DOLocalJump(movePosition, 1f, 1, 0.5f);
        }
    }
}
