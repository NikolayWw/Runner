using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly AllServices _services;

        private Transform _uiRoot;

        public UIFactory(AllServices allServices)
        {
            _services = allServices;
        }

        public void CreateUIRoot() =>
            _uiRoot = _services.Single<IAssetProvider>().Instantiate(AssetsPath.UIRootPath).transform;

        public void CreateMainMenu()
        {
            GameObject instance = _services.Single<IAssetProvider>().Instantiate(AssetsPath.UIMainGameMenu, _uiRoot);
            instance.GetComponent<RestartGameButton>()?.Construct(_services.Single<IGameStateMachine>());
        }

        public void CreateEndGameMenu()
        {
            GameObject instance = _services.Single<IAssetProvider>().Instantiate(AssetsPath.UIEndGameMenu, _uiRoot);
            instance.GetComponent<RestartGameButton>()?.Construct(_services.Single<IGameStateMachine>());
        }
    }
}