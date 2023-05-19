using System;
using UnityEngine;

namespace CodeBase.StaticData.Tile.Collectable
{
    [Serializable]
    public class CollectableConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public CollectableId Id { get; private set; }
        [field: SerializeField] public GameObject PrefabGroup { get; private set; }
        [field: SerializeField] public GameObject PrefabCollectable { get; private set; }

        public void OnValidate()
        {
            _inspectorName = Id.ToString();
        }
    }
}