using UnityEngine;
using Zenject;
using Scripts.Dungeon;

namespace Scripts.Player
{
    public class PlayerService : IPlayerService
    {
        
        [Inject]
        private void Construct()
        {
        }

        public void SpawnPlayer(Vector3 position, Quaternion rotation)
        {
        }

    }
}
