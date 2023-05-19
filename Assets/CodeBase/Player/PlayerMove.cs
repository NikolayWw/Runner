using CodeBase.Services.GameObserverReporter;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private PlayerCollect _collect;
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField] private Transform _playerBody;
        [SerializeField] private PlayerAnimation _animation;

        private IInputService _inputService;
        private IGameObserverReporterService _gameObserverReporterService;
        private PlayerStaticData _config;
        private float _leftClamp;
        private float _rightClamp;

        public void Construct(IInputService inputService, IStaticDataService dataService, IGameObserverReporterService gameObserverReporterService, float leftClamp, float rightClamp)
        {
            _inputService = inputService;
            _gameObserverReporterService = gameObserverReporterService;
            _config = dataService.PlayerStaticData;
            _leftClamp = leftClamp;
            _rightClamp = rightClamp;

            _gameObserverReporterService.OnGameOver += StopMove;
            _collect.OnCollect += Jump;
        }

        private void Update()
        {
            UpdateMove();
        }

        private void Jump()
        {
            _playerBody.position += Vector3.up;
            _animation.PlayJump();
            _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, 0, _playerRigidbody.velocity.z);
            _playerRigidbody.AddForce(Vector3.up, ForceMode.VelocityChange);
        }

        private void UpdateMove()
        {
            Vector3 position = transform.position;

            if (_inputService.TouchScreenPress)
                position.x += _config.MoveX * _inputService.MoveAxis * Time.deltaTime;

            position.z += _config.MoveForward * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, _leftClamp, _rightClamp);
            transform.position = position;
        }

        private void StopMove() =>
            enabled = false;
    }
}