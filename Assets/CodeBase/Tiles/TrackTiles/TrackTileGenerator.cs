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
        private TrackGround _previousTile;

        public void Construct(IStaticDataService dataService, IGameFactory gameFactory, Transform cameraTransform)
        {
            _dataService = dataService;
            _gameFactory = gameFactory;
            _cameraTransform = cameraTransform;
        }

        private void Start()
        {
            CreateFirstTile();
            StartCoroutine(CheckCameraPosition(RemoveFirstTile, CreateTile));
        }

        private void CreateFirstTile()
        {
            TrackGround ground = _gameFactory.CreateTrackTile(TrackTileId.Tile1, Vector3.back * 5);//start offset
            Register(ground);
        }

        private IEnumerator CheckCameraPosition(params Action[] onBehindCamera)
        {
            var wait = new WaitForSeconds(_dataService.TrackTileStaticData.CheckNewTileDelay);

            while (true)
            {
                for (int i = 0; i < 10_000; i++) //stack over flow
                {
                    if (_previousTile != null && _previousTile.EndZPosition() < _cameraTransform.position.z)
                    {
                        foreach (var action in onBehindCamera)
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
            TrackGround ground = _gameFactory.CreateTrackTile(TrackTileId.Tile1, Vector3.forward * _previousTile.EndZPosition());
            Register(ground);
        }

        private void Register(TrackGround ground)
        {
            _tiles.Add(ground);
            _previousTile = ground;
        }

        private void RemoveFirstTile()
        {
            _tiles[0].Close();
            _tiles.Remove(_tiles[0]);
        }
    }
}