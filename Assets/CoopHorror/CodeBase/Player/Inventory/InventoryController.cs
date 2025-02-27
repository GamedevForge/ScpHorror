using CoopHorror.CodeBase.Items;
using System;
using UnityEngine;

namespace CoopHorror.CodeBase.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        public event Action<IItem> CurrentSlotChange;
        
        public InventorySlot _currentInventorySlot { get; private set; }
        
        [SerializeField] private InventorySlot[] _inventorySlots;

        private void Awake()
        {   
            foreach (InventorySlot slot in _inventorySlots)
            {
                slot.Selected += SetCurrentSlot;
            }
        }

        private void OnDestroy()
        {
            foreach (InventorySlot slot in _inventorySlots)
            {
                slot.Selected -= SetCurrentSlot;
            }
        }

        private void SetCurrentSlot(InventorySlot inventorySlot)
        {
            if (_currentInventorySlot == null)
            {
                _currentInventorySlot = inventorySlot;
                CurrentSlotChange?.Invoke((IItem)_currentInventorySlot.ItemData);
            }
            else if (_currentInventorySlot == inventorySlot)
            {
                return;
            }
            else
            {
                _currentInventorySlot.Deactivate();
                _currentInventorySlot = inventorySlot;
                CurrentSlotChange?.Invoke((IItem)_currentInventorySlot.ItemData);
            }
        }

        public void SetCurrentItem(IItemData itemData)
        {
            foreach(InventorySlot slot in _inventorySlots)
            {
                if (slot.ItemDataIsSet == false)
                {
                    _currentInventorySlot = slot;
                    _currentInventorySlot.SetItemData(itemData);
                    _currentInventorySlot.Activate();

                    if (itemData.ItemPrefab.TryGetComponent(out IItemDisposable itemDisposable))
                    {
                        itemDisposable.OnDispose += _currentInventorySlot.ResetItemData;
                    }

                    return;
                }
            }
        }
    }
}