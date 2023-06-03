using CodeBase.Collectable;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic;
using CodeBase.Player;
using CodeBase.Services.GameObserverReporter;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Tile.Collectable;
using CodeBase.StaticData.Tile.Hinder;
using CodeBase.StaticData.Tile.Track;
using CodeBase.Tiles.Collectable;
using CodeBase.Tiles.HinderTiles;
using CodeBase.Tiles.TrackTiles;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly AllServices _services;
        private UnityEngine.Camera _mainCamera;

        public GameFactory(AllServices services)
        {
            _services = services;
        }

        public void SetMainCamera(UnityEngine.Camera mainCamera) =>
            _mainCamera = mainCamera;

        public GameObject CreatePlayer(Vector3 position, float leftClamp, float rightClamp)
        {
            GameObject instance = Service<IAssetProvider>().Instantiate(AssetsPath.Player, position);
            instance.GetComponent<PlayerMove>()?.Construct(Service<IInputService>(), Service<IStaticDataService>(), Service<IGameObserverReporterService>(), leftClamp, rightClamp);
            instance.GetComponent<PlayerCollect>()?.Construct(this, Service<IUIFactory>(), Service<IGameObserverReporterService>());
            instance.GetComponent<EnableRagdoll>()?.Construct(Service<IGameObserverReporterService>(), Service<IStaticDataService>());
            instance.GetComponentInChildren<PlayerApplyTouchHinder>()?.Construct(Service<IGameObserverReporterService>(), Service<IUIFactory>());
            return instance;
        }

        public void CreateTrackGenerator()
        {
            var instance = Service<IAssetProvider>().Instantiate(AssetsPath.TrackGenerator);
            instance.GetComponent<TrackTileGenerator>()?.Construct(Service<IStaticDataService>(), this, _mainCamera.transform);
        }

        public void CreateHinderGenerator()
        {
            GameObject instantiate = Service<IAssetProvider>().Instantiate(AssetsPath.HinderGenerator);
            instantiate.GetComponent<HinderGenerator>()?.Construct(this, Service<IStaticDataService>(), Service<IGameObserverReporterService>());
        }

        public void CreateCollectableGenerator()
        {
            GameObject instantiate = Service<IAssetProvider>().Instantiate(AssetsPath.CollectableGenerator);
            instantiate.GetComponent<CollectableGenerator>()?.Construct(this, Service<IStaticDataService>(), Service<IGameObserverReporterService>());
        }

        public void CreateCollectableGroupPiece(CollectableId id, Transform parent, Vector3 at)
        {
            CollectableConfig config = Service<IStaticDataService>().ForCollectable(id);
            var instance = Object.Instantiate(config.PrefabGroup, at, Quaternion.identity);
            instance.transform.SetParent(parent);

            foreach (var collectableObject in instance.GetComponentsInChildren<CollectableObjectPiece>())
                collectableObject.Construct(id);
        }

        public PlayerCollectedObject CreateCollectablePlayer(CollectableId id, Transform parent, Transform follow, float height)
        {
            CollectableConfig config = Service<IStaticDataService>().ForCollectable(id);
            var instance = Object.Instantiate(config.PrefabCollectable, parent);
            instance.transform.localPosition = new Vector3(0, height, 0);
            return instance.GetComponentInChildren<PlayerCollectedObject>();
        }

        public TrackGround CreateTrackTile(TrackTileId id, Vector3 at)
        {
            TrackTileConfig config = Service<IStaticDataService>().ForTrackTile(id);
            GameObject instantiate = Object.Instantiate(config.Prefab, at, Quaternion.identity);
            return instantiate.GetComponent<TrackGround>();
        }

        public HinderTileGroup CreateHinderTileGroup(HinderTileGroupId groupId, Vector3 at)
        {
            HinderTileGroupConfig groupConfig = Service<IStaticDataService>().ForHinderTile(groupId);
            GameObject instance = Object.Instantiate(groupConfig.Prefab, at, Quaternion.identity);
            HinderTileGroup group = instance.GetComponent<HinderTileGroup>();
            group.Construct(Service<IGameObserverReporterService>(), Service<IStaticDataService>());
            return group;
        }

        public void CreateCollectText(Vector3 at)
        {
            var instance = Service<IAssetProvider>().Instantiate(AssetsPath.CollectText, at);
            instance.GetComponent<HideCollectText>()?.Construct(Service<IStaticDataService>());
        }

        private T Service<T>() where T : IService =>
            _services.Single<T>();
    }
}