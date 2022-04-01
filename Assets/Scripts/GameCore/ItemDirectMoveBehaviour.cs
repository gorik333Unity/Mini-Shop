using UnityEngine;
using DG.Tweening;

namespace Game.Core
{
    public class ItemDirectMoveBehaviour : MonoBehaviour, IItemMoveable
    {
        public void Move(ItemKeeper giver, ItemKeeper receiver, Vector3 movePosition)
        {
            Item givedItem = giver.GetItem();

            givedItem.transform.DOLocalMove(movePosition, 0.3f).OnComplete(() => OnComplete(givedItem, giver, receiver));
        }

        private void OnComplete(Item givedItem, ItemKeeper giver, ItemKeeper receiver)
        {
            givedItem.OnTook?.Invoke(givedItem);

            giver.TryRemoveItem(givedItem);
            receiver.AddItem(givedItem);
        }
    }
}
