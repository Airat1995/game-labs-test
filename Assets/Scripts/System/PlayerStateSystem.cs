using System.Collections.Generic;
using System.Interface;
using Data;
using UnityEngine;
using View;

namespace System
{
    public class PlayerStateSystem : IUpdatableSystem, IDamagable
    {
        private readonly List<PowerUpData> _powerUps;

        private readonly Spaceship _spaceship;

        private HealthSystem _healthSystem;
        private ShieldSystem _shieldSystem;
        private List<WeaponSystem> _weaponSystems;

        private bool _dead;
        public bool IsDead => _dead;

        public PlayerStateSystem(SpaceShipData spaceShip, List<WeaponData> weapons, List<PowerUpData> powerUps, Spaceship spaceship)
        {
            _dead = false;
            
            _powerUps = powerUps;
            _spaceship = spaceship;

            _healthSystem = new HealthSystem(spaceShip.Health);
            _shieldSystem = new ShieldSystem(spaceShip.Shield, spaceShip.ShieldRestorePerSecond);
            _weaponSystems = new List<WeaponSystem>();
            
            if(weapons.Count > spaceShip.WeaponsCount)
                Debug.LogException(new Exception("Came more weapons than spaceship can handle"));
            
            if(powerUps.Count > spaceShip.PowerUpsCount)
                Debug.LogException(new Exception("Came more modules than spaceship can handle"));


            foreach (var weaponData in weapons)
            {
                WeaponSystem weaponSystem = new WeaponSystem(weaponData.DealtDamage, weaponData.CooldownTime);
                _weaponSystems.Add(weaponSystem);
            }
            AddPowerUps();
        }

        public void SetDamageTarget(IDamagable damagable)
        {
            for (int weaponIndex = 0; weaponIndex < _weaponSystems.Count; weaponIndex++)
            {
                _weaponSystems[weaponIndex].SetTarget(damagable);
            }
        }

        private void AddPowerUps()
        {
            foreach (var powerUp in _powerUps)
            {
                switch (powerUp)
                {
                    case AddHealthPowerUp addHealthPowerUp:
                        _healthSystem.AddHealthPowerUp(addHealthPowerUp);
                        break;
                    case AddShieldPowerUp addShieldPowerUp:
                        _shieldSystem.AddShieldPowerUp(addShieldPowerUp);
                        break;
                    case ShieldRestorePowerUp shieldRestorePowerUp:
                        _shieldSystem.AddShieldRestorePowerUp(shieldRestorePowerUp);
                        break;
                    case CooldownReducePowerUp cooldownReducePowerUp:
                        for (int weaponIndex = 0; weaponIndex < _weaponSystems.Count; weaponIndex++)
                        {
                            _weaponSystems[weaponIndex].AddCooldownReducePowerUp(cooldownReducePowerUp);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(powerUp));
                }
            }
        }

        public void Update(float deltaTime)
        {
            _shieldSystem.Update(deltaTime);
            foreach (var weapon in _weaponSystems)
            {
                weapon.Update(deltaTime);
            }
        }

        public void DealDamage(float damage)
        {
            float leftDamage = _shieldSystem.DealDamage(damage);
            _dead = _healthSystem.DealDamage(leftDamage);
            if(_dead)
                _spaceship.gameObject.SetActive(false);
        }
    }
}