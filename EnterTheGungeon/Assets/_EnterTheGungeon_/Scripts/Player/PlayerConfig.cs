using RedLabsGames.Utls.Input;
using UnityEngine;

namespace Scripts.Player
{
    [CreateAssetMenu(menuName = "Configs/Player/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public PlayerView mPlayerView;
        public InputController mInputController;
    }
}
