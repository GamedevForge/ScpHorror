using CoopHorror.CodeBase.Advanced;
using CoopHorror.CodeBase.Items;
using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase.Inventory
{
    public class PlayerHand : NetworkBehaviour
    {
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private AdvancedPlayerInput _input;

        private IUsable _currentUsableItem;

        public override void Spawned()
        {
            _playerInventory.ItemDataSelected += SetUsableItem;
            _playerInventory.ItemDataSet += SetUsableItem;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _playerInventory.ItemDataSelected -= SetUsableItem;
            _playerInventory.ItemDataSet -= SetUsableItem;
        }

        public override void FixedUpdateNetwork()
        {
            if (_input.CurrentInput.Actions.WasPressed(_input.PreviousInput.Actions, AdvancedInput.MOUSE_LEFT_BUTTON) 
                && _currentUsableItem != null)
            {
                _currentUsableItem.RpcUse(GetComponent<NetworkObject>());
            }
        }

        private void SetUsableItem(IItemData itemData)
        {
            if (itemData == null)
            {
                _currentUsableItem = null;
                return;
            }
            
            if (itemData.ItemPrefab.TryGetComponent(out IUsable usableItem))
            {
                _currentUsableItem = usableItem;
                return;
            }

            _currentUsableItem = null;
        }
    }
}