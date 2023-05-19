using System;
using UnityEngine;

namespace CodeBase.Services.GameObserverReporter
{
    public class GameObserverReporterService : IGameObserverReporterService
    {
        public Action OnGameOver { get; set; }
        public Action OnPlayerTakeHinder { get; set; }
        public Action<Transform, float> OnHinderTileGenerate { get; set; }

        public void SendPlayerTakeHinder() =>
            OnPlayerTakeHinder?.Invoke();

        public void SendHinderTileGenerate(Transform tileTransform, float lastTilePositionZ) =>
            OnHinderTileGenerate?.Invoke(tileTransform, lastTilePositionZ);

        public void SendEndGame() =>
            OnGameOver?.Invoke();

        public void Cleanup()
        {
            OnGameOver = null;
            OnPlayerTakeHinder = null;
            OnHinderTileGenerate = null;
        }
    }
}