using UnityEngine;
using UnityEngine.Events;
using UHFPS.Tools;
using TMPro;
using ThunderWire.Attributes;
using Fusion;
using CoopHorror.CodeBase.Advanced;
using System;

namespace UHFPS.Runtime
{
    [Docs("https://docs.twgamesdev.com/uhfps/guides/puzzles#lockpick-puzzle")]
    public class LockpickInteract : NetworkBehaviour, IDynamicUnlock, IInteractStart
    {
        public NetworkObject LockpickModel;

        [Range(-90f, 90f)]
        public float UnlockAngle;
        public bool RandomUnlockAngle;
        public bool IsDynamicUnlockComponent;

        public Vector3 LockpickRotation;
        public float LockpickDistance;
        public GString LockpicksText;
        public ControlsContext[] ControlsContexts;

        public ItemGuid BobbyPinItem;
        public MinMax BobbyPinLimits;

        public float BobbyPinUnlockDistance = 0.1f;
        public float BobbyPinLifetime = 2;
        public bool UnbreakableBobbyPin;

        [Range(0f, 90f)]
        public float KeyholeMaxTestRange = 20;
        public float KeyholeUnlockTarget = 0.1f;

        public UnityEvent OnUnlock;
        public event Action OnUnlockEvent;

        public GameObject LockpickUI;
        public TMP_Text LockpickText;

        public AdvancedPlayer advancedPlayer;
        public DynamicObject DynamicObject;
        public GameManager GameManager;

        private Camera MainCamera;
        private bool isUnlocked;
        private NetworkObject lockpickObj;


        private void Awake()
        {
            MainCamera = Camera.main;
            if (RandomUnlockAngle) UnlockAngle = Mathf.Floor(GameTools.Random(BobbyPinLimits));
        }

        private void Start()
        {
            foreach (var control in ControlsContexts)
            {
                control.SubscribeGloc();
            }

            LockpicksText.SubscribeGloc();
        }

        public void Init(AdvancedPlayer advancedPlayer)
        {
            this.advancedPlayer = advancedPlayer;
        }

        public void InteractStart()
        {
            if (IsDynamicUnlockComponent || isUnlocked) 
                return;

            AttemptToUnlock();
        }

        public void OnTryUnlock(DynamicObject dynamicObject)
        {
            if (!IsDynamicUnlockComponent || isUnlocked) 
                return;

            DynamicObject = dynamicObject;
            AttemptToUnlock();
        }

        public void AttemptToUnlock()
        {
            Vector3 holdPosition = MainCamera.transform.position + MainCamera.transform.forward * LockpickDistance;
            Quaternion faceRotation = Quaternion.LookRotation(MainCamera.transform.forward) * Quaternion.Euler(LockpickRotation);
            lockpickObj = Runner.Spawn(LockpickModel, holdPosition, faceRotation);
            LockpickComponent lockpickComponent = lockpickObj.GetComponent<LockpickComponent>();

            lockpickComponent.SetLockpick(this, advancedPlayer);
        }

        public void Unlock()
        {
            if (isUnlocked) 
                return;

            if (IsDynamicUnlockComponent && DynamicObject != null) 
                DynamicObject.TryUnlockResult(true);
            else
            { 
                OnUnlock?.Invoke();
                OnUnlockEvent?.Invoke();
                //DestroyInteract();
            }

            isUnlocked = true;
        }
        public void DestroyInteract()
        {
            if (lockpickObj != null)
                Runner.Despawn(lockpickObj);
        }
    }

}