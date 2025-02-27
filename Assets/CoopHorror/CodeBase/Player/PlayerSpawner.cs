using CoopHorror.CodeBase.Advanced;
using Fusion;
using System;
using UnityEngine;

namespace CoopHorror.CodeBase
{
    public class PlayerSpawner : NetworkBehaviour
    {
        [Networked] private bool _gameIsReady { get; set; }
        
        [SerializeField] private NetworkPrefabRef _playerNetworkPrefab;
        [SerializeField] private Transform[] _spawnPoints;

        public event Action PlayerSpawned;

        public override void Spawned()
        {
            if(_gameIsReady)
                SpawnPlayer(Runner.LocalPlayer);
        }
        
        public void StartPlayerSpawner()
        {
            _gameIsReady = true;
            RpcInitialSpaceshipSpawn();
        }
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RpcInitialSpaceshipSpawn()
        {
            SpawnPlayer(Runner.LocalPlayer);
        }

        private void SpawnPlayer(PlayerRef player)
        {
            int index = player.PlayerId % _spawnPoints.Length;
            Vector3 spawnPosition = _spawnPoints[index].position;

            NetworkObject playerObject = Runner.Spawn(_playerNetworkPrefab, spawnPosition, Quaternion.identity, player);

            AdvancedPlayer advancedPlayer = playerObject.GetComponent<AdvancedPlayer>();
            advancedPlayer.Model.Hide();
            advancedPlayer.Hands.Show();
            advancedPlayer.InventoryCanvas.Show();

            Runner.SetPlayerObject(player, playerObject);

            PlayerSpawned?.Invoke();
        }
    }
}
