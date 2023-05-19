using CodeBase.Services.GameObserverReporter;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Camera
{
    public class CameraShake : MonoBehaviour
    {
        private IGameObserverReporterService _reporter;
        private CameraConfig _config;

        public void Construct(IGameObserverReporterService gameObserverReporterService, IStaticDataService dataService)
        {
            _reporter = gameObserverReporterService;
            _config = dataService.PlayerStaticData.CameraConfig;
            _reporter.OnPlayerTakeHinder += () => StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            var shakeWait = new WaitForSeconds(_config.ShakeSpeed);
            Quaternion startRotate = transform.rotation;

            for (int i = 0; i < _config.ShakeCount; i++)
            {
                Vector2 randomPos = Random.insideUnitCircle;
                transform.eulerAngles += new Vector3(randomPos.x, randomPos.y);
                yield return shakeWait;
            }

            transform.rotation = startRotate;
        }
    }
}