using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase.Items
{
    public class FLashlight : NetworkBehaviour, IUsable, IChargeable
    {
        [Networked] private bool IsActive { get; set; } = false;
        [Networked] private bool IsDischarged { get; set; } = false;

        private float _maxLifeTime => _lifeTime;

        [SerializeField] private Light _light;
        [SerializeField] private float _lifeTime;

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcUse(NetworkObject playerObject)
        {
            IsActive = !IsActive;
            _light.enabled = IsActive && (!IsDischarged);
        }

        public void AddLifeTime(float time)
        { 
            _lifeTime += time;

            if (_lifeTime > _maxLifeTime)
                _lifeTime = _maxLifeTime;

            IsDischarged = false;
            IsActive = false;
        }

        public override void FixedUpdateNetwork()
        {
            if (IsActive)
            {
                _lifeTime -= Runner.DeltaTime;
            }

            if (_lifeTime <= 0f)
            {
                IsDischarged = true;
                _light.enabled = false;
            }
        }
    }
}