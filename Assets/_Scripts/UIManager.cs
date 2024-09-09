using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _MainMenu_button;
    [SerializeField] private Button _EndGameMainMenu_button;
    public Button _return_balls_button;
    [SerializeField] private Button _bonus_ADS_button;

    [SerializeField] private GameObject _death_icon;
    [SerializeField] private GameObject _win_icon;
    [SerializeField] private GameObject _end_game_canvas;
    [SerializeField] private GameObject _borders_end_game;
    [SerializeField] private GameObject _borders_MAIN;
    
    [SerializeField] private AudioSource _audioSource;
    private AudioClip _button_click_sound;

    public GameObject _can_click_Particle_Return;
    public GameObject _can_click_Particle_PtoDamage;
    public GameObject _can_click_Particle_PtoBalls;
    public GameObject _getPresent_Particle;

    public static Action onClickReturnButton;

    public TMP_Text _damage_text;
    public TMP_Text _present_text;
    public TMP_Text _money_text_EndGame;
    [SerializeField] private TMP_Text _bonus_ADS_text;

    public Slider _XP_bar;
    public Slider _RoundPrgs_bar;

    private void Start()
    {
        _MainMenu_button.onClick.AddListener(ExitToMainMenu);
        _EndGameMainMenu_button.onClick.AddListener(ExitToMainMenu);
        _bonus_ADS_button.onClick.AddListener(bonusADS);
        _return_balls_button.onClick.AddListener(KillAllBalls);

        _bonus_ADS_text.text = ((YandexGame.savesData._wins + 1) * 150).ToString();

        _end_game_canvas.SetActive(false);
        _borders_end_game.SetActive(false);
        _borders_MAIN.SetActive(true);
        
        _audioSource = GetComponent<AudioSource>();
        _button_click_sound = _audioSource.clip;
    }

    public void ButtonClickSound()
    {
        _audioSource.PlayOneShot(_button_click_sound);
    }

    public void ShowDeathIcon()
    {
        _death_icon.SetActive(true);
        _win_icon.SetActive(false);
    }

    public void ShowWinIcon()
    {
        _death_icon.SetActive(false);
        _win_icon.SetActive(true);
    }

    public void ShowEndGame()
    {
        _end_game_canvas.SetActive(true);
        if (_borders_end_game != null)
            _borders_end_game.SetActive(true);
        _borders_MAIN.SetActive(false);
        _money_text_EndGame.text = Attributes._money.ToString();
    }

    public void bonusADS()
    {
        YandexGame.RewVideoShow(1);

        _bonus_ADS_button.interactable = false;
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += AddRewardMoney;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= AddRewardMoney;
    }

    public void AddRewardMoney(int id)
    {
        if (id == 1)
            YandexGame.savesData._money += (YandexGame.savesData._wins + 1) * 150;
    }

    public void KillAllBalls()
    {
        BallPusher._is_no_returned = false;
        onClickReturnButton?.Invoke();
        BallPusher._is_no_returned = true;
    }

    private void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}