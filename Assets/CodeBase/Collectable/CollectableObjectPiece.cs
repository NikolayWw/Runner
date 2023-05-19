using CodeBase.Logic;
using CodeBase.StaticData.Tile.Collectable;
using UnityEngine;

namespace CodeBase.Collectable
{
    public class CollectableObjectPiece : MonoBehaviour
    {
        [SerializeField] private CollectedObjectCollisionReporter _reporter;
        private CollectableId _id = CollectableId.Cube1;

        public void Construct(CollectableId id)
        {
            _id = id;
        }

        private void Start()
        {
            _reporter.OnCollision += Collect;
        }

        private void Collect()
        {
            _reporter.OnCollision -= Collect;
            _reporter.PlayerCollected?.Collect(_id);
            Destroy(gameObject);
        }
    }
}