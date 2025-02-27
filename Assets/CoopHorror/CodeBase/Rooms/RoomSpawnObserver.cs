using CoopHorror.CodeBase.Advanced;
using Fusion;
using UnityEngine;
using System;

namespace CoopHorror.CodeBase.Rooms
{
    public class RoomSpawnObserver : NetworkBehaviour
    {
        [SerializeField] private Transform _door;

        [field: SerializeField] public DoorType DoorType { get; private set; }
        public Vector3 DoorPosition => _door.position;
        public bool IsSpawned;

        public event Action<RoomSpawnObserver> PlayerTriggered;
        public event Action<RoomSpawnObserver> DoorTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out RoomSpawnObserver _))
            {
                TryDisable();
                DoorTriggered?.Invoke(this);
            }
            else if (other.TryGetComponent(out AdvancedPlayer _))
            {
                PlayerTriggered?.Invoke(this);
            }
        }

        private void TryDisable()
        {
            if(IsSpawned)
                gameObject.SetActive(false);
        }
    }

    public enum DoorType
    {
        Front,
        Back,
        Right,
        Left
    }
}