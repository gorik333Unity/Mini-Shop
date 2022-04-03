using UnityEngine;
using DG.Tweening;

namespace Game.Core
{
    public class ItemJumpMoveBehaviour : MonoBehaviour, IItemMoveable
    {
        private float _moveTime;

        public ItemJumpMoveBehaviour(float moveTime = 0.3f)
        {
            if (_moveTime < 0)
                throw new System.ArgumentException("Move time less than 0");

            _moveTime = moveTime;
        }

        public void Move(ItemKeeper giver, ItemKeeper receiver, Vector3 movePosition)
        {
            Item givedItem = giver.GetItem();

            givedItem.transform.DOLocalJump(movePosition, 1, 1, _moveTime).OnComplete(() => OnComplete(givedItem, giver, receiver));
        }

        private void OnComplete(Item givedItem, ItemKeeper giver, ItemKeeper receiver)
        {
            givedItem.OnTookEvent();

            giver.TryRemoveItem(givedItem);
            receiver.AddItem(givedItem);
        }
    }
}
