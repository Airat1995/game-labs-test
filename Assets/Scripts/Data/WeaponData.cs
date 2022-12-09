using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Create WeaponData", order = 0)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField]
        private string _visibleName;
        public string VisibleName => _visibleName;
        
        [SerializeField]
        private float _dealtDamage;
        public float DealtDamage => _dealtDamage;

        [SerializeField]
        private float _cooldownTime;
        public float CooldownTime => _cooldownTime;
    }
}