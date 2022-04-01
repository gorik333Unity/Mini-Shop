using UnityEngine;

namespace Game.Scriptable
{
    [CreateAssetMenu(fileName = "CustomerData.asset", menuName = "MiniShop/Customer configuration")]
    public class CustomerCharacteristicsData : ScriptableObject
    {
        [Min(1)]
        [SerializeField]
        private int _itemsCapacity = 1;

        [Min(1)]
        [SerializeField]
        private float _movementSpeed = 1f;

        [Min(1)]
        [SerializeField]
        private float _angularSpeed = 500f;

        public int ItemsCapacity { get { return _itemsCapacity; } }

        public float MovementSpeed { get { return _movementSpeed; } }

        public float AngularSpeed { get { return _angularSpeed; } }
    }
}