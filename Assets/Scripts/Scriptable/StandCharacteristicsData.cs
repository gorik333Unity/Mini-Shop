using UnityEngine;

namespace Game.Scriptable
{
    [CreateAssetMenu(fileName = "StandData.asset", menuName = "MiniShop/Stand configuration")]
    public class StandCharacteristicsData : ScriptableObject
    {
        [Min(1)]
        [SerializeField]
        private int _itemsCapacity = 1;

        [SerializeField]
        private GameObject _standVisual;

        public int ItemsCapacity { get { return _itemsCapacity; } }

        public GameObject StandVisual { get { return _standVisual; } }
    }
}