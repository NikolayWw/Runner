﻿using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);

        GameObject Instantiate(string path, Transform parent);

        GameObject Instantiate(string path, Vector3 at);
    }
}