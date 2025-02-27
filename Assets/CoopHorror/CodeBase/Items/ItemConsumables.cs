using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase.Items
{
    public class ItemConsumables : NetworkBehaviour, IInteractable, IConsumable
    {
        public void Interact(NetworkObject gameObject)
        {
            RpcUseUp();
            RpcDestroy();
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcDestroy()
        {
            Runner.Despawn(GetComponent<NetworkObject>());
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcUseUp()
        {
            Debug.Log("UseUp");
        }
    }
}