using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Tile.Track;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Tiles.TrackTiles
{
    public class TrackTileGenerator : MonoBehaviour
    {
        private IStaticDataService _dataService;
        private IGameFactory _gameFactory;
        private Transform _cameraTransform;

        private readonly List<TrackGround> _tiles = new();
        private TrackGround _previousGround;

        public void Construct(IStaticDataService dataService, IGameFactory gameFactory, Transform cameraTransform)
        {
            _dataService = dataService;
            _gameFactory = gameFactory;
            _cameraTransform = cameraTransform;
        }

        private void Start()
        {
            CreateFirstGround();
            StartCoroutine(CheckCameraPosition(RemoveFirstTile, CreateTile));
        }

        private void CreateFirstGround()
        {
            TrackGround ground = _gameFactory.CreateTrackGround(TrackTileId.Tile1, Vector3.back * 5);//start offset
            Register(ground);
        }

        private IEnumerator CheckCameraPosition(params Action[] playerTriggered)
        {
            var wait = new WaitForSeconds(_dataService.TrackTileStaticData.CheckNewTileDelay);
            const int stackOverFlow = 10_000;

            while (true)
            {
                for (int i = 0; i < stackOverFlow; i++)
                {
                    if (_previousGround != null && _previousGround.EndZPosition() < _cameraTransform.position.z)
                    {
                        foreach (var action in playerTriggered)
                        {
                            action?.Invoke();
                        }
                        continue;
                    }
                    break;
                }

                yield return wait;
            }
        }

        private void CreateTile()
        {
            TrackGround ground = _gameFactory.CreateTrackGround(TrackTileId.Tile1, Vector3.forward * _previousGround.EndZPosition());
            Register(ground);
        }

        private void Register(TrackGround ground)
        {
            _tiles.Add(ground);
            _previousGround = ground;
        }

        private void RemoveFirstTile()
        {
            _tiles[0].Close();
            _tiles.Remove(_tiles[0]);
        }
    }
}