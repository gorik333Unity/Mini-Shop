using UnityEngine;

namespace Game.Core
{
    public abstract class ItemDistributor : MonoBehaviour
    {
        [SerializeField]
        private ItemKeeper _itemKeeper;

        private IItemMoveable _itemMoveable;

        protected ItemKeeper ItemKeeper { get { return _itemKeeper; } }

        public void SetItemMoveBehaviour(IItemMoveable moveable)
        {
            _itemMoveable = moveable;
        }

        public void ExecuteItemMoveBehaviour(ItemKeeper giver, ItemKeeper receiver, Vector3 movePosition)
        {
            _itemMoveable.Move(giver, receiver, movePosition);
        }

        private void OnTriggerEnter(Collider other)
        {
            var itemKeeper = other.GetComponent<ItemKeeper>();

            if (itemKeeper == null)
                itemKeeper = other.GetComponentInParent<ItemKeeper>();

            if (itemKeeper != null)
                StartItemProcess(itemKeeper);
        }

        private void OnTriggerExit(Collider other)
        {
            var itemKeeper = other.GetComponent<ItemKeeper>();

            if (itemKeeper == null)
                itemKeeper = other.GetComponentInParent<ItemKeeper>();

            if (itemKeeper != null)
                StopItemProcess(itemKeeper);
        }

        public abstract void InitializeBehaviours();

        protected abstract void StartItemProcess(ItemKeeper itemKeeper);

        protected abstract void StopItemProcess(ItemKeeper itemKeeper);
    }
}
