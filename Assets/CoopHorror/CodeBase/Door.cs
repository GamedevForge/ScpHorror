using CoopHorror.CodeBase.Rooms;
using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase
{
    public class Door : NetworkBehaviour
    {
        [SerializeField] private RoomSpawnObserver _roomSpawnObserver;

        private NetworkMecanimAnimator _animator;
        private readonly string _openAnimation = "Open";

        private void Awake()
        {
            _animator = GetComponent<NetworkMecanimAnimator>();
            _roomSpawnObserver.PlayerTriggered += OpenTheDoor;
        }

        private void OnDestroy()
        {
            _roomSpawnObserver.PlayerTriggered -= OpenTheDoor;
        }

        private void OpenTheDoor(RoomSpawnObserver roomSpawnObserver)
        {
            _animator.SetTrigger(_openAnimation);
            _roomSpawnObserver.PlayerTriggered -= OpenTheDoor;
        }
    }
}