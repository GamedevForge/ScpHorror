using Fusion;
using System;
using UnityEngine;

namespace CoopHorror.CodeBase.Items
{
    public class RegenerationSyringe : NetworkBehaviour, IUsable, IItemDisposable 
    {
        public event Action OnDispose;
        
        [SerializeField] private float _healthValue;
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcUse(NetworkObject playerObject)
        {
            PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();

            playerHealth.AddHealth(_healthValue);
            OnDispose?.Invoke();
        }
    }
}