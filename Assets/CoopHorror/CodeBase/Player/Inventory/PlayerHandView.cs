using CoopHorror.CodeBase.Items;
using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase.Inventory
{
    public class PlayerHandView : NetworkBehaviour
    {
        [field:SerializeField] public Transform HandPoint { get; private set; }
        
        [SerializeField] private PlayerInventory _inventory;
        [SerializeField] private NetworkObject _playerNetworkObject;

        private IItem _currentItem;
        private NetworkObject _currentItemPrefab;

        public override void Spawned()
        {
            _inventory.ItemDataSelected += TakeItem;
            _inventory.ItemDataSet += SetItem;
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _inventory.ItemDataSet -= SetItem;
            _inventory.ItemDataSelected -= TakeItem;
        }

        private void SetItem(IItem itemData)
        {
            itemData.ItemPrefab.GetComponent<NetworkTransform>().SyncParent = true;
            itemData.ItemPrefab.transform.SetParent(HandPoint);
            itemData.ItemPrefab.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            TakeItem(itemData);
        }

        private void TakeItem(IItem itemData)
        {
            if (_currentItemPrefab != null)
            {
                if (_currentItemPrefab.TryGetComponent(out IConsumableWhitDelay consumableWhitDelay)
                    && consumableWhitDelay.IsActive)
                {
                    _currentItemPrefab.GetComponent<IItemData>().ItemView.gameObject.SetActive(false);
                }
                else
                    _currentItemPrefab.gameObject.SetActive(false);
            }

            if (itemData == null)
            {
                _currentItem = null;
                
                return;
            }

            _currentItem = itemData;

            _currentItemPrefab = _currentItem.ItemPrefab;
            _currentItemPrefab.gameObject.SetActive(true);
        }
    }
}