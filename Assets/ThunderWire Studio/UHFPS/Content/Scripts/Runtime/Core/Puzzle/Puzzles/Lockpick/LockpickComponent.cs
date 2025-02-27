using System;
using System.Collections;
using UnityEngine;
using UHFPS.Input;
using UHFPS.Tools;
using Fusion;
using CoopHorror.CodeBase.Advanced;
using Fusion.Addons.KCC;

namespace UHFPS.Runtime
{
    public class LockpickComponent : NetworkBehaviour
    {
        public AudioSource AudioSource;
        public Transform BobbyPin;
        public Transform LockpickKeyhole;
        public Transform KeyholeCopyLocation;

        public Axis BobbyPinForward;
        public Axis KeyholeForward;

        public float BobbyPinRotateSpeed = 0.1f;
        public float BobbyPinResetTime = 1;
        public float BobbyPinShakeAmount = 3;

        public float KeyholeUnlockAngle = -90;
        public float KeyholeRotateSpeed = 2;
        public float sensivity = 15f;

        public SoundClip Unlock;
        public SoundClip BobbyPinBreak;

        private LockpickInteract lockpick;
        private AdvancedPlayer advancedPlayer;

        private bool isActive;
        private bool isUnlocked;

        private MinMax keyholeLimits;
        private float bobbyPinAngle;
        private float keyholeAngle;
        private float keyholeTarget;

        private float keyholeTestRange;
        private float bobbyPinLifetime;
        private float bobbyPinUnlockDistance;
        private float keyholeUnlockTarget;

        private int bobbyPins;
        private float bobbyPinTime;
        private bool canUseBobbyPin;
        private bool tryUnlock = false;
        private Vector2 originalPostion;

        public void SetLockpick(LockpickInteract lockpick, AdvancedPlayer advancedPlayer)
        {
            this.lockpick = lockpick;
            this.advancedPlayer = advancedPlayer;

            originalPostion = advancedPlayer.KCC.Data.GetLookRotation();
            keyholeLimits = new MinMax(0, KeyholeUnlockAngle);
            keyholeTestRange = lockpick.KeyholeMaxTestRange;
            bobbyPinLifetime = lockpick.BobbyPinLifetime;
            bobbyPinUnlockDistance = lockpick.BobbyPinUnlockDistance;
            keyholeUnlockTarget = lockpick.KeyholeUnlockTarget;

            bobbyPinTime = bobbyPinLifetime;
            bobbyPins = lockpick.BobbyPinItem.Quantity;
            bobbyPins = 4;
            BobbyPin.gameObject.SetActive(bobbyPins > 0);

            UpdateLockpicksText();

            canUseBobbyPin = true;
            isActive = true;
        }

        public override void FixedUpdateNetwork()
        {
            RpcLockPiclLogic();
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcLockPiclLogic()
        {
            if (!isActive)
                return;

            if (KeyholeCopyLocation != null)
            {
                Vector3 copyPosiiton = KeyholeCopyLocation.position;
                Vector3 bobbyPinPosition = new Vector3(copyPosiiton.x, copyPosiiton.y, copyPosiiton.z);
                BobbyPin.position = bobbyPinPosition;
            }

            if (advancedPlayer.Input.CurrentInput.Actions.WasPressed(advancedPlayer.Input.PreviousInput.Actions, AdvancedInput.QUIT_BUTTON))
            {
                UnuseLockpick();
                return;
            }

            /*if (InputManager.ReadButtonOnce(GetInstanceID(), Controls.EXAMINE))
            {
                UnuseLockpick();
                return;
            }*/

            if (advancedPlayer.Input.CurrentInput.Actions.WasPressed(advancedPlayer.Input.PreviousInput.Actions, AdvancedInput.JUMP_BUTTON))
            {
                tryUnlock = true;
            }

            if (advancedPlayer.Input.CurrentInput.Actions.WasReleased(advancedPlayer.Input.PreviousInput.Actions, AdvancedInput.JUMP_BUTTON))
            {
                tryUnlock = false;
            }

            keyholeTarget = tryUnlock ? KeyholeUnlockAngle : 0;

            float bobbyPinDiff = Mathf.Abs(lockpick.UnlockAngle - bobbyPinAngle);
            float bobbyPinNormalized = 0;
            float bobbyPinShake = 0;

            if (bobbyPins > 0 && canUseBobbyPin && !isUnlocked)
            {
                bool damageBobbyPin = false;
                if (!tryUnlock)
                {
                    Vector2 bobbyPinMove = new Vector2(advancedPlayer.Input.CurrentInput.LookRotationDelta.y,
                        advancedPlayer.Input.CurrentInput.LookRotationDelta.x);
                    bobbyPinAngle += bobbyPinMove.x * BobbyPinRotateSpeed * Runner.DeltaTime;
                }
                else
                {
                    damageBobbyPin = true;
                    float randomShake = UnityEngine.Random.insideUnitCircle.x;
                    bobbyPinShake = UnityEngine.Random.Range(-randomShake, randomShake) * BobbyPinShakeAmount;

                    if (bobbyPinDiff <= keyholeTestRange)
                    {
                        bobbyPinNormalized = 1 - (bobbyPinDiff / keyholeTestRange);
                        bobbyPinNormalized = (float)Math.Round(bobbyPinNormalized, 2);
                        float targetDiff = Mathf.Abs(keyholeTarget - keyholeAngle);
                        float targetNormalized = targetDiff / keyholeTestRange;

                        if (bobbyPinNormalized >= (1 - bobbyPinUnlockDistance))
                        {
                            bobbyPinNormalized = 1;
                            damageBobbyPin = false;
                            bobbyPinShake = 0;

                            if (targetNormalized <= keyholeUnlockTarget)
                            {
                                UnuseLockpick();
                                StartCoroutine(OnUnlock());
                                isUnlocked = true;
                            }
                        }
                    }
                }

                if (damageBobbyPin && !lockpick.UnbreakableBobbyPin)
                {
                    if (bobbyPinTime > 0)
                    {
                        bobbyPinTime -= Runner.DeltaTime;
                    }
                    else
                    {
                        //bobbyPins = Inventory.Instance.RemoveItem(lockpick.BobbyPinItem, 1);
                        BobbyPin.gameObject.SetActive(false);
                        bobbyPinTime = bobbyPinLifetime;
                        UpdateLockpicksText();

                        StartCoroutine(ResetBobbyPin());
                        AudioSource.PlayOneShotSoundClip(BobbyPinBreak);

                        canUseBobbyPin = false;
                        bobbyPinAngle = 0;
                    }
                }

                bobbyPinAngle = Mathf.Clamp(bobbyPinAngle, -90, 90);
                BobbyPin.localRotation = Quaternion.AngleAxis(bobbyPinAngle + bobbyPinShake, BobbyPinForward.Convert());
            }

            if (isUnlocked)
            {
                keyholeTarget = KeyholeUnlockAngle;
                bobbyPinNormalized = 1f;
            }

            keyholeTarget *= bobbyPinNormalized;
            keyholeAngle = Mathf.MoveTowardsAngle(keyholeAngle, keyholeTarget, Time.deltaTime * KeyholeRotateSpeed * 100);
            keyholeAngle = Mathf.Clamp(keyholeAngle, keyholeLimits.RealMin, keyholeLimits.RealMax);
            LockpickKeyhole.localRotation = Quaternion.AngleAxis(keyholeAngle, KeyholeForward.Convert());
        }

        private void UnuseLockpick()
        {
            //playerManager.PlayerItems.IsItemsUsable = true;
            //gameManager.FreezePlayer(false);
            //gameManager.SetBlur(false, true);
            //gameManager.ShowPanel(GameManager.PanelType.MainPanel);
            //gameManager.ShowControlsInfo(false, null);
            //lockpick.LockpickUI.SetActive(false);

            Runner.Despawn(GetComponent<NetworkObject>());
        }

        private void UpdateLockpicksText()
        {
            string text = lockpick.LockpicksText;
            text = text.RegexReplaceTag('[', ']', "count", bobbyPins.ToString());
            //lockpick.LockpickText.text = text;
        }

        IEnumerator ResetBobbyPin()
        {
            yield return new WaitForSeconds(BobbyPinResetTime);

            if (bobbyPins > 0)
            {
                BobbyPin.gameObject.SetActive(true);
                canUseBobbyPin = true;
            }
            else UnuseLockpick();
        }

        IEnumerator OnUnlock()
        {
            lockpick.Unlock();

            if(Unlock.audioClip != null) 
                AudioSource.PlayOneShotSoundClip(Unlock);

            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => !AudioSource.isPlaying);
        }
    }
}