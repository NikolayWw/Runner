using CodeBase.Infrastructure.Logic;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
    public class LoadMainMenuState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly SceneLoader _sceneLoader;

        public LoadMainMenuState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(GameConstants.MainMenuScene, OnLoaded);
        }

        public void Exit()
        { }

        private void OnLoaded()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateMainMenu();

            _gameStateMachine.Enter<LoopState>();
        }
    }
}