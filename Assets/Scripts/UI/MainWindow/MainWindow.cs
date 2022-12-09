using UI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainWindow : MonoBehaviour
    {
        [SerializeField]
        private SpaceshipConfigurationView _firstPlayerConfig;
        public SpaceshipConfigurationView FirstPlayerConfigView => _firstPlayerConfig;

        [SerializeField]
        private SpaceshipConfigurationView _secondPlayerConfig;
        public SpaceshipConfigurationView SecondPlayerConfig => _secondPlayerConfig;

        [SerializeField]
        private Button _startButton;
        public Button StartButton => _startButton;
    }
}