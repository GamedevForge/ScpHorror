using Fusion;
using UnityEngine;
using CoopHorror.CodeBase.Advanced;
using System;

namespace CoopHorror.CodeBase
{
    public class PlayerInteract : NetworkBehaviour
    {
        [SerializeField] private PlayerRayCaster _playerRayCaster;
        [SerializeField] private AdvancedPlayerInput _input;

        private NetworkObject _playerObject;

        public event Action Quit;

        private void Awake()
        {
            _playerObject = GetComponent<NetworkObject>();
        }

        public override void FixedUpdateNetwork()
        {
            if (_input.CurrentInput.Actions.WasPressed(_input.PreviousInput.Actions, AdvancedInput.INTERACT_BUTTON))
            {
                if (_playerRayCaster.CurrentIntectableObject != null)
                {
                    _playerRayCaster.CurrentIntectableObject.GetComponent<IInteractable>().Interact(_playerObject);
                }
            }

            if (_input.CurrentInput.Actions.WasPressed(_input.PreviousInput.Actions, AdvancedInput.QUIT_BUTTON))
            {
                Quit?.Invoke();
            }
        }
    }
}
