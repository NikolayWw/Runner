using UnityEngine;

namespace CodeBase.Player
{
    public class ZeroLocalPosition : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;

        private void Update() =>
            _targetTransform.localPosition = new Vector3(0, _targetTransform.localPosition.y, 0);
    }
}