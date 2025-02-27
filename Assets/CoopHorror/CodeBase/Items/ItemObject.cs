using CoopHorror.CodeBase.Inventory;
using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase.Items
{
    public class ItemObject : NetworkBehaviour, IItem, IInteractable, IBeingDestroyed
    {
        [Networked] private bool _isPicked { get; set; } = false;

        [field:SerializeField] public ItemData ItemData { get; private set; }

        [SerializeField] private NetworkObject _fbx;

        public NetworkObject ItemPrefab => 
            GetComponent<NetworkObject>();

        public NetworkObject ItemView => _fbx;

        public void Interact(NetworkObject gameObject)
        {
            if (_isPicked)
            {
                return;
            }

            gameObject.GetComponent<PlayerInventory>().SetCurrentItem(this);
            RpcPickItem();
            RpcDestroy();
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcPickItem()
        {
            _isPicked = true;
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcDestroy()
        {
            Runner.Despawn(ItemPrefab);
        }
    }
}