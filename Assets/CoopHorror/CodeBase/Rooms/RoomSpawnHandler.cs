using System.Collections.Generic;
using Fusion;
using UnityEngine;
using System;

namespace CoopHorror.CodeBase.Rooms
{
    public class RoomSpawnHandler : NetworkBehaviour
    {
        [SerializeField] private List<RoomSpawnObserver> _doorTriggerObservers;

        private List<RoomSpawnObserver> _spawnableTriggers;

        public event Action<List<RoomSpawnObserver>> PlayerTriggered;
        
        private void Awake()
        {
            _spawnableTriggers = _doorTriggerObservers;
            
            foreach (RoomSpawnObserver doorTriggerObserver in _doorTriggerObservers)
            {
                doorTriggerObserver.PlayerTriggered += OnPlayerTriggered;
                doorTriggerObserver.DoorTriggered += OnDoorTriggered;
            }
        }

        private void OnPlayerTriggered(RoomSpawnObserver roomSpawnObserver)
        {
            PlayerTriggered?.Invoke(_spawnableTriggers);
        }
        
        private void OnDoorTriggered(RoomSpawnObserver roomSpawnObserver)
        {
            _spawnableTriggers.Remove(roomSpawnObserver);
            roomSpawnObserver.DoorTriggered -= OnDoorTriggered;
            
            if(roomSpawnObserver.IsSpawned)
                roomSpawnObserver.PlayerTriggered -= OnPlayerTriggered;
        }
    }
}