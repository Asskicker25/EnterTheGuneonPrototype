
using Scripts.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
  

    public interface IPlayerService
    {

        public abstract void SpawnPlayer(Vector3 position, Quaternion rotation);
        public abstract void DestroyPlayer();
        public abstract void ReturnToHome();
        public abstract void EquipWeapon();

        public PlayerView PlayerView { get; }

        #region STATE_MACHINE

        public abstract void AddState(EPlayerState eState, BaseState state);
        public abstract void RemoveState(EPlayerState eState);
        public abstract void ChangeState(EPlayerState eState);

        public abstract void AddConditionalState(EPlayerState eState, ConditionalState state);
        public abstract void RemoveConditionalState(EPlayerState eState);

        public abstract BaseState GetCurrentState();
        public abstract BaseState GetState(EPlayerState eState);
        public abstract ConditionalState GetConditionalState(EPlayerState eState);

        public EPlayerState CurrentStateID { get; }

        public Vector3 CurrentPosition { get; }

        #endregion
    }
}
