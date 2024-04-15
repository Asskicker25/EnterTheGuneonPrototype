using Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI
{
    public class RestartWindow : UIWindow
    {
        public Button m_RestartButton;

        private IPlayerService m_PlayerService;

        [Inject]
        private void Construct(IPlayerService playerService)
        {
            m_PlayerService = playerService;

            m_RestartButton.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            m_PlayerService.ReturnToHome();
        }
    }
}
