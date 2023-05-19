using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Tile.Hinder
{
    [CreateAssetMenu(menuName = "Static Data/Hinder Static Data", order = 0)]
    public class HinderTileStaticData : ScriptableObject
    {
        [field: SerializeField] public int StartTilesCount { get; private set; } = 5;
        [field: SerializeField] public float StartTileOffsetForward { get; private set; } = 10f;
        [field: SerializeField] public int DistanceBetweenTiles { get; private set; } = 5;
        [field: SerializeField] public int LifeTimeAfterTriggered { get; private set; } = 5;
        [field: SerializeField] public List<HinderTileGroupConfig> TileConfigs { get; private set; }

        private void OnValidate()
        {
            TileConfigs.ForEach(x => x.OnValidate());
        }
    }
}