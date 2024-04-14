using Scripts.Bullet;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Player
{
    public class FallCheckState : ConditionalState
    {
        private ContactFilter2D m_ContactFilters;
        private List<Collider2D> m_ListOfOverlaps = new List<Collider2D>();

        private bool m_IsFalling = false;

        public FallCheckState() 
        {
            m_ListOfConditionalStates.Add(EPlayerState.MOVE);
            m_ContactFilters = new ContactFilter2D();
        }

        public override void Start()
        {
            m_ContactFilters.layerMask = m_PlayerConfig.m_OverLapCheckLayer;
            m_ContactFilters.useDepth = false;

            DodgeRollState.OnDodgeComplete += OnDodgeEnd;
        }
      
        public override void FixedUpdate()
        {
            if(m_InputService.InputAxis.magnitude >  0.0f)
            {
                CheckFallAndSwitchState();
            }
        }

        public override void Cleanup()
        {
            DodgeRollState.OnDodgeComplete -= OnDodgeEnd;
        }

        private bool IsFalling()
        {
            m_ListOfOverlaps = Physics2D.OverlapCircleAll(m_PlayerView.transform.position, 
                m_PlayerConfig.m_FallCheckRadius, m_PlayerConfig.m_OverLapCheckLayer).ToList();


            if(m_ListOfOverlaps.Count > 0)
            {
                int waterColliderCount = 0;
                foreach(Collider2D collider in m_ListOfOverlaps)
                {
                    if ((m_PlayerConfig.m_FallLayer & (1 << collider.gameObject.layer)) != 0)
                    {
                        waterColliderCount++;
                    }
                }

                if (waterColliderCount == m_ListOfOverlaps.Count)
                {
                    return m_IsFalling = true;
                }

                if (waterColliderCount == 0)
                {
                    m_PlayerView.m_LastFloorPosition = m_PlayerView.transform.position;
                }
            }


            return m_IsFalling = false;
        }

        private void OnDodgeEnd()
        {
            CheckFallAndSwitchState();
        }

        private void CheckFallAndSwitchState()
        {
            if (IsFalling())
            {
                m_PlayerService.ChangeState(EPlayerState.DEATH);
            }
        }
    }
}
