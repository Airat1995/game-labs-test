using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SpaceShipData", menuName = "Create SpaceShip Data", order = 0)]
    public class SpaceShipData : ScriptableObject
    {
        [SerializeField]
        private string _visibleName;
        public string VisibleName => _visibleName;
        
        [SerializeField]
        private float _health;
        public float Health => _health;

        [SerializeField]
        private float _shield;
        public float Shield => _shield;

        [SerializeField]
        private float _shieldRestorePerSecond;
        public float ShieldRestorePerSecond => _shieldRestorePerSecond;

        [SerializeField]
        private uint _weaponsCount;
        public uint WeaponsCount => _weaponsCount;

        [SerializeField]
        private uint _powerUpsCount;
        public uint PowerUpsCount => _powerUpsCount;
    }
}