using System.Collections.Generic;
using Data;
using DefaultNamespace;
using UI.Widget;

namespace UI
{
    public class MainWindowPresenter
    {
        private readonly MainWindow _view;

        private readonly IStartable _startGame;

        private readonly SpaceshipConfigurationPresenter _firstPlayerConfigurationPresenter;
        private readonly SpaceshipConfigurationPresenter _secondPlayerConfigurationPresenter;

        public MainWindowPresenter(MainWindow view, LabeledToggle toggle, List<SpaceShipData> spaceShips, List<WeaponData> weapons, List<PowerUpData> powerUps, IStartable startGame)
        {
            _view = view;
            _startGame = startGame;

            _firstPlayerConfigurationPresenter = new SpaceshipConfigurationPresenter(_view.FirstPlayerConfigView, toggle, spaceShips, powerUps, weapons);
            _secondPlayerConfigurationPresenter = new SpaceshipConfigurationPresenter(_view.SecondPlayerConfig, toggle, spaceShips, powerUps, weapons);

            _view.StartButton.onClick.AddListener(StartFight);
        }

        private void StartFight()
        {
            _view.gameObject.SetActive(false);
            _startGame.StartGame(true);
        }

        public SpaceShipData GetFPSpaceShip()
        {
            return _firstPlayerConfigurationPresenter.GetSpaceShip();
        }

        public List<WeaponData> GetFPWeapons()
        {
            return _firstPlayerConfigurationPresenter.GetWeapons();
        }

        public List<PowerUpData> GetFPPowerUps()
        {
            return _firstPlayerConfigurationPresenter.GetPowerUps();
        }
        
        public SpaceShipData GetSPSpaceShip()
        {
            return _secondPlayerConfigurationPresenter.GetSpaceShip();
        }

        public List<WeaponData> GetSPWeapons()
        {
            return _secondPlayerConfigurationPresenter.GetWeapons();
        }

        public List<PowerUpData> GetSPPowerUps()
        {
            return _secondPlayerConfigurationPresenter.GetPowerUps();
        }
    }
}