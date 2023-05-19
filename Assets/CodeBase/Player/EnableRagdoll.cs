using CodeBase.Services.GameObserverReporter;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class EnableRagdoll : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _playerCollider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Rigidbody _pelvisRigidbodyRagdoll;
        [SerializeField] private Transform _ragDollTransform;

        private Rigidbody[] _rigidbodys;
        private Collider[] _colliders;
        private CharacterJoint[] _characterJoints;
        private IGameObserverReporterService _gameObserverReporterService;
        private PlayerStaticData _staticData;

        public void Construct(IGameObserverReporterService gameObserverReporterService, IStaticDataService dataService)
        {
            _gameObserverReporterService = gameObserverReporterService;
            _staticData = dataService.PlayerStaticData;

            _rigidbodys = _ragDollTransform.GetComponentsInChildren<Rigidbody>();
            _colliders = _ragDollTransform.GetComponentsInChildren<Collider>();
            _characterJoints = _ragDollTransform.GetComponentsInChildren<CharacterJoint>();

            _gameObserverReporterService.OnGameOver += EnableDoll;
            _gameObserverReporterService.OnGameOver += AddForwardForce;
            DisableDoll();
        }

        private void EnableDoll()
        {
            _animator.enabled = false;
            _playerCollider.enabled = false;
            _rigidbody.detectCollisions = false;
            _rigidbody.useGravity = false;

            foreach (CharacterJoint joint in _characterJoints)
            {
                joint.enableCollision = true;
            }

            foreach (Rigidbody body in _rigidbodys)
            {
                body.detectCollisions = true;
                body.useGravity = true;
            }

            foreach (Collider dollCollider in _colliders)
            {
                dollCollider.enabled = true;
            }
        }

        private void DisableDoll()
        {
            foreach (CharacterJoint joint in _characterJoints)
            {
                joint.enableCollision = false;
            }

            foreach (Rigidbody body in _rigidbodys)
            {
                body.detectCollisions = false;
                body.useGravity = false;
            }

            foreach (Collider dollCollider in _colliders)
            {
                dollCollider.enabled = false;
            }
        }

        private void AddForwardForce()
        {
            _pelvisRigidbodyRagdoll.AddForce(Vector3.forward * _staticData.DeadPushForce, ForceMode.VelocityChange);
        }
    }
}