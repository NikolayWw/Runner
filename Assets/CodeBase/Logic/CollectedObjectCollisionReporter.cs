using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class CollectedObjectCollisionReporter : MonoBehaviour
    {
        public Action OnCollision;
        public ICollect PlayerCollected { get; private set; }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out ICollect collectedObject))
            {
                PlayerCollected = collectedObject;
                OnCollision?.Invoke();
            }
        }
    }
}