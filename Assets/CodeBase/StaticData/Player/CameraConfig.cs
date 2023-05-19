using System;
using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [Serializable]
    public class CameraConfig
    {
        [field: SerializeField] public int ShakeCount { get; private set; } = 10;
        [field: SerializeField] public float ShakeSpeed { get; private set; } = 0.005f;
    }
}