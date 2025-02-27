using Fusion;
using UHFPS.Runtime;
using UnityEngine;

namespace CoopHorror.CodeBase
{
    public class CrateAnimator : NetworkBehaviour 
    {
        [SerializeField] private NetworkMecanimAnimator _animator;
        [SerializeField] private LockpickInteract _lockpickInteract;

        private readonly string _open = "Open";

        public void OpenCrate()
        {
            _lockpickInteract.InteractStart();
            _animator.SetTrigger(_open);
        }
    }
}