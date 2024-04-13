using UnityEngine;

namespace Scripts.Player
{
    public class ShootState : ConditionalState
    {
        private float m_CurrentTime = 0f;

        public ShootState()
        {
            mListOfConditionalStates.Add(EPlayerState.MOVE);
        }

        public override void Update() 
        {
            mPlayerConfig.m_AimMagnitude = mInputService.AimAxis.magnitude;
            if (mPlayerConfig.m_AimMagnitude > mPlayerConfig.m_ShootAxisAt)
            {
                HandleShoot();
            }
        }
        void HandleShoot()
        {
            if(Time.time > m_CurrentTime)
            {
                m_CurrentTime = Time.time + 1.0f/mPlayerConfig.m_FireRate;
            }
        }
    }

}
