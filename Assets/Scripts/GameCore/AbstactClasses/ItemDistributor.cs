using UnityEngine;

namespace Game.Core
{
    [RequireComponent(typeof(ItemKeeper))]
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

        public void ExecuteItemMoveBehaviour(Item item)
        {
            _itemMoveable.Move(item);
        }

        public void SetItemKeeperPlacementBehaviour(IItemPlacement itemPlacement)
        {
            ItemKeeper.SetItemPlacementBehaviour(itemPlacement);
        }

        private void OnTriggerEnter(Collider other)
        {
            var itemKeeper = other.GetComponent<ItemKeeper>();

            if (itemKeeper != null)
                StartItemProcess(itemKeeper);
        }

        private void OnTriggerExit(Collider other)
        {
            var itemKeeper = other.GetComponent<ItemKeeper>();

            if (itemKeeper != null)
                StopItemProcess(itemKeeper);
        }

        public abstract void InitializeBehaviours();

        protected abstract void StartItemProcess(ItemKeeper itemKeeper);

        protected abstract void StopItemProcess(ItemKeeper itemKeeper);
    }
}
