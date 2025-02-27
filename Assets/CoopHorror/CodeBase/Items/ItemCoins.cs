using CoopHorror.CodeBase.Advanced;
using Fusion;

namespace CoopHorror.CodeBase.Items
{
    public class ItemCoins : NetworkBehaviour, IInteractable, IConsumable
    {
        [SerializableType] private int _value;
        
        private PlayerData _playerData;
        
        public void Interact(NetworkObject gameObject)
        {
            _playerData = gameObject.GetComponent<AdvancedPlayer>().PlayerData;

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
            _playerData.AddCoins(_value);
        }
    }
}