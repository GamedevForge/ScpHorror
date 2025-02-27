using CoopHorror.CodeBase.Items;
using Fusion;
using System;
using UnityEngine;

namespace CoopHorror.CodeBase.Inventory
{
    public class PlayerInventory : NetworkBehaviour
    {
        public event Action<IItem> ItemDataSelected;
        public event Action<IItem> ItemDataSet;

        public IItem CurrentItemData { get; private set; }
        
        [SerializeField] private InventoryController _inventoryController;

        private NetworkObject _currentObject;

        public void SetCurrentItem(IItem item)
        {
            _currentObject = Runner.Spawn(item.ItemData.Prefab);
            CurrentItemData = _currentObject.GetComponent<IItem>();

            _inventoryController.SetCurrentItem(CurrentItemData);
            ItemDataSet?.Invoke(CurrentItemData);
        }

        public override void Spawned()
        {
            _inventoryController.CurrentSlotChange += ChangeCurrentItemData;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _inventoryController.CurrentSlotChange -= ChangeCurrentItemData;
        }

        private void ChangeCurrentItemData(IItem itemData)
        {
            CurrentItemData = itemData;
            ItemDataSelected?.Invoke(CurrentItemData);
        }
    }
}