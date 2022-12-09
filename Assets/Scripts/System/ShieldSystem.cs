using System.Interface;
using Data;
using UnityEngine;

namespace System
{
    public class ShieldSystem : IUpdatableSystem
    {
        private float _restorePerSecond;

        private float _maxShield;
        private float _currentShield;

        public ShieldSystem(float shield, float restorePerSecond)
        {
            _maxShield = shield;
            _currentShield = shield;
            _restorePerSecond = restorePerSecond;
        }

        public void AddShieldPowerUp(AddShieldPowerUp shieldPowerUp)
        {
            _maxShield += shieldPowerUp.Value;
            _currentShield += shieldPowerUp.Value;
        }

        public void AddShieldRestorePowerUp(ShieldRestorePowerUp shieldRestorePowerUp)
        {
            _restorePerSecond += _restorePerSecond * shieldRestorePowerUp.Value * 0.01f;
        }

        public void Update(float deltaTime)
        {
            _currentShield = Mathf.Min(_currentShield + _restorePerSecond * deltaTime, _maxShield);
        }

        public float DealDamage(float damage)
        {
            if (_currentShield <= 0)
                return damage;

            float exceedDamage = damage - _currentShield;
            _currentShield = Mathf.Max(_currentShield - damage, 0);
            if (exceedDamage < 0)
                exceedDamage = 0;

            return exceedDamage;
        }
    }
}