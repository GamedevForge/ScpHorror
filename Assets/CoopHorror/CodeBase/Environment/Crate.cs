using Fusion;
using UHFPS.Runtime;
using UnityEngine;
using CoopHorror.CodeBase.Advanced;

namespace CoopHorror.CodeBase
{
    public class Crate : NetworkBehaviour, IInteractable
    {
        [Networked] private bool _isOpen { get; set; } = false;
        [Networked] private bool _inProgress { get; set; } = false;
        
        [SerializeField] private CrateAnimator _crateAnimator;
        [SerializeField] private LockpickInteract _lockpickInteract;

        private PlayerInteract _playerInteract;
        private AdvancedPlayer _advancedPlayer;

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcOpenCrate()
        {
            _crateAnimator.OpenCrate();
            _lockpickInteract.OnUnlockEvent -= RpcOpenCrate;
            RpcAllowMovement();
            _lockpickInteract.DestroyInteract();

            _playerInteract = null;
            _advancedPlayer = null;

            _isOpen = true;
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcInitLockPick(AdvancedPlayer advancedPlayer)
        {
            _lockpickInteract.Init(advancedPlayer);
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcQuit()
        {
            if (_playerInteract == null)
            {
                return;
            }
            
            _playerInteract.Quit -= RpcQuit;
            _lockpickInteract.OnUnlockEvent -= RpcOpenCrate;
            
            RpcDeactivateCrate();
            RpcAllowMovement();

            _playerInteract = null;
            _advancedPlayer = null;
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcDeactivateCrate()
        {
            _inProgress = false;
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcActivateCrate()
        {
            _inProgress = true;
        }


        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcAllowMovement()
        {
            if (_advancedPlayer == null)
            {
                return;
            }

            _advancedPlayer.AllowMovement();
            _advancedPlayer.AllowCameraRotaion();
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcBanMovemnet()
        {
            if (_advancedPlayer == null)
            {
                return;
            }

            _advancedPlayer.BanMovement();
            _advancedPlayer.BanCameraRotation();
        }

        public void Interact(NetworkObject gameObject)
        {
            if (_isOpen || _inProgress)
            {
                return;
            }

            _advancedPlayer = gameObject.GetComponent<AdvancedPlayer>();
            _playerInteract = gameObject.GetComponent<PlayerInteract>();

            RpcActivateCrate();
            RpcInitLockPick(_advancedPlayer);
            _lockpickInteract.InteractStart();

            _playerInteract.Quit += RpcQuit;
            _lockpickInteract.OnUnlockEvent += RpcOpenCrate;
            RpcBanMovemnet();
        }
    }
}