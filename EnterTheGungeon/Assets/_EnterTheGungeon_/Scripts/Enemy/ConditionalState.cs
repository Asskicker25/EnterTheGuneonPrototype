using Scripts.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    public class ConditionalState : BaseState
    {
        public List<EEnemyState> m_ListOfConditionalStates = new List<EEnemyState>();
    }
}

