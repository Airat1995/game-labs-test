using UnityEngine;
using UnityEngine.UI;

namespace UI.Widget
{
    public class SpaceshipConfigurationView : MonoBehaviour
    {
        [SerializeField]
        private Transform _powerUpTransform;
        public Transform PowerUpSpawnTransform => _powerUpTransform;

        [SerializeField]
        private Transform _weaponTransform;
        public Transform WeaponToggleSpawnParent => _weaponTransform;
        
        [SerializeField]
        private Transform _spaceShipsTransform;
        public Transform SpaceShipsTransform => _spaceShipsTransform;

        [SerializeField]
        private ToggleGroup _spaceShipGroup;
        public ToggleGroup SpaceShipGroup => _spaceShipGroup;
    }
}