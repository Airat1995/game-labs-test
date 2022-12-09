using System;
using System.Collections.Generic;
using Data;
using DefaultNamespace;
using UI;
using UI.Widget;
using UI.WinnerWindow;
using UnityEngine;
using UnityEngine.UI;
using View;

public class Initializer : MonoBehaviour, IStartable
{
    [SerializeField]
    private List<SpaceShipData> _allSpaceShips;

    [SerializeField]
    private List<PowerUpData> _allPowerUps;

    [SerializeField]
    private List<WeaponData> _allWeapons;

    [SerializeField]
    private MainWindow _mainWindow;

    [SerializeField]
    private WinnerWindow _winnerWindow;

    [SerializeField]
    private LabeledToggle _selectionToggle;

    [SerializeField]
    private Spaceship _firstPlayerSpaceship;

    [SerializeField]
    private Spaceship _secondPlayerSpaceship;

    private PlayerStateSystem _playerOneSystem;
    private PlayerStateSystem _playerTwoSystem;

    private MainWindowPresenter _mainWindowPresenter;
    private WinnerWindowPresenter _winnerWindowPresenter;

    private bool _gameStarted;
    
    private void Start()
    {
        _mainWindowPresenter = new MainWindowPresenter(_mainWindow, _selectionToggle, _allSpaceShips, _allWeapons, _allPowerUps, this);
        _winnerWindowPresenter = new WinnerWindowPresenter(_winnerWindow);
    }

    private void Update()
    {
        if(!_gameStarted)
            return;
        _playerOneSystem.Update(Time.deltaTime);
        _playerTwoSystem.Update(Time.deltaTime);
    }

    private void LateUpdate()
    {
        if(!_gameStarted)
            return;
        bool playerOneDead = _playerOneSystem.IsDead;
        bool playerTwoDead = _playerTwoSystem.IsDead;
        if (!playerOneDead && !playerTwoDead)
            return;

        StartGame(false);
        _winnerWindowPresenter.ShowWinner(playerOneDead, playerTwoDead);
    }

    public void StartGame(bool started)
    {
        if (started)
        {
            _playerOneSystem = new PlayerStateSystem(_mainWindowPresenter.GetFPSpaceShip(), _mainWindowPresenter.GetFPWeapons(), _mainWindowPresenter.GetFPPowerUps(), _firstPlayerSpaceship);
            _playerTwoSystem = new PlayerStateSystem(_mainWindowPresenter.GetSPSpaceShip(), _mainWindowPresenter.GetSPWeapons(), _mainWindowPresenter.GetSPPowerUps(), _secondPlayerSpaceship);
            _playerOneSystem.SetDamageTarget(_playerTwoSystem);
            _playerTwoSystem.SetDamageTarget(_playerOneSystem);
        }
        _gameStarted = started;
    }
}