using UnityEngine;
using Input = RedLabsGames.Utls.Input.ActiveInputController;

namespace Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] Vector2 mInputAxis = Vector2.zero;

        private void Update()
        {
            mInputAxis.x = Input.GetAxis("Horizontal");
            mInputAxis.y = Input.GetAxis("Vertical");

            mInputAxis.Normalize();

        }
    }
}

