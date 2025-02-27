using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase.Items
{
    public class UsableItemObject : NetworkBehaviour, IUsable
    {
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcUse(NetworkObject playerObject)
        {
            Debug.Log("Use");
        }
    }
}