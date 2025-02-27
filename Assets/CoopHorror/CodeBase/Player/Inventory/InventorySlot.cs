using UnityEngine;
using System;
using CoopHorror.CodeBase.Items;

namespace CoopHorror.CodeBase.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public event Action<InventorySlot> Selected;
        
        public IItemData ItemData { get; private set; }
        public bool IsActive { get; private set; } = false;
        public bool ItemDataIsSet { get; private set; } = false;

        [SerializeField] private InventorySlotController _inventorySlotController;
        [SerializeField] private InventorySlotViewController _inventorySlotViewController;

        private void Awake()
        {
            _inventorySlotController.Selected += Activate;
        }

        private void OnDestroy()
        {
            _inventorySlotController.Selected -= Activate;
        }

        public void Activate()
        {
            IsActive = true;
            Selected?.Invoke(this);
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void SetItemData(IItemData itemData)
        {
            ItemData = itemData;
            _inventorySlotViewController.DrawSlotSprite(itemData.ItemData.View);
            ItemDataIsSet = true;
        }

        public void ResetItemData()
        {
            if (ItemData.ItemPrefab.TryGetComponent(out IItemDisposable itemDisposable))
            {
                itemDisposable.OnDispose -= ResetItemData;
            }
            
            ItemData = null;
            _inventorySlotViewController.ResetSprite();
            ItemDataIsSet = false;
        }
    }
}