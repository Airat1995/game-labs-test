using System.Collections.Generic;
using Data;
using UnityEngine;

namespace UI.Widget
{
    public class SpaceshipConfigurationPresenter
    {
        private readonly SpaceshipConfigurationView _view;

        private readonly List<SpaceShipData> _allSpaceShips;

        private uint _powerUpsMaxCount;
        private uint _weaponsMaxCount;

        private readonly List<WeaponData> _weapons;
        private readonly List<PowerUpData> _powerUps;
        private SpaceShipData _spaceShipData;
        
        private readonly Dictionary<WeaponData, LabeledToggle> _weaponToggles;
        private readonly Dictionary<PowerUpData, LabeledToggle> _powerUpToggles;

        public SpaceshipConfigurationPresenter(SpaceshipConfigurationView view, LabeledToggle toggle,  List<SpaceShipData> allSpaceShips, List<PowerUpData> allPowerUps, List<WeaponData> allWeapons)
        {
            _view = view;
            _allSpaceShips = allSpaceShips;
            _weaponsMaxCount = 0;
            _powerUpsMaxCount = 0;
            _weapons = new List<WeaponData>();
            _powerUps = new List<PowerUpData>();

            _weaponToggles = new Dictionary<WeaponData, LabeledToggle>();
            _powerUpToggles = new Dictionary<PowerUpData, LabeledToggle>();

            CreateWeaponsToggle(toggle, allWeapons);
            CreatePowerUpsToggle(toggle, allPowerUps);
            CreateSpaceShipToggle(toggle, allSpaceShips);
        }

        private void CreatePowerUpsToggle(LabeledToggle toggle, List<PowerUpData> allPowerUps)
        {
            for (int powerUpIndex = 0; powerUpIndex < allPowerUps.Count; powerUpIndex++)
            {
                LabeledToggle newPowerUpToggle = Object.Instantiate(toggle, _view.PowerUpSpawnTransform);
                newPowerUpToggle.Toggle.interactable = false;
                newPowerUpToggle.Label.text = allPowerUps[powerUpIndex].Name;
                int index = powerUpIndex;
                newPowerUpToggle.Toggle.onValueChanged.AddListener((active) =>
                {
                    PowerUpToggleChanged(active, allPowerUps[index]);
                });
                _powerUpToggles.Add(allPowerUps[powerUpIndex], newPowerUpToggle);
            }
        }

        private void CreateWeaponsToggle(LabeledToggle toggle, List<WeaponData> allWeapons)
        {
            for (int weaponIndex = 0; weaponIndex < allWeapons.Count; weaponIndex++)
            {
                LabeledToggle newWeaponToggle = Object.Instantiate(toggle, _view.WeaponToggleSpawnParent);
                newWeaponToggle.Toggle.interactable = false;
                newWeaponToggle.Label.text = allWeapons[weaponIndex].VisibleName;
                int index = weaponIndex;
                newWeaponToggle.Toggle.onValueChanged.AddListener((active) =>
                {
                    WeaponToggleChanged(active, allWeapons[index]);
                });
                _weaponToggles.Add(allWeapons[weaponIndex], newWeaponToggle);
            }
        }
        
        private void CreateSpaceShipToggle(LabeledToggle toggle, List<SpaceShipData> allSpaceShips)
        {
            for (int spaceShipIndex = 0; spaceShipIndex < allSpaceShips.Count; spaceShipIndex++)
            {
                LabeledToggle newWeaponToggle = Object.Instantiate(toggle, _view.SpaceShipsTransform);
                newWeaponToggle.Toggle.interactable = true;
                newWeaponToggle.Label.text = allSpaceShips[spaceShipIndex].VisibleName;
                newWeaponToggle.Toggle.group = _view.SpaceShipGroup;
                int index = spaceShipIndex;
                newWeaponToggle.Toggle.onValueChanged.AddListener((active) =>
                {
                    SpaceShipToggleChanged(active, index);
                });
            }
        }

        private void SpaceShipToggleChanged(bool active, int spaceShipIndex)
        {
            if (active)
            {
                if (_spaceShipData == null)
                {
                    foreach (var weaponToggle in _weaponToggles)
                    {
                        weaponToggle.Value.Toggle.interactable = true;
                    }

                    foreach (var powerUpToggle in _powerUpToggles)
                    {
                        powerUpToggle.Value.Toggle.interactable = true;
                    }
                }
                _spaceShipData = _allSpaceShips[spaceShipIndex];
                
                PowerUpCheck();
                _powerUpsMaxCount = _spaceShipData.PowerUpsCount;
                
                WeaponCheck();
                _weaponsMaxCount = _spaceShipData.WeaponsCount;
            }
        }

        private void PowerUpCheck()
        {
            if (_powerUpsMaxCount < _spaceShipData.PowerUpsCount)
            {
                foreach (var powerUpToggle in _powerUpToggles)
                {
                    powerUpToggle.Value.Toggle.interactable = true;
                }
            }
            else if (_powerUpsMaxCount > _spaceShipData.PowerUpsCount)
            {
                uint powerUpCountDifference = _powerUpsMaxCount - _spaceShipData.PowerUpsCount;
                for (int powerUpToggleIndex = _powerUps.Count - 1; powerUpToggleIndex >= 0; powerUpToggleIndex--)
                {
                    _powerUpToggles[_powerUps[powerUpToggleIndex]].Toggle.SetIsOnWithoutNotify(false);
                    _powerUpToggles[_powerUps[powerUpToggleIndex]].Toggle.interactable = false;
                    powerUpCountDifference--;
                    _powerUps.Remove(_powerUps[powerUpToggleIndex]);

                    if (powerUpCountDifference == 0)
                        break;
                }
            }
        }
        
        private void WeaponCheck()
        {
            if (_weaponsMaxCount < _spaceShipData.WeaponsCount)
            {
                foreach (var weaponToggle in _weaponToggles)
                {
                    weaponToggle.Value.Toggle.interactable = true;
                }
            }
            else if (_weaponsMaxCount > _spaceShipData.WeaponsCount)
            {
                uint weaponCountDifference = _weaponsMaxCount - _spaceShipData.WeaponsCount;
                for (int weaponToggleIndex = _weapons.Count - 1; weaponToggleIndex >= 0; weaponToggleIndex--)
                {
                    _weaponToggles[_weapons[weaponToggleIndex]].Toggle.SetIsOnWithoutNotify(false);
                    _weaponToggles[_weapons[weaponToggleIndex]].Toggle.interactable = false;
                    weaponCountDifference--;
                    _weapons.RemoveAt(weaponToggleIndex);

                    if (weaponCountDifference == 0)
                        break;
                }
            }
        }

        private void WeaponToggleChanged(bool activate, WeaponData weapon)
        {
            if (activate)
            {
                _weapons.Add(weapon);
                if (_weapons.Count != _weaponsMaxCount)
                    return;
                
                foreach (var weaponToggle in _weaponToggles)
                {
                    weaponToggle.Value.Toggle.interactable = weaponToggle.Value.Toggle.isOn;
                }
            }
            else
            {
                _weapons.Remove(weapon);
                if (_weapons.Count + 1 != _weaponsMaxCount)
                    return;
                
                foreach (var weaponToggle in _weaponToggles)
                {
                    weaponToggle.Value.Toggle.interactable = true;
                }
            }
        }
        
        private void PowerUpToggleChanged(bool activate, PowerUpData powerUp)
        {
            if (activate)
            {
                _powerUps.Add(powerUp);
                if (_powerUps.Count != _powerUpsMaxCount)
                    return;
                
                foreach (var powerUpToggle in _powerUpToggles)
                {
                    powerUpToggle.Value.Toggle.interactable = powerUpToggle.Value.Toggle.isOn;
                }
            }
            else
            {
                _powerUps.Remove(powerUp);
                if (_powerUps.Count + 1 != _powerUpsMaxCount)
                    return;
                
                foreach (var powerUpToggle in _powerUpToggles)
                {
                    powerUpToggle.Value.Toggle.interactable = true;
                }
            }
        }

        public List<WeaponData> GetWeapons()
        {
            return _weapons;
        }

        public List<PowerUpData> GetPowerUps()
        {
            return _powerUps;
        }

        public SpaceShipData GetSpaceShip()
        {
            return _spaceShipData;
        }
    }
}