using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private PlayerData _player_data;

    [SerializeField] private TMP_Text _wins_text;
    [SerializeField] private TMP_Text _money_text;
    [SerializeField] private TMP_Text _damage_text;
    [SerializeField] private TMP_Text _count_of_balls_text;
    [SerializeField] private TMP_Text _getExp_text;
    [SerializeField] private TMP_Text _crit_text;
    [SerializeField] private TMP_Text _crit_chance_text;
    [SerializeField] private TMP_Text _oneShot_chance_text;

    [SerializeField] private TMP_Text _price_of_damage_text;
    [SerializeField] private TMP_Text _price_of_count_of_balls_text;
    [SerializeField] private TMP_Text _price_of_exp_get_text;
    [SerializeField] private TMP_Text _price_of_crit_text;
    [SerializeField] private TMP_Text _price_of_crit_chance_text;
    [SerializeField] private TMP_Text _price_of_oneshot_chance_text;

    [SerializeField] private Button _Play;
    [SerializeField] private Button _buy_damage;
    [SerializeField] private Button _buy_balls;
    [SerializeField] private Button _buy_exp;
    [SerializeField] private Button _buy_crit;
    [SerializeField] private Button _buy_crit_chance;
    [SerializeField] private Button _buy_oneShot_chance;

    [SerializeField] private AudioSource _audioSource;
    private AudioClip _button_click_sound;
    private void Start()
    {
        YandexGame.FullscreenShow();

        DataToText();

        _Play.onClick.AddListener(PlayGame);
        _buy_damage.onClick.AddListener(BuyDamage);
        _buy_balls.onClick.AddListener(BuyBalls);
        _buy_exp.onClick.AddListener(BuyExp);
        _buy_crit.onClick.AddListener(BuyCrit);
        _buy_crit_chance.onClick.AddListener(BuyCritChance);
        _buy_oneShot_chance.onClick.AddListener(BuyOneShot);

        UpdateButtonsInteract();
        
        _audioSource = GetComponent<AudioSource>();
        _button_click_sound = _audioSource.clip;
    }

    public void DataToText()
    {
        _wins_text.text = _player_data._wins.ToString();
        _money_text.text = _player_data._money.ToString();
        _damage_text.text = _player_data._damage.ToString();
        _count_of_balls_text.text = _player_data._count_of_balls.ToString();
        _getExp_text.text = _player_data._getXP.ToString();
        _crit_text.text = "+" + _player_data._crit.ToString() + "%";
        _crit_chance_text.text = _player_data._crit_chance.ToString() + "%";
        _oneShot_chance_text.text = _player_data._oneshot_chance.ToString() + "%";

        _price_of_damage_text.text = _player_data._price_of_damage.ToString();
        _price_of_count_of_balls_text.text = _player_data._price_of_count_of_balls.ToString();
        _price_of_exp_get_text.text = _player_data._price_of_exp_get.ToString();
        _price_of_crit_text.text = _player_data._price_of_crit.ToString();
        _price_of_crit_chance_text.text = _player_data._price_of_crit_chance.ToString();
        _price_of_oneshot_chance_text.text = _player_data._price_of_oneshot_chance.ToString();
    }

    private void UpdateButtonsInteract()
    {
        if (_player_data._price_of_damage > _player_data._money) _buy_damage.interactable = false;
        else _buy_damage.interactable = true;

        if (_player_data._price_of_count_of_balls > _player_data._money) _buy_balls.interactable = false;
        else _buy_balls.interactable = true;

        if (_player_data._price_of_exp_get > _player_data._money) _buy_exp.interactable = false;
        else _buy_exp.interactable = true;

        if (_player_data._price_of_crit > _player_data._money) _buy_crit.interactable = false;
        else _buy_crit.interactable = true;

        if (_player_data._price_of_crit_chance > _player_data._money) _buy_crit_chance.interactable = false;
        else _buy_crit_chance.interactable = true;

        if (_player_data._price_of_oneshot_chance > _player_data._money) _buy_oneShot_chance.interactable = false;
        else _buy_oneShot_chance.interactable = true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    private void UpdateAllData()
    {
        YandexGame.SaveProgress();
        _player_data.GetDataFromSave();
        DataToText();
        UpdateButtonsInteract();
    }
    
    public void ButtonClickSound()
    {
        _audioSource.PlayOneShot(_button_click_sound);
    }

    public void BuyDamage()
    {
        if (YandexGame.savesData._money < YandexGame.savesData._price_of_damage) return;
        YandexGame.savesData._money -= YandexGame.savesData._price_of_damage;
        YandexGame.savesData._damage++;
        YandexGame.savesData._price_of_damage = Mathf.RoundToInt(YandexGame.savesData._price_of_damage * 1.5f);
        UpdateAllData();
    }

    public void BuyBalls()
    {
        if (YandexGame.savesData._money < YandexGame.savesData._price_of_count_of_balls) return;
        YandexGame.savesData._money -= YandexGame.savesData._price_of_count_of_balls;
        YandexGame.savesData._count_of_balls++;
        YandexGame.savesData._price_of_count_of_balls =
            Mathf.RoundToInt(YandexGame.savesData._price_of_count_of_balls * 1.5f);
        UpdateAllData();
    }

    public void BuyExp()
    {
        if (YandexGame.savesData._money < YandexGame.savesData._price_of_exp_get) return;
        YandexGame.savesData._money -= YandexGame.savesData._price_of_exp_get;
        YandexGame.savesData._getXP++;
        YandexGame.savesData._price_of_exp_get = Mathf.RoundToInt(YandexGame.savesData._price_of_exp_get * 1.5f);
        UpdateAllData();
    }

    public void BuyCrit()
    {
        if (YandexGame.savesData._money < YandexGame.savesData._price_of_crit) return;
        YandexGame.savesData._money -= YandexGame.savesData._price_of_crit;
        YandexGame.savesData._crit += 10;
        YandexGame.savesData._price_of_crit = Mathf.RoundToInt(YandexGame.savesData._price_of_crit * 1.5f);
        UpdateAllData();
    }

    public void BuyCritChance()
    {
        if (YandexGame.savesData._money < YandexGame.savesData._price_of_crit_chance ||
            YandexGame.savesData._crit_chance >= 100) return;
        YandexGame.savesData._money -= YandexGame.savesData._price_of_crit_chance;
        YandexGame.savesData._crit_chance++;
        YandexGame.savesData._price_of_crit_chance =
            Mathf.RoundToInt(YandexGame.savesData._price_of_crit_chance * 1.5f);
        UpdateAllData();
    }

    public void BuyOneShot()
    {
        if (YandexGame.savesData._money < YandexGame.savesData._price_of_oneshot_chance ||
            YandexGame.savesData._oneshot_chance >= 100) return;
        YandexGame.savesData._money -= YandexGame.savesData._price_of_oneshot_chance;
        YandexGame.savesData._oneshot_chance++;
        YandexGame.savesData._price_of_oneshot_chance =
            Mathf.RoundToInt(YandexGame.savesData._price_of_oneshot_chance * 1.5f);
        UpdateAllData();
    }
}