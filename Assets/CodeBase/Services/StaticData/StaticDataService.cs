using CodeBase.StaticData.CollectText;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.Tile.Collectable;
using CodeBase.StaticData.Tile.Hinder;
using CodeBase.StaticData.Tile.Track;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string CollectableStaticDataPath = "StaticData/CollectableStaticData";
        private const string HinderTileStaticDataPath = "StaticData/HinderTileStaticData";
        private const string TrackTileStaticDataPath = "StaticData/TrackTileStaticData";
        private const string CollectTextConfigPath = "StaticData/CollectTextConfig";
        private const string PlayerStaticDataPath = "StaticData/PlayerStaticData";
        public HinderTileStaticData HinderTileStaticData { get; private set; }
        public TrackTileStaticData TrackTileStaticData { get; private set; }
        public CollectableStaticData CollectableStaticData { get; private set; }
        public CollectTextConfig CollectTextConfig { get; private set; }
        public PlayerStaticData PlayerStaticData { get; private set; }
        private Dictionary<HinderTileGroupId, HinderTileGroupConfig> _hinderTiles;
        private Dictionary<CollectableId, CollectableConfig> _collectables;
        private Dictionary<TrackTileId, TrackTileConfig> _trackTiles;

        public void Load()
        {
            LoadCollectableTileData();
            LoadHinderTileData();
            LoadTrackTileData();
            CollectTextConfig = Resources.Load<CollectTextConfig>(CollectTextConfigPath);
            PlayerStaticData = Resources.Load<PlayerStaticData>(PlayerStaticDataPath);
        }

        private void LoadCollectableTileData()
        {
            CollectableStaticData = Resources.Load<CollectableStaticData>(CollectableStaticDataPath);
            _collectables = CollectableStaticData.Configs.ToDictionary(x => x.Id, x => x);
        }

        private void LoadTrackTileData()
        {
            TrackTileStaticData = Resources.Load<TrackTileStaticData>(TrackTileStaticDataPath);
            _trackTiles = Resources.Load<TrackTileStaticData>(TrackTileStaticDataPath).Configs.ToDictionary(x => x.Id, x => x);
        }

        private void LoadHinderTileData()
        {
            HinderTileStaticData = Resources.Load<HinderTileStaticData>(HinderTileStaticDataPath);
            _hinderTiles = Resources.Load<HinderTileStaticData>(HinderTileStaticDataPath).TileConfigs.ToDictionary(x => x.GroupId, x => x);
        }

        public HinderTileGroupConfig ForHinderTile(HinderTileGroupId groupId) =>
            _hinderTiles.TryGetValue(groupId, out var config) ? config : null;

        public CollectableConfig ForCollectable(CollectableId id) =>
            _collectables.TryGetValue(id, out var config) ? config : null;

        public TrackTileConfig ForTrackTile(TrackTileId id) =>
            _trackTiles.TryGetValue(id, out var config) ? config : null;
    }
}