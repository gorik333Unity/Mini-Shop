using UnityEngine;
using System;
using Game.Scriptable;

namespace Game.Core
{
    public sealed class CommonItem : Item
    {
        [SerializeField]
        private Transform _model;

        [SerializeField]
        private CommonItemCharacteristicsData _commonItemData;

        protected override void SetUpItem()
        {
            var data = _commonItemData;

            SetPrice(data.Price);
            SpawnModel(data.Visul);
        }

        private void SpawnModel(GameObject visual)
        {
            if (visual == null)
                throw new ArgumentNullException(nameof(visual));

            Instantiate(visual, transform.position, visual.transform.rotation, _model);
        }

        private void Awake()
        {
            SetUpItem();
        }
    }
}