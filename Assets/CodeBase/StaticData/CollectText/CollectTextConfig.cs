using UnityEngine;

namespace CodeBase.StaticData.CollectText
{
    [CreateAssetMenu(menuName = "Static Data/Collect Text Config", order = 0)]
    public class CollectTextConfig : ScriptableObject
    {
        [field: SerializeField] public float PlayCloseAnimationDelay { get; private set; }

        private void OnValidate()
        {
            if (PlayCloseAnimationDelay < 0)
                PlayCloseAnimationDelay = 0;
        }
    }
}