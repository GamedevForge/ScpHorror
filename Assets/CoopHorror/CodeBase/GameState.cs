using System;
using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase
{
    [RequireComponent(typeof(PlayerSpawner))]
    public class GameState : NetworkBehaviour
    {
        public enum GamePhase
        {
            Starting,
            Running,
            Ending
        }
        
        [Networked] private GamePhase _phase { get; set; }
        private PlayerSpawner _playerSpawner { get; set; }
        
        private void Awake()
        {
            GetComponent<NetworkObject>().Flags |= NetworkObjectFlags.MasterClientObject;
            _playerSpawner = GetComponent<PlayerSpawner>();
        }
        
        public override void Spawned()
        {
            if (Object.HasStateAuthority)
            {
                _phase = GamePhase.Starting;
            }
        }
        
        public override void Render()
        {
            switch (_phase)
            {
                case GamePhase.Starting:
                    UpdateStartingDisplay();
                    break;
                case GamePhase.Running:
                    break;
                case GamePhase.Ending:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void UpdateStartingDisplay()
        {
            if (!Object.HasStateAuthority) 
                return;

            _playerSpawner.StartPlayerSpawner();
            
            _phase = GamePhase.Running;
        }
    }
}