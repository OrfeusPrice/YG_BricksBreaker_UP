using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Random = System.Random;

public class Attributes : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private UIManager _uiManager;

    [SerializeField] private Button _add_damage_button;
    [SerializeField] private Button _add_balls_button;
    
    [SerializeField] private AudioSource _audioSource;
    private AudioClip _present_sound;

    public static int _count_of_balls;
    public static int _money;
    public static int _damage;
    public static int _curXP;
    public static int _getXP;
    public static int _crit;
    public static int _crit_chance;
    public static int _oneShot_chance;

    private int _present = 0;

    private int _nextXP;
    private Slider _XP_bar;
    private TMP_Text _damage_text;
    private TMP_Text _present_text;

    private void Start()
    {
        _nextXP = 5 + YandexGame.savesData._wins;

        _playerData = this.GetComponent<PlayerData>();
        _count_of_balls = _playerData._count_of_balls;
        _damage = _playerData._damage;
        _curXP = 0;
        _getXP = _playerData._getXP;
        _crit = _playerData._crit;
        _crit_chance = _playerData._crit_chance;
        _oneShot_chance = _playerData._oneshot_chance;

        _XP_bar = _uiManager._XP_bar;
        _XP_bar.minValue = 0;
        _XP_bar.maxValue = _nextXP;
        _XP_bar.value = _curXP;

        _damage_text = _uiManager._damage_text;
        _damage_text.text = _damage.ToString();
        _present_text = _uiManager._present_text;
        _money = 0;
        _present_text.text = _present.ToString();

        _add_damage_button.onClick.AddListener(AddDamage);
        _add_balls_button.onClick.AddListener(AddBalls);
        
        _add_damage_button.interactable = false;
        _add_balls_button.interactable = false;
        _uiManager._can_click_Particle_PtoDamage.SetActive(false);
        _uiManager._can_click_Particle_PtoBalls.SetActive(false);
        
        _audioSource = GetComponent<AudioSource>();
        _present_sound = _audioSource.clip;
    }

    private void OnEnable()
    {
        Brick.onDestroyBrick += ChangeXPAndMoney;
    }

    private void OnDisable()
    {
        Brick.onDestroyBrick -= ChangeXPAndMoney;
    }

    private void ChangeXPAndMoney()
    {
        _curXP += _getXP;
        _XP_bar.value = _curXP;
        _money += _playerData._wins + 1;
        _playerData._money += _playerData._wins + 1;
        

        if (_curXP >= _nextXP)
        {
            _present++;
            _audioSource.PlayOneShot(_present_sound);
            _uiManager._getPresent_Particle.SetActive(true);
            _nextXP *= 2;
            _XP_bar.maxValue = _nextXP;
            _curXP = 0;
            _XP_bar.value = _curXP;
            _present_text.text = _present.ToString();
        }
        
        if (_present > 0)
        {
            _add_damage_button.interactable = true;
            _add_balls_button.interactable = true;
            _uiManager._can_click_Particle_PtoDamage.SetActive(true);
            _uiManager._can_click_Particle_PtoBalls.SetActive(true);
        }
    }

    public void AddDamage()
    {
        if (_present > 0)
        {
            _damage += (1 + Mathf.RoundToInt(YandexGame.savesData._wins / 5))*2;
            _damage_text.text = _damage.ToString();
            _present--;
            _present_text.text = _present.ToString();
        }
        if (_present <= 0)
        {
            _add_damage_button.interactable = false;
            _add_balls_button.interactable = false;
            _uiManager._can_click_Particle_PtoDamage.SetActive(false);
            _uiManager._can_click_Particle_PtoBalls.SetActive(false);
        }
    }

    public void AddBalls()
    {
        if (_present > 0)
        {
            _count_of_balls += 1 + Mathf.RoundToInt(YandexGame.savesData._wins / 5);
            BallPusher._cur_count_of_ball += 1 + Mathf.RoundToInt(YandexGame.savesData._wins / 5);
            _present--;
            _present_text.text = _present.ToString();
        }
        if (_present <= 0)
        {
            _add_damage_button.interactable = false;
            _add_balls_button.interactable = false;
            _uiManager._can_click_Particle_PtoDamage.SetActive(false);
            _uiManager._can_click_Particle_PtoBalls.SetActive(false);
        }
    }
}