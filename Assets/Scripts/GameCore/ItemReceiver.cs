using UnityEngine;
using System.Collections;

namespace Game.Core
{
    public sealed class ItemReceiver : ItemDistributor
    {
        [SerializeField]
        private float _receiveDelay;

        private Coroutine _receiveItemC;

        public override void InitializeBehaviours()
        {
            SetItemMoveBehaviour(new ItemJumpMoveBehaviour());
        }

        protected override void StartItemProcess(ItemKeeper itemKeeper)
        {
            _receiveItemC = StartCoroutine(IEReceiveItem(itemKeeper));
        }

        protected override void StopItemProcess(ItemKeeper itemKeeper)
        {
            if (_receiveItemC != null)
                StopCoroutine(_receiveItemC);
        }

        private IEnumerator IEReceiveItem(ItemKeeper otherKeeper)
        {
            while (true)
            {
                if (otherKeeper.TryGetItem(out Item item))
                {
                    if (ItemKeeper.TryAddItem(item))
                    {
                        ExecuteItemMoveBehaviour(item);

                        yield return new WaitForSeconds(_receiveDelay);
                    }
                }

                yield return null;
            }
        }
    }
}
