using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [CreateAssetMenu(menuName = "Static Data/Player Static Data", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        [field: SerializeField] public float DeadPushForce { get; private set; } = 15f;
        [field: SerializeField] public float MoveX { get; private set; } = 4f;
        [field: SerializeField] public float MoveForward { get; private set; } = 5f;
        [field: SerializeField] public CameraConfig CameraConfig { get; private set; }
    }
}