using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Tile.Collectable
{
    [CreateAssetMenu(menuName = "Static Data/Collectable Static Data", order = 0)]
    public class CollectableStaticData : ScriptableObject
    {
        [field: SerializeField] public List<CollectableConfig> Configs { get; private set; }

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}