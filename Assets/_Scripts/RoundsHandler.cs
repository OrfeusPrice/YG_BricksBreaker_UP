using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RoundsHandler : MonoBehaviour
{
    public static Action onRoundOver;
    public static Action onGameOver;
    public int _roundProgress;
    public int _winValue;
    private Slider _RoundPrgs_bar;
    [SerializeField] private UIManager _uiManager;
    private bool _is_win;

    private void Start()
    {
        _roundProgress = 0;
        _winValue = 75 + YandexGame.savesData._wins * 2;
        _is_win = true;

        _RoundPrgs_bar = _uiManager._RoundPrgs_bar;
        _RoundPrgs_bar.minValue = 0;
        _RoundPrgs_bar.maxValue = _winValue;
        _RoundPrgs_bar.value = _roundProgress;
    }

    private void OnEnable()
    {
        BallPusher.onAllBallRemoved += RoundOver;
        Brick.onDestroyBrick += RoundProgressChange;
        BrickMove.onBrickFinished += GameOver;
    }

    private void OnDisable()
    {
        BallPusher.onAllBallRemoved -= RoundOver;
        Brick.onDestroyBrick -= RoundProgressChange;
        BrickMove.onBrickFinished -= GameOver;
    }

    private void RoundProgressChange()
    {
        _roundProgress++;
        _RoundPrgs_bar.value = _roundProgress;

    }

    private void RoundOver()
    {
        onRoundOver?.Invoke();
        
        if (_roundProgress >= _winValue && _is_win)
        {
            _is_win = false;
            GameOver();
            _uiManager.ShowWinIcon();
            YandexGame.savesData._wins++;
            YandexGame.NewLeaderboardScores("Wins", YandexGame.savesData._wins);
            YandexGame.SaveProgress();
        }
    }

    private void GameOver()
    {
        BallPusher._is_game_not_over = false;
        _uiManager.ShowEndGame();
        _uiManager.ShowDeathIcon();
        onGameOver?.Invoke();
    }
}