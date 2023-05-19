using CodeBase.StaticData.CollectText;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.Tile.Collectable;
using CodeBase.StaticData.Tile.Hinder;
using CodeBase.StaticData.Tile.Track;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();

        HinderTileStaticData HinderTileStaticData { get; }
        TrackTileStaticData TrackTileStaticData { get; }
        CollectTextConfig CollectTextConfig { get; }
        PlayerStaticData PlayerStaticData { get; }
        CollectableStaticData CollectableStaticData { get; }

        HinderTileGroupConfig ForHinderTile(HinderTileGroupId groupId);

        CollectableConfig ForCollectable(CollectableId id);

        TrackTileConfig ForTrackTile(TrackTileId id);
    }
}