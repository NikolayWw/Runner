using CodeBase.Services.Factory;
using CodeBase.Services.GameObserverReporter;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Tile.Collectable;
using CodeBase.StaticData.Tile.Hinder;
using UnityEngine;

namespace CodeBase.Tiles.Collectable
{
    public class CollectableGenerator : MonoBehaviour
    {
        private IGameFactory _gameFactory;
        private HinderTileStaticData _hinderData;
        private CollectableStaticData _staticData;

        public void Construct(IGameFactory gameFactory, IStaticDataService dataService, IGameObserverReporterService observerReporterService)
        {
            _gameFactory = gameFactory;
            _staticData = dataService.CollectableStaticData;
            _hinderData = dataService.HinderTileStaticData;
            observerReporterService.OnHinderTileGenerate += CreateCollectable;
        }

        private void CreateCollectable(Transform parent, float lastTilePositionZ)
        {
            int randomTileIndex = Random.Range(0, _staticData.Configs.Count);
            Vector3 position = Vector3.forward * (lastTilePositionZ - _hinderData.DistanceBetweenTiles / 2f);
            _gameFactory.CreateCollectableGroupPiece(_staticData.Configs[randomTileIndex].Id, parent, position);
        }
    }
}