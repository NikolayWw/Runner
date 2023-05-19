using System;
using UnityEngine;

namespace CodeBase.StaticData.Tile.Hinder
{
    [Serializable]
    public class HinderTileGroupConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public HinderTileGroupId GroupId { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public void OnValidate()
        {
            _inspectorName = GroupId.ToString();
        }
    }
}