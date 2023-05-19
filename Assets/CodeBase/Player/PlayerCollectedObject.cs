using CodeBase.Logic;
using CodeBase.StaticData.Tile.Collectable;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerCollectedObject : MonoBehaviour, ICollect, IHinderTouch
    {
        [SerializeField] private Transform _rootTransform;

        public Action<CollectableId> OnCollect;
        public Action OnTouchHinder;
        public Action<PlayerCollectedObject> OnClosed;

        private bool _isTouch;

        public void Freeze()
        {
            StopAllCoroutines();
        }

        public void Collect(CollectableId id) =>
            OnCollect?.Invoke(id);

        public void TouchHinder()
        {
            if (_isTouch)
                return;

            _isTouch = true;

            OnTouchHinder?.Invoke();
            StartCoroutine(TryDestroyThisEndFrame());
        }

        private IEnumerator TryDestroyThisEndFrame()
        {
            yield return new WaitForEndOfFrame();
            OnClosed?.Invoke(this);
            Destroy(_rootTransform.gameObject);
        }
    }
}