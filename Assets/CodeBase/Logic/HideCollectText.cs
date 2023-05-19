using CodeBase.Services.StaticData;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
    public class HideCollectText : MonoBehaviour
    {
        private readonly int HideHash = Animator.StringToHash("Hide");

        [SerializeField] private Animator _animator;
        private float _playCloseAnimationDelay;

        public void Construct(IStaticDataService dataService)
        {
            _playCloseAnimationDelay = dataService.CollectTextConfig.PlayCloseAnimationDelay;
        }

        private void Start()
        {
            StartCoroutine(HideDelay());
        }

        private void Hide()//use event animation
        {
            Destroy(gameObject);
        }

        private IEnumerator HideDelay()
        {
            yield return new WaitForSeconds(_playCloseAnimationDelay);
            _animator.Play(HideHash, 0, 0);
        }
    }
}