using UnityEngine;

namespace Scripts.Player
{
    public enum EPlayerState
    {
        NONE = 0,
        MOVE = 1,
        DODGE_ROLL = 2,
        ANY = 3,
        SHOOT = 4, 
        AIM= 5,
        FALL_CHECK = 6,
        DEATH = 7, 
        REVIVE = 8,
    }
}
