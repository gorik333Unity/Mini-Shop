using UnityEngine;
using DG.Tweening;

namespace Game.Core
{
    public class ItemJumpMoveBehaviour : MonoBehaviour, IItemMoveable
    {
        public void Move(ItemKeeper giver, ItemKeeper receiver, Vector3 movePosition)
        {
            Item givedItem = giver.GetItem();

            givedItem.transform.DOLocalJump(movePosition, 1, 1, 0.5f).OnComplete(() => OnComplete(givedItem, giver, receiver));
        }

        private void OnComplete(Item givedItem, ItemKeeper giver, ItemKeeper receiver)
        {
            givedItem.OnTook?.Invoke(givedItem);

            giver.TryRemoveItem(givedItem);
            receiver.AddItem(givedItem);
        }
    }
}
