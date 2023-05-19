using CodeBase.Player;
using CodeBase.StaticData.Tile.Collectable;
using CodeBase.StaticData.Tile.Hinder;
using CodeBase.StaticData.Tile.Track;
using CodeBase.Tiles.HinderTiles;
using CodeBase.Tiles.TrackTiles;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Vector3 position, float leftClamp, float rightClamp);

        HinderTileGroup CreateHinderTileGroup(HinderTileGroupId groupId, Vector3 at);

        void CreateCollectableGroupPiece(CollectableId id, Transform parent, Vector3 at);

        PlayerCollectedObject CreateCollectablePlayer(CollectableId id, Transform parent, Transform follow, float height);

        TrackGround CreateTrackGround(TrackTileId id, Vector3 at);

        void SetMainCamera(UnityEngine.Camera mainCamera);

        void CreateTrackGenerator();

        void CreateCollectText(Vector3 at);

        void CreateHinderGenerator();

        void CreateCollectableGenerator();
    }
}