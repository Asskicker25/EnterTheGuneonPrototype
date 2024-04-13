using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class PlayerAnimationEventListener : MonoBehaviour
    {
        [Inject] IPlayerService m_PlayerService;

        public void OnDodgeVelocityChange()
        {
            ((DodgeRollState)m_PlayerService.GetState(EPlayerState.DODGE_ROLL)).OnVelocityChanged();
        }

        public void OnDodgeEnd()
        {
            ((DodgeRollState)m_PlayerService.GetState(EPlayerState.DODGE_ROLL)).OnDodgeEnd();
        }
    }
}
