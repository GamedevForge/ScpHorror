using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase.Rooms
{
    public class RoomSpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject[] _roomPrefabs;
        [SerializeField] private List<RoomSpawnHandler> _roomSpawnObservers;
        [SerializeField] private RoomCount _roomCount;

        private List<RoomSpawnObserver> _doorTriggerObservers;
        private NetworkRunner _networkRunner;

        public event Action OnRoomSpawn;
        public event Action OnDespawn;
        
        private void Awake()
        {
            _networkRunner = FindObjectOfType<NetworkRunner>();

            foreach (RoomSpawnHandler roomSpawnObserver in _roomSpawnObservers)
            {
                roomSpawnObserver.PlayerTriggered += OnPlayerTriggered;
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            OnDespawn?.Invoke();

            foreach (RoomSpawnHandler roomSpawnObserver in _roomSpawnObservers)
            {
                roomSpawnObserver.PlayerTriggered -= OnPlayerTriggered;
            }

            base.Despawned(runner, hasState);
        }

        private void OnPlayerTriggered(List<RoomSpawnObserver> doorTriggerObservers)
        {
            _doorTriggerObservers = doorTriggerObservers;
            
            if (HasStateAuthority)
            {
                SpawnRoom();
            }
            else
            {
                RpcSpawnRoom(PlayerRef.None);
            }
        }
        
        [Rpc(RpcSources.Proxies, RpcTargets.StateAuthority)]
        private void RpcSpawnRoom([RpcTarget] PlayerRef player)
        {
            SpawnRoom();
        }

        private void SpawnRoom()
        {
            NetworkObject roomPrefab = _roomPrefabs[UnityEngine.Random.Range(0, _roomPrefabs.Length)];

            foreach (RoomSpawnObserver doorTriggerObserver in _doorTriggerObservers)
            {
                switch (doorTriggerObserver.DoorType)
                {
                    case DoorType.Front:
                        RunnerSpawn(roomPrefab, doorTriggerObserver, new Vector3(0, 0, 5));
                        break;
                    case DoorType.Back:
                        RunnerSpawn(roomPrefab, doorTriggerObserver, new Vector3(0, 0, -5));
                        break;
                    case DoorType.Left:
                        RunnerSpawn(roomPrefab, doorTriggerObserver, new Vector3(-5, 0, 0));
                        break;
                    case DoorType.Right:
                        RunnerSpawn(roomPrefab, doorTriggerObserver, new Vector3(5, 0, 0));
                        break;
                }
            }
        }

        private void RunnerSpawn(NetworkObject roomPrefab, RoomSpawnObserver spawnObserver, Vector3 offset)
        {
            spawnObserver.IsSpawned = true;
            
            NetworkObject room = Runner.Spawn(roomPrefab, spawnObserver.DoorPosition + offset,
                Quaternion.identity);
            
            RoomSpawnHandler roomSpawnHandler = room.GetComponentInChildren<RoomSpawnHandler>();
            _roomSpawnObservers.Add(roomSpawnHandler);
            roomSpawnHandler.PlayerTriggered += OnPlayerTriggered;

            DoorNumber doorNumber = room.GetComponentInChildren<DoorNumber>();
            doorNumber.RpcInit(_roomCount);
            _networkRunner.AddCallbacks(doorNumber);
            
            OnRoomSpawn?.Invoke();
        }
    }
}