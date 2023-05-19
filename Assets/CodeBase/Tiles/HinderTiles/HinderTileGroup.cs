using CodeBase.Services.GameObserverReporter;
using CodeBase.Services.StaticData;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Tiles.HinderTiles
{
    public class HinderTileGroup : MonoBehaviour
    {
        private IGameObserverReporterService _reporterService;
        public Action<HinderTileGroup> OnTouch;
        private IStaticDataService _dataService;

        private HinderTile[] _hinderTiles = Array.Empty<HinderTile>();
        private bool _isTouch;

        public void Construct(IGameObserverReporterService gameObserverReporterService, IStaticDataService dataService)
        {
            _reporterService = gameObserverReporterService;
            _dataService = dataService;

            _reporterService.OnGameOver += OnGameOver;
        }

        private void Start()
        {
            FindTileReporter();
        }

        private void OnDestroy()
        {
            _reporterService.OnGameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            StopAllCoroutines();
            foreach (var child in _hinderTiles)
            {
                child.OnTouch -= PlayerTriggered;
            }
        }

        private void FindTileReporter()
        {
            _hinderTiles = GetComponentsInChildren<HinderTile>();

            foreach (var child in _hinderTiles)
            {
                child.OnTouch += PlayerTriggered;
            }
        }

        private void PlayerTriggered()
        {
            if (_isTouch)
                return;

            StartCoroutine(DestroyThisEndFrame());
            StartCoroutine(SendOnTouch());

            _isTouch = true;
        }

        private IEnumerator DestroyThisEndFrame()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(_dataService.HinderTileStaticData.LifeTimeAfterTriggered);
            Destroy(gameObject);
        }

        private IEnumerator SendOnTouch()
        {
            yield return new WaitForEndOfFrame();
            OnTouch?.Invoke(this);
        }
    }
}