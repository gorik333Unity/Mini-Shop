using UnityEngine;
using System.Collections;

namespace Game.Core
{
    public sealed class ItemGiver : ItemDistributor
    {
        [SerializeField]
        private float _giveDelay;

        private Coroutine _giveItemC;

        public override void InitializeBehaviours()
        {
            SetItemMoveBehaviour(new ItemJumpMoveBehaviour());
        }

        protected override void StartItemProcess(ItemKeeper itemKeeper)
        {
            _giveItemC = StartCoroutine(IEGiveItem(itemKeeper));
        }

        protected override void StopItemProcess(ItemKeeper itemKeeper)
        {
            if (_giveItemC != null)
                StopCoroutine(_giveItemC);
        }

        private void Start()
        {
            InitializeBehaviours();
        }

        private IEnumerator IEGiveItem(ItemKeeper otherKeeper)
        {
            while (true)
            {
                if (ItemKeeper.TryGetItem(out Item item))
                {
                    if (otherKeeper.TryAddItem(item, out Vector3 localPosition))
                    {
                        ItemKeeper.TryRemoveItem(item);

                        ExecuteItemMoveBehaviour(item, localPosition);

                        yield return new WaitForSeconds(_giveDelay);
                    }
                }

                yield return null;
            }
        }
    }
}
