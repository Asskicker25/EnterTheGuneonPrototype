using DG.Tweening;
using UnityEngine;

namespace Scripts.Player
{
    public class ReviveState : BaseState
    {
        private bool mReviveDone = false;

        private float mTimeStep = 0;
        private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> m_Tween;
        public override void Start()
        {
            mTimeStep = 0;
            mReviveDone = false;
        }

        public override void Update()
        {
            if (mReviveDone) return;

            mTimeStep += Time.deltaTime;

            if(mTimeStep > m_PlayerConfig.m_ReviveDelaytime)
            {
                Revive();
            }
        }

        private void Revive()
        {
            mReviveDone = true;
            m_PlayerView.m_Animator.Play(PlayerAnimationStrings.m_Revive);
            m_Tween = m_PlayerView.transform.DOMove(m_PlayerView.m_LastFloorPosition, m_PlayerConfig.m_ReviveLerpTime);
        }

        public void OnRevieEnd()
        {
            m_PlayerService.ChangeState(EPlayerState.MOVE);
        }

        public override void Cleanup()
        {
            m_Tween.Kill();
        }
    }
}
