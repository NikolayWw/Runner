using CodeBase.StaticData;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Logic
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadInitialScene(() =>
                _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded))));
        }

        private static IEnumerator LoadInitialScene(Action initSceneLoaded)
        {
            if (SceneManager.GetActiveScene().name != GameConstants.InitialScene)
            {
                var waitInitScene = SceneManager.LoadSceneAsync(GameConstants.InitialScene);

                do
                {
                    yield return null;
                } while (waitInitScene.isDone == false);
            }

            initSceneLoaded?.Invoke();
        }

        private static IEnumerator LoadScene(string name, Action onLoaded)
        {
            if (name == SceneManager.GetActiveScene().name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitScene = SceneManager.LoadSceneAsync(name);
            do
            {
                yield return null;
            } while (waitScene.isDone == false);

            onLoaded?.Invoke();
        }
    }
}