using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase
{
    public class WardrobeAnimator : NetworkBehaviour
    {
        [SerializeField] private NetworkMecanimAnimator _animator;

        private readonly string _open = "Open";
        private readonly string _close = "Close";

        public void OpenWardrobe()
        {
            _animator.SetTrigger(_open);
        }

        public void CloseWardrobe()
        {
            _animator.SetTrigger(_close);
        }
    }
}