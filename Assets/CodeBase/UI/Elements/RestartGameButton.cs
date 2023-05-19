using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class RestartGameButton : MonoBehaviour
    {
        [SerializeField] private Button _loadGameButton;

        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _loadGameButton.onClick.AddListener(Reload);
        }

        private void Reload() =>
            _gameStateMachine.Enter<LoadLevelState, string>(GameConstants.GameScene);
    }
}