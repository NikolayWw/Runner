using CodeBase.Logic;
using System;
using UnityEngine;

namespace CodeBase.Tiles.HinderTiles
{
    public class HinderTile : MonoBehaviour
    {
        private bool _isTouch;
        public Action OnTouch;

        private void OnCollisionEnter(Collision collision)
        {
            if (_isTouch)
                return;

            if (collision.collider.TryGetComponent(out IHinderTouch hinderTouch))
            {
                float difference = Math.Abs(Mathf.Ceil(collision.transform.position.y) - transform.position.y);
                if (difference == 0)
                {
                    _isTouch = true;
                    hinderTouch.TouchHinder();
                    OnTouch?.Invoke();
                }
            }
        }
    }
}