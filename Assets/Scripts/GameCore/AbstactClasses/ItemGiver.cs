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
            SetItemMoveBehaviour(new ItemDirectMoveBehaviour());
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

        private IEnumerator IEGiveItem(ItemKeeper otherKeeper)
        {
            while (true)
            {
                if (ItemKeeper.TryGetItem(out Item item))
                {
                    if (otherKeeper.TryAddItem(item))
                    {
                        ExecuteItemMoveBehaviour(item);

                        yield return new WaitForSeconds(_giveDelay);
                    }
                }

                yield return null;
            }
        }
    }
}
