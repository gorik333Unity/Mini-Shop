﻿using UnityEngine;
using DG.Tweening;

namespace Game.Core
{
    public class ItemJumpMoveBehaviour : MonoBehaviour, IItemMoveable
    {
        public bool Move(ItemKeeper giver, ItemKeeper receiver, Vector3 movePosition)
        {
            Item givedItem = giver.GetItem();

            givedItem.transform.DOLocalJump(movePosition, 1, 1, 0.5f).OnComplete(() => OnComplete(givedItem, giver, receiver));

            return false;
        }

        private void OnComplete(Item givedItem, ItemKeeper giver, ItemKeeper receiver)
        {
            giver.TryRemoveItem(givedItem);
            receiver.AddItem(givedItem);
        }
    }
}