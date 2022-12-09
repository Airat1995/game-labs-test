using System.Interface;
using Data;

namespace System
{
    public class WeaponSystem : IUpdatableSystem
    {
        private readonly float _dealDamage;
        private readonly float _weaponInitialCooldown; 
            
        private float _cooldown;
        private float _reducedCooldown;

        private IDamagable _target;

        public WeaponSystem(float dealDamage, float cooldown)
        {
            _dealDamage = dealDamage;
            _weaponInitialCooldown = cooldown;
            _cooldown = 0;
        }

        public void SetTarget(IDamagable damagable)
        {
            _target = damagable;
        }

        public void AddCooldownReducePowerUp(CooldownReducePowerUp cooldownReducePowerUp)
        {
            _reducedCooldown = _weaponInitialCooldown * cooldownReducePowerUp.Value * 0.01f;
        }

        public void Update(float deltaTime)
        {
            _cooldown -= deltaTime;
            if(_cooldown > 0)
                return;
            //Can't deal damage to nothing also
            //Weapon didn't shoot so no need to put on cooldown again 
            if(_target == null)
                return;

            _cooldown = _weaponInitialCooldown - _reducedCooldown;
            _target.DealDamage(_dealDamage);
        }
    }
}