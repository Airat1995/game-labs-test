using Data;

namespace System
{
    public class HealthSystem
    {
        private float _currentHp;

        public HealthSystem(float health)
        {
            _currentHp = health;
        }

        public void AddHealthPowerUp(AddHealthPowerUp healthPowerUp)
        {
            _currentHp += healthPowerUp.Value;
        }

        public bool DealDamage(float damage)
        {
            _currentHp -= damage;

            return _currentHp <= 0;
        }
    }
}