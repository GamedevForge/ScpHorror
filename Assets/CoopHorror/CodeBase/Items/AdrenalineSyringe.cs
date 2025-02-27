using CoopHorror.CodeBase.Advanced;
using Fusion;
using System;
using UnityEngine;

namespace CoopHorror.CodeBase.Items
{
    public class AdrenalineSyringe : NetworkBehaviour, IUsable, IItemDisposable, IConsumableWhitDelay
    {
        public event Action OnDispose;

        public bool IsActive { get; private set; } = false;
        
        private ItemObject _itemObject => GetComponent<ItemObject>();
        
        [SerializeField] private float _time;

        private AdvancedPlayer _advancedPlayer;
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcUse(NetworkObject playerObject)
        {
            if (IsActive)
            {
                return;
            }

            _advancedPlayer = playerObject.GetComponent<AdvancedPlayer>();
            _itemObject.ItemView.gameObject.SetActive(false);
            IsActive = true;
            _advancedPlayer.AddSpeed();
            OnDispose?.Invoke();
        }

        public override void FixedUpdateNetwork()
        {
            if (IsActive == false)
            {
                return;
            }
            
            if (_time <= 0f)
            {
                _advancedPlayer.ResetSpeed();
                _itemObject.RpcDestroy();
                return;
            }

            _time -= Runner.DeltaTime;
        }
    }
}