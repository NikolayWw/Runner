using CodeBase.Services.Factory;
using CodeBase.Services.GameObserverReporter;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Tile.Hinder;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Tiles.HinderTiles
{
    public class HinderGenerator : MonoBehaviour
    {
        private IGameFactory _gameFactory;
        private HinderTileStaticData _hinderData;
        private IGameObserverReporterService _reporterService;

        private readonly List<HinderTileGroup> _tiles = new();

        public void Construct(IGameFactory gameFactory, IStaticDataService dataService, IGameObserverReporterService gameObserverReporterService)
        {
            _gameFactory = gameFactory;
            _hinderData = dataService.HinderTileStaticData;
            _reporterService = gameObserverReporterService;
        }

        private void Start()
        {
            InitFirstTile();
        }

        private void InitFirstTile()
        {
            HinderTileGroup hinder = _gameFactory.CreateHinderTileGroup(HinderTileGroupId.Wall1, Vector3.forward * _hinderData.StartTileOffsetForward);
            Register(hinder);
            _reporterService.SendHinderTileGenerate(hinder.transform, LastTilePositionZ());

            for (int i = 0; i < _hinderData.StartTilesCount - 1; i++) //first one was created
                CreateHinder();
        }

        private void CreateHinder()
        {
            int randomTileIndex = Random.Range(0, _hinderData.TileConfigs.Count);
            Vector3 position = Vector3.forward * (LastTilePositionZ() + _hinderData.DistanceBetweenTiles);
            HinderTileGroup hinder = _gameFactory.CreateHinderTileGroup(_hinderData.TileConfigs[randomTileIndex].GroupId, position);
            Register(hinder);
            _reporterService.SendHinderTileGenerate(hinder.transform, LastTilePositionZ());
        }

        private void Register(HinderTileGroup instance)
        {
            _tiles.Add(instance);
            instance.OnTouch += _ => CreateHinder();
            instance.OnTouch += RemoveFromList;
        }

        private void RemoveFromList(HinderTileGroup group) =>
            _tiles.Remove(group);

        private float LastTilePositionZ() =>
            _tiles[^1].transform.position.z;
    }
}