using CodeBase.Logic;
using CodeBase.Services.GameObserverReporter;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerApplyTouchHinder : MonoBehaviour, IHinderTouch
    {
        [SerializeField] private PlayerCollect _collect;

        public void Construct(IGameObserverReporterService gameObserverReporterService, IUIFactory uiFactory)
        {
            //_reporterService = gameObserverReporterService;
            //_uiFactory = uiFactory;
        }

        public void TouchHinder()
        {
            _collect.IncrementTouchCounter();
        }
    }
}