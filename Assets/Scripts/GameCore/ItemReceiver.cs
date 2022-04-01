using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Core
{
    public sealed class ItemReceiver : ItemDistributor
    {
        [SerializeField]
        private float _receiveDelay;

        private readonly Dictionary<ItemKeeper, Coroutine> _receiveItemC = new Dictionary<ItemKeeper, Coroutine>();

        public override void InitializeBehaviours()
        {
            SetItemMoveBehaviour(new ItemDirectMoveBehaviour());
        }

        protected override void StartItemProcess(ItemKeeper itemKeeper)
        {
            if (!_receiveItemC.ContainsKey(itemKeeper))
                _receiveItemC.Add(itemKeeper, StartCoroutine(IEReceiveItem(itemKeeper)));
        }

        protected override void StopItemProcess(ItemKeeper itemKeeper)
        {
            if (_receiveItemC.TryGetValue(itemKeeper, out Coroutine value))
            {
                _receiveItemC.Remove(itemKeeper);

                StopCoroutine(value);
            }
        }

        private void Start()
        {
            InitializeBehaviours();
        }

        private IEnumerator IEReceiveItem(ItemKeeper otherKeeper)
        {
            while (true)
            {
                if (otherKeeper.CanGetItem(out Item item))
                {
                    if (ItemKeeper.CanAddItem(item, out Vector3 localPosition))
                    {
                        ExecuteItemMoveBehaviour(otherKeeper, ItemKeeper, localPosition);

                        yield return new WaitForSeconds(_receiveDelay);
                    }
                }

                yield return null;
            }
        }
    }
}
