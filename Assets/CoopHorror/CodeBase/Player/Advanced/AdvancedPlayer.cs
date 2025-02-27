using Fusion;
using Fusion.Addons.KCC;
using UnityEngine;

namespace CoopHorror.CodeBase.Advanced 
{
	[DefaultExecutionOrder(-5)]
	public sealed class AdvancedPlayer : NetworkBehaviour
	{	
		[field: SerializeField] public HiddingPlayer HiddingPlayer { get; private set; }
        [field: SerializeField] public PlayerData PlayerData { get; private set; }

        private Vector3 _originalExternalDelta => KCC.Data.ExternalDelta;

        [SerializeField] private float _accelerationScale;

        public KCC                 KCC;
		public AdvancedPlayerInput Input;
		public Transform           CameraPivot;
		public Transform           CameraHandle;
		public Model Model;
		public Hands Hands;
		public InventoryCanvas InventoryCanvas;

		public bool RestrictedInMovement { get; private set; } = false;
		public bool RestrictedInRotation { get; private set; } = false;
		public bool RestricredInAcceleration { get; private set; } = false;

        public override void FixedUpdateNetwork()
		{
            if (HiddingPlayer.HiddingIsStarted)
			{
				HiddingPlayer.Hide();
				return;
			}

			if (RestrictedInMovement)
			{
				Debug.Log("true");
				return;
			}

			if (RestricredInAcceleration)
			{
                KCC.SetExternalDelta(KCC.Data.TransformRotation
                * new Vector3(Input.CurrentInput.MoveDirection.x, 0.0f, Input.CurrentInput.MoveDirection.y)
                * _accelerationScale);
            }
			else
			{
				KCC.SetExternalDelta(_originalExternalDelta);
            }

			Vector3 inputDirection = KCC.Data.TransformRotation * new Vector3(Input.CurrentInput.MoveDirection.x, 0.0f, Input.CurrentInput.MoveDirection.y);
			KCC.SetInputDirection(inputDirection, false);


            KCC.AddLookRotation(Input.CurrentInput.LookRotationDelta);
			
			if (Input.CurrentInput.Actions.WasPressed(Input.PreviousInput.Actions, AdvancedInput.JUMP_BUTTON))
			{
				if (KCC.Data.IsGrounded == true)
				{
					KCC.Jump(Vector3.up * 6.0f);
				}
			}
		}

		public void BanMovement()
		{
			RestrictedInMovement = true;
		}

		public void AllowMovement()
		{
            RestrictedInMovement = false;
		}

		public void BanCameraRotation()
		{
			RestrictedInRotation = true;
		}

		public void AllowCameraRotaion()
		{
			RestrictedInRotation = false;
		}

		public void AddSpeed()
		{
            RestricredInAcceleration = true;
        }

		public void ResetSpeed()
		{
            RestricredInAcceleration = false;
        }

        private void LateUpdate()
		{
			if (HasInputAuthority == false || RestrictedInRotation)
				return;

			Vector2 pitchRotation = KCC.Data.GetLookRotation(true, false);
			CameraPivot.localRotation = Quaternion.Euler(pitchRotation);
			
			Camera.main.transform.SetPositionAndRotation(CameraPivot.position, CameraPivot.rotation);
		}
	}
}
