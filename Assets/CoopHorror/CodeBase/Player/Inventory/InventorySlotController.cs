using UnityEngine;
using CoopHorror.CodeBase.Advanced;
using Fusion;
using System;

namespace CoopHorror.CodeBase.Inventory
{
    public class InventorySlotController : NetworkBehaviour
    {
        [SerializeField] private SlotNumber _slotNumber;
        [SerializeField] private AdvancedPlayerInput _input;

        public event Action Selected;

        public override void FixedUpdateNetwork()
        {
            if (_input.CurrentInput.Actions.WasPressed(_input.PreviousInput.Actions, _slotNumber))
            {
                Selected?.Invoke();
            }
        }
    }
}