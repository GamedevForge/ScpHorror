using UnityEngine;

namespace CoopHorror.CodeBase.Inventory
{
    [RequireComponent(typeof(InventorySlotView))]
    public class InventorySlotViewController : MonoBehaviour
    {
        private InventorySlotView _inventorySlotView;

        private void Awake()
        {
            _inventorySlotView = GetComponent<InventorySlotView>();
        }

        public void DrawSlotSprite(Sprite sprite)
        {
            _inventorySlotView.DrawSlot(sprite);
        }

        public void ResetSprite()
        {
            _inventorySlotView.ResetView();
        }
    }
}