using CodeBase.StaticData.Tile.Collectable;

namespace CodeBase.Logic
{
    public interface ICollect
    {
        void Collect(CollectableId id);
    }
}