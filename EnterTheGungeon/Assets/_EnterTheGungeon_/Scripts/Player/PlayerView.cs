using UnityEngine;
using Input = RedLabsGames.Utls.Input.ActiveInputController;

namespace Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jump");
            }
        }
    }
}

