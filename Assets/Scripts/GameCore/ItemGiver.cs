using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Core
{
    public sealed class ItemGiver : ItemDistributor
    {
        [SerializeField]
        private float _giveDelay;

        private readonly Dictionary<ItemKeeper, Coroutine> _giveItemC = new Dictionary<ItemKeeper, Coroutine>();

        public override void InitializeBehaviours()
        {
            SetItemMoveBehaviour(new ItemJumpMoveBehaviour());
        }

        protected override void StartItemProcess(ItemKeeper otherKeeper)
        {
            if (!_giveItemC.ContainsKey(otherKeeper))
                _giveItemC.Add(otherKeeper, StartCoroutine(IEGiveItem(otherKeeper)));
        }

        protected override void StopItemProcess(ItemKeeper otherKeeper)
        {
            if (_giveItemC.TryGetValue(otherKeeper, out Coroutine value))
            {
                _giveItemC.Remove(otherKeeper);

                StopCoroutine(value);
            }
        }

        private void Start()
        {
            InitializeBehaviours();
        }

        private IEnumerator IEGiveItem(ItemKeeper otherKeeper)
        {
            while (true)
            {
                if (ItemKeeper.CanGetItem(out Item item))
                {
                    if (otherKeeper.CanAddItem(item, out Vector3 localPosition))
                    {
                        ExecuteItemMoveBehaviour(ItemKeeper, otherKeeper, localPosition);

                        yield return new WaitForSeconds(_giveDelay);
                    }
                }

                yield return null;
            }
        }
    }
}
