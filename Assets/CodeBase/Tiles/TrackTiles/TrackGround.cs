using System.Collections;
using UnityEngine;

namespace CodeBase.Tiles.TrackTiles
{
    public class TrackGround : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;

        public void Close() =>
            StartCoroutine(DestroyThis());

        public float EndZPosition() =>
            transform.position.z + _collider.size.z / 2f;

        private IEnumerator DestroyThis()
        {
            yield return new WaitForEndOfFrame();
            Destroy(gameObject);
        }
    }
}