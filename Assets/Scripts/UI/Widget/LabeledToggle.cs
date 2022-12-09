using UnityEngine;
using UnityEngine.UI;

namespace UI.Widget
{
    public class LabeledToggle : MonoBehaviour
    {
        [SerializeField]
        private Toggle _toggle;
        public Toggle Toggle => _toggle;

        [SerializeField]
        private Text _label;
        public Text Label => _label;
        
    }
}