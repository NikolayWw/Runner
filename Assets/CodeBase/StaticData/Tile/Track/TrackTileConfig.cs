using System;
using UnityEngine;

namespace CodeBase.StaticData.Tile.Track
{
    [Serializable]
    public class TrackTileConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public TrackTileId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}