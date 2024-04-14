using Scripts.Weapon;
using System;
using UnityEngine;
using Zenject;

namespace Scripts.Player
{
    public class WeaponReloadState : ConditionalState
    {
        public static event Action OnReloadStart = delegate { };
        public static event Action OnReloadEnd = delegate { };

        private bool m_ReloadStart = false;
        public float m_CurrentReloadTime = 0;

        private bool IsMagFull
        {
            get
            {
               return m_PlayerConfig.m_WeaponConfig.m_CurrentMagSize.Equals(
                   m_PlayerConfig.m_WeaponConfig.m_TotalMagSize);
            }
        }

        public WeaponReloadState()
        {
            m_ListOfConditionalStates.Add(EPlayerState.MOVE);
            m_ListOfConditionalStates.Add(EPlayerState.DODGE_ROLL);
        }

        public override void Start()
        {
            m_InputService.OnReloadPressed += OnReloadPressed;
            ShootState.OnAutoReload += OnAutoReload;
        }
      
        public override void Update()
        {
            HandleReload();
        }

        public override void Cleanup()
        {
            m_InputService.OnReloadPressed -= OnReloadPressed;
            ShootState.OnAutoReload -= OnAutoReload;
        }

        private void OnAutoReload()
        {
            ReloadStart();
        }

        private void OnReloadPressed()
        {
            if (m_PlayerService.CurrentStateID == EPlayerState.MOVE || m_PlayerService.CurrentStateID == EPlayerState.DODGE_ROLL)
            {
                if (!m_ReloadStart && !IsMagFull)
                {
                    ReloadStart();
                }
            }
        }

        private void ReloadStart()
        {
            m_ReloadStart = true;
            m_CurrentReloadTime = 0;
            m_PlayerView.m_WeaponReloadView.Show();

            m_PlayerView.m_Weapon.m_Animator.Play(PlayerAnimationStrings.m_PistolReload);

            OnReloadStart.Invoke();
        }

        private void HandleReload()
        {
            if (!m_ReloadStart) return;

            m_CurrentReloadTime += Time.deltaTime;

            if (m_CurrentReloadTime > m_PlayerConfig.m_WeaponConfig.m_ReloadTime)
            {
                RefillMag();
            }
        }

        private void RefillMag()
        {
            m_PlayerConfig.m_WeaponConfig.m_CurrentMagSize = m_PlayerConfig.m_WeaponConfig.m_TotalMagSize;
            m_ReloadStart = false;

            OnReloadEnd.Invoke();
        }


    }
}
