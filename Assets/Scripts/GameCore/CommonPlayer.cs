using UnityEngine;
using UnityEngine.AI;
using Game.Scriptable;

namespace Game.Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CommonPlayer : Entity
    {
        [SerializeField]
        private Joystick _joystick;

        [SerializeField]
        private PlayerCharacteristicsData[] _playerData;

        private NavMeshAgent _agent;

        public override void InitializeBehaviours()
        {
            SetMoveable(new JoystickBehaviour(_joystick, _agent, transform));
            ExecuteMoveable();
        }

        public override void IncreaseLevel()
        {
            m_currentLevel++;
            SetUpDataComponents();
        }

        public override void SetLevel(int level)
        {
            m_currentLevel = level;
            SetUpDataComponents();
        }

        private void Awake()
        {
            InitializeComponents();
        }

        private void OnEnable()
        {
            SetUpDataComponents();
        }

        private void Start()
        {
            InitializeBehaviours();
        }

        private void InitializeComponents()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void SetUpDataComponents()
        {
            int desiredLevel = m_currentLevel;

            if (desiredLevel >= _playerData.Length)
            {
                desiredLevel = _playerData.Length - 1;

                Debug.LogWarning("Max level reached");
            }

            _agent.speed = _playerData[desiredLevel].MovementSpeed;
            _agent.angularSpeed = _playerData[desiredLevel].StackItemsCapacity;
        }
    }
}