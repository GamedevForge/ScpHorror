using UnityEngine;
using UnityEngine.UI;

namespace CoopHorror.CodeBase.Inventory
{
    [RequireComponent(typeof(InventorySlotViewController))]
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private Sprite _sprite;

        private void Awake()
        {
            _sprite = (Sprite)Resources.Load("SolidColor");
        }

        public void DrawSlot(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void ResetView()
        {
            _image.sprite = _sprite;
        }
    }
}