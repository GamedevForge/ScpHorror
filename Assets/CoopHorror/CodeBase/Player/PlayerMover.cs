using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase
{
    public class PlayerMover : NetworkBehaviour
    {
        private CharacterController _controller;

        public float PlayerSpeed = 2f;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        public override void FixedUpdateNetwork()
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * PlayerSpeed;

            _controller.Move(move);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
        }
    }
}
