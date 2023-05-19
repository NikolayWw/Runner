using CodeBase.Services.Factory;
using CodeBase.Services.GameObserverReporter;
using CodeBase.StaticData.Tile.Collectable;
using CodeBase.UI.Services.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerCollect : MonoBehaviour
    {
        [SerializeField] private Transform _playerBody;
        public Action OnCollect;

        private IGameFactory _gameFactory;
        private IGameObserverReporterService _reporterService;

        private readonly List<PlayerCollectedObject> _collectableList = new();
        private int _touchCounter;
        private IUIFactory _uiFactory;

        public void Construct(IGameFactory gameFactory, IUIFactory uiFactory, IGameObserverReporterService gameObserverReporterService)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _reporterService = gameObserverReporterService;

            _reporterService.OnGameOver += OnGameOver;
            _reporterService.OnPlayerTakeHinder += CreateCollectText;
        }

        private void Start()
        {
            CreateFirstCollectable();
        }

        private void LateUpdate()
        {
            CheckEndGame();
        }

        public void IncrementTouchCounter()
        {
            _touchCounter++;
        }

        private void CreateFirstCollectable()
        {
            PlayerCollectedObject collectedObject = _gameFactory.CreateCollectablePlayer(CollectableId.Cube1, transform, transform, 1);//start height
            Register(collectedObject);
        }

        private void CreateCollectable(CollectableId id)
        {
            PlayerCollectedObject collectedObject = _gameFactory.CreateCollectablePlayer(id, transform, transform, CurrentHeight());
            Register(collectedObject);
            OnCollect?.Invoke();
        }

        private void Register(PlayerCollectedObject collectedObject)
        {
            _collectableList.Add(collectedObject);
            collectedObject.OnCollect += CreateCollectable;
            collectedObject.OnTouchHinder += IncrementTouchCounter;
            collectedObject.OnClosed += RemoveFromList;
        }

        private void RemoveFromList(PlayerCollectedObject collectableObject) =>
            _collectableList.Remove(collectableObject);

        private float CurrentHeight() =>
            _collectableList.Count + 1f;

        private void CheckEndGame()
        {
            if (_touchCounter >= _collectableList.Count)
            {
                foreach (PlayerCollectedObject collected in _collectableList)
                    collected.Freeze();

                _reporterService.SendEndGame();
                _uiFactory.CreateEndGameMenu();
            }
            else if (_touchCounter != 0)
            {
                _reporterService.SendPlayerTakeHinder();
                _touchCounter = 0;
            }
            else
            {
                _touchCounter = 0;
            }
        }

        private void CreateCollectText()
        {
            StartCoroutine(CreateCollectTextEndFrame());
        }

        private void OnGameOver()
        {
            StopAllCoroutines();
            _reporterService.OnPlayerTakeHinder -= CreateCollectText;
            enabled = false;
        }

        private IEnumerator CreateCollectTextEndFrame()
        {
            yield return new WaitForEndOfFrame();
            _gameFactory.CreateCollectText(_playerBody.position + Vector3.up);
        }
    }
}