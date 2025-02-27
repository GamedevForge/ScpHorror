using CoopHorror.CodeBase.Inventory;
using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase.Items
{
    public class ItemBattery : NetworkBehaviour, IInteractable, IConsumable
    {
        [SerializeField] private float _powar;
        
        private PlayerInventory _playerInventory;
        private IChargeable _chargeable;
        
        public void Interact(NetworkObject gameObject)
        {
            _playerInventory = gameObject.GetComponent<PlayerInventory>();

            if (_playerInventory.CurrentItemData.ItemPrefab.TryGetComponent(out IChargeable chargeable))
            {
                _chargeable = chargeable;
                RpcUseUp();
                RpcDestroy();
            }
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcDestroy()
        {
            Runner.Despawn(GetComponent<NetworkObject>());
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcUseUp()
        {
            _chargeable.AddLifeTime(_powar);
        }
    }
}