using Fusion;
using UnityEngine;
using UnityEngine.Events;

namespace CoopHorror.CodeBase
{
    public class PlayerRayCaster : NetworkBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private LayerMask _rayCastBarrier;
        [SerializeField] private float _distance;

        public NetworkObject CurrentIntectableObject { get; private set; }

        private Vector3 _rayStartPosition = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        public event UnityAction<IInteractable> OnRaycast;

        public override void FixedUpdateNetwork()
        {
            Ray ray = Camera.main.ScreenPointToRay(_rayStartPosition);

            if (Physics.Raycast(ray, _distance, _rayCastBarrier))
            {
                return;
            }

            if (Physics.Raycast(ray, out RaycastHit hit, _distance, _layerMask))
            {
                CurrentIntectableObject = hit.transform.GetComponent<NetworkObject>();
            }
            else
            {
                CurrentIntectableObject = null;
            }
        }
    }
}
