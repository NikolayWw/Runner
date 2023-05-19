using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Tile.Track
{
    [CreateAssetMenu(menuName = "Static Data/Track Tile Config Container", order = 0)]
    public class TrackTileStaticData : ScriptableObject
    {
        [field: SerializeField] public float LeftBorder { get; private set; } = -2.1f;
        [field: SerializeField] public float RightBorder { get; private set; } = 2.1f;
        [field: SerializeField] public float CheckNewTileDelay { get; private set; } = 1f;
        [field: SerializeField] public List<TrackTileConfig> Configs { get; private set; }

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}