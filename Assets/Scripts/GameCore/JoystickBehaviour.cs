using System.Collections;
using UnityEngine;
using System;
using UnityEngine.AI;

namespace Game.Core
{
    public class JoystickBehaviour : MonoBehaviour, IMoveable
    {
        private Joystick _joystick;
        private NavMeshAgent _meshAgent;
        private Transform _entityTransform;

        public JoystickBehaviour(Joystick joystick, NavMeshAgent meshAgent, Transform entityTransform)
        {
            if (joystick == null)
                throw new ArgumentNullException(nameof(joystick));

            if (meshAgent == null)
                throw new ArgumentNullException(nameof(meshAgent));

            if (entityTransform == null)
                throw new ArgumentNullException(nameof(entityTransform));

            _joystick = joystick;
            _meshAgent = meshAgent;
            _entityTransform = entityTransform;
        }

        public IEnumerator Move()
        {
            while (true)
            {
                Vector3 direction = GetJoystickDirection(_joystick.Direction);
                Vector3 position = _entityTransform.position;
                Vector3 movePosition = position + direction;

                _meshAgent.SetDestination(movePosition);

                yield return null;
            }
        }
        /// <summary>
        /// Converts input from Vector2 to Vector3
        /// </summary>
        /// <returns>Vector3 direction</returns>
        private Vector3 GetJoystickDirection(Vector2 direction)
        {
            return new Vector3(direction.x, 0f, direction.y);
        }
    }
}
