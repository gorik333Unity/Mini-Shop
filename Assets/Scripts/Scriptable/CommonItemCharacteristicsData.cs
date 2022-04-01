using UnityEngine;

namespace Game.Scriptable
{
    [CreateAssetMenu(fileName = "CommonItemData.asset", menuName = "MiniShop/Common item configuration")]
    public class CommonItemCharacteristicsData : ScriptableObject
    {
        [Min(0)]
        [SerializeField]
        private int _price = 0;

        [SerializeField]
        private GameObject _visual;

        public int Price { get { return _price; } }

        public GameObject Visul { get { return _visual; } }
    }
}