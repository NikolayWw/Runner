using CodeBase.Camera;
using CodeBase.Infrastructure.Logic;
using CodeBase.Services.Factory;
using CodeBase.Services.GameObserverReporter;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IGameObserverReporterService _gameObserverReporterService;
        private readonly IStaticDataService _dataService;

        public LoadLevelState(IGameStateMachine stateMachine, SceneLoader sceneLoader, LoadCurtain loadingCurtain, IGameFactory gameFactory, IUIFactory uiFactory, IGameObserverReporterService gameObserverReporterService, IStaticDataService dataService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _gameObserverReporterService = gameObserverReporterService;
            _dataService = dataService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            Clean();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            _uiFactory.CreateUIRoot();
            UnityEngine.Camera mainCamera = UnityEngine.Camera.main;
            _gameFactory.SetMainCamera(mainCamera);
            InitWorld(mainCamera);

            _stateMachine.Enter<LoopState>();
        }

        private void InitWorld(UnityEngine.Camera mainCamera)
        {
            GameObject player = _gameFactory.CreatePlayer(Vector3.zero, _dataService.TrackTileStaticData.LeftBorder, _dataService.TrackTileStaticData.RightBorder);
            InitCamera(mainCamera, player.transform);
            InitTrackGenerators();
        }

        private void InitTrackGenerators()
        {
            _gameFactory.CreateTrackGenerator();
            _gameFactory.CreateHinderGenerator();
            _gameFactory.CreateCollectableGenerator();
        }

        private void InitCamera(UnityEngine.Camera camera, Transform player)
        {
            camera.GetComponent<CameraFollow>()?.SetTarget(player);
            camera.GetComponent<CameraShake>()?.Construct(_gameObserverReporterService, _dataService);
        }

        private void Clean()
        {
            _gameObserverReporterService.Cleanup();
        }
    }
}