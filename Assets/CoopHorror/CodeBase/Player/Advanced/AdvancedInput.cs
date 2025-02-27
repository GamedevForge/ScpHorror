using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase.Advanced
{
	/// <summary>
	/// Input structure polled by Fusion. This is sent over network and processed by server, keep it optimized and remove unused data.
	/// </summary>
	public struct AdvancedInput : INetworkInput
	{
		public Vector2        MoveDirection;
		public Vector2        LookRotationDelta;
		public NetworkButtons Actions;

		public const int JUMP_BUTTON   = 0;
		public const int SPRINT_BUTTON = 1;
		
		public const int INTERACT_BUTTON = 2;
		public const int QUIT_BUTTON = 3;
		
		public const int INVENTORY_SLOT_1 = 4;
        public const int INVENTORY_SLOT_2 = 5;
        public const int INVENTORY_SLOT_3 = 6;
        public const int INVENTORY_SLOT_4 = 7;
        public const int INVENTORY_SLOT_5 = 8;
        public const int INVENTORY_SLOT_6 = 9;
        public const int INVENTORY_SLOT_7 = 10;
        public const int INVENTORY_SLOT_8 = 11;
        public const int INVENTORY_SLOT_9 = 12;

		public const int MOUSE_LEFT_BUTTON = 13;
    }
}
