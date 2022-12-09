using UnityEngine;

namespace Data
{
    public class PowerUpData : ScriptableObject
    {
        [SerializeField]
        private string _name;
        public string Name => _name;
        
        [SerializeField]
        private float _value;
        public float Value => _value;
    }
}