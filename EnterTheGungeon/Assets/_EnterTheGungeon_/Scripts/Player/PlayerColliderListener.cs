using System;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerColliderListener : MonoBehaviour
    {
        public static event Action<Collider2D> OnEnemyTriggerEnter = delegate { };

        public void OnTriggerEnter2D(Collider2D collision)
        {
            OnEnemyTriggerEnter.Invoke(collision);
        }
    }
}
