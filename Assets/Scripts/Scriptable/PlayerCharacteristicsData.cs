using UnityEngine;

namespace Game.Scriptable
{
    [CreateAssetMenu(fileName = "PlayerData.asset", menuName = "MiniShop/Player configuration")]
    public class PlayerCharacteristicsData : ScriptableObject
    {
        [Min(1f)]
        [SerializeField]
        private float _movementSpeed;

        [Min(10f)]
        [SerializeField]
        private float _angularSpeed;

        [Min(1)]
        [SerializeField]
        private int _itemsCapacity;

        public float MovementSpeed { get { return _movementSpeed; } }

        public float AngularSpeed { get { return _angularSpeed; } }

        public int ItemsCapacity { get { return _itemsCapacity; } }
    }
}
