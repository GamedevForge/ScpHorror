using UnityEngine;

namespace CoopHorror.CodeBase
{
    public class InventoryCanvas : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
