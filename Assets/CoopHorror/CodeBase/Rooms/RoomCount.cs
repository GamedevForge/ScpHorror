using Fusion;
using System;
using UnityEngine;

namespace CoopHorror.CodeBase.Rooms
{
    public class RoomCount : NetworkBehaviour
    {
        [Networked] public int Count { get; private set; } = 0;
        
        [SerializeField] private RoomSpawner _roomSpawner;

        public event Action CountChanged;
        public event Action OnUnsubscribe;

        private void Awake()
        {
            _roomSpawner.OnRoomSpawn += RpcAddCount;
            _roomSpawner.OnDespawn += UnsubcribeOnEvent;
        }

        private void UnsubcribeOnEvent()
        {
            OnUnsubscribe?.Invoke();
            _roomSpawner.OnRoomSpawn -= RpcAddCount;
            _roomSpawner.OnDespawn -= UnsubcribeOnEvent;
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        private void RpcAddCount()
        {
            Count++;
            CountChanged?.Invoke();
        }
    }
}