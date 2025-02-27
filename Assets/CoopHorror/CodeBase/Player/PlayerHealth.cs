using Fusion;
using System;
using UnityEngine;

namespace CoopHorror.CodeBase
{
    public class PlayerHealth : NetworkBehaviour
    {
        public event Action Died;

        public bool IsAlive => _current > 0f;

        [SerializeField] private float _max;
        [SerializeField] private float _current;

        private void Start() =>
            _current = _max;

        public void TakeDamage(float value)
        {
            _current = Mathf.Max(_current - value, 0f);

            if (!IsAlive)
                Died?.Invoke();
        }

        public void AddHealth(float value)
        {
            _current += value;

            if (_current > _max)
                _current = _max;
        }
    }
}
