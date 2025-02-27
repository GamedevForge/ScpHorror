using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase
{
    public class Wardrobe : NetworkBehaviour, IInteractable
    {
        [SerializeField] private Transform _positionInFrontOfCabinet;
        [SerializeField] private Transform _shelterPosition;
        [SerializeField] private WardrobeAnimator _waedrobeAnimator;
        [SerializeField] private BoxCollider _doorCollider;

        [Networked] public bool IsActive { get; private set; } = false;
        [Networked] private bool _doorColliderActive { get; set; } = true;

        private HiddingPlayer _hiddingPlayer;

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        private void RPC_ChangeActive()
        {
            IsActive = !IsActive;
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        private void RPC_OpenDoors()
        {
            _waedrobeAnimator.OpenWardrobe();
        }

        [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
        private void RPC_CloseDoors()
        {
            _waedrobeAnimator.CloseWardrobe();
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_CloseDoorCollider()
        {
            _doorColliderActive = true;
            _doorCollider.enabled = _doorColliderActive;
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_OpenDoorCollider()
        {
            _doorColliderActive = false;
            _doorCollider.enabled = _doorColliderActive;
        }

        public void Interact(NetworkObject gameObject)
        {
            if (IsActive)
            {
                return;
            }

            RPC_ChangeActive();

            _hiddingPlayer = gameObject.GetComponent<HiddingPlayer>();

            _hiddingPlayer.HiddingStopped += ReturnOriginalState;
            _hiddingPlayer.HiddingStopped += RPC_CloseDoors;
            _hiddingPlayer.DoorOpened += RPC_OpenDoors;
            _hiddingPlayer.DoorClosed += RPC_CloseDoors;
            _hiddingPlayer.DoorOpened += RPC_OpenDoorCollider;
            _hiddingPlayer.DoorClosed += RPC_CloseDoorCollider;

            _hiddingPlayer.StartHiding(_positionInFrontOfCabinet, _shelterPosition, transform);
        }

        public void ReturnOriginalState()
        {
            RPC_ChangeActive();
            _hiddingPlayer.HiddingStopped -= ReturnOriginalState;
            _hiddingPlayer.HiddingStopped -= RPC_CloseDoors;
            _hiddingPlayer.DoorOpened -= RPC_OpenDoors;
            _hiddingPlayer.DoorClosed -= RPC_CloseDoors;
            _hiddingPlayer.DoorOpened -= RPC_OpenDoorCollider;
            _hiddingPlayer.DoorClosed -= RPC_CloseDoorCollider;
        }
    }
}