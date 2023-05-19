using System;
using UnityEngine;

namespace CodeBase.Services.GameObserverReporter
{
    public interface IGameObserverReporterService : IService
    {
        Action OnGameOver { get; set; }
        Action OnPlayerTakeHinder { get; set; }
        Action<Transform, float> OnHinderTileGenerate { get; set; }

        void Cleanup();

        void SendEndGame();

        void SendPlayerTakeHinder();

        void SendHinderTileGenerate(Transform tileTransform, float lastTilePositionZ);
    }
}