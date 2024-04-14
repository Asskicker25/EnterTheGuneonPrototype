using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class ConditionalState : BaseState
    {
        public List<EPlayerState> m_ListOfConditionalStates = new List<EPlayerState>();
    }
}
