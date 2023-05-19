using UnityEngine;

namespace CodeBase.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotate;

        private Transform _target;

        private void Start()
        {
            transform.rotation = Quaternion.Euler(_rotate);
        }

        private void LateUpdate()
        {
            if (_target != null)
                transform.position = _target.position + _positionOffset;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}