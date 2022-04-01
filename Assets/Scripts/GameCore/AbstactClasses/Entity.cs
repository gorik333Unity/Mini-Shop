using UnityEngine;

namespace Game.Core
{
    public abstract class Entity : MonoBehaviour
    {
        protected int m_currentLevel;

        private IMoveable _moveable;
        private Coroutine _moveableC;

        public void SetMoveable(IMoveable moveable)
        {
            _moveable = moveable;
        }

        public void ExecuteMoveable()
        {
            if (_moveableC != null)
                StopCoroutine(_moveableC);

            _moveableC = StartCoroutine(_moveable.Move());
        }

        public abstract void InitializeBehaviours();

        public abstract void IncreaseLevel();

        public abstract void SetLevel(int level);

        protected abstract void SetUpDataComponents();
    }
}
