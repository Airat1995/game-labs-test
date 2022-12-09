using UnityEngine;
using UnityEngine.UI;

namespace UI.WinnerWindow
{
    public class WinnerWindow : MonoBehaviour
    {
        [SerializeField]
        private Text _winnerText;
        public Text WinnerText => _winnerText;
    }
}