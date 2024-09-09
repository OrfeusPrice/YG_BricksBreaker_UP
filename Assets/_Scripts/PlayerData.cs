using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerData : MonoBehaviour
{
    public int _wins;
    public int _money;
    public int _damage;
    public int _count_of_balls;
    public int _getXP;
    public int _crit;
    public int _crit_chance;
    public int _oneshot_chance;

    public int _price_of_damage;
    public int _price_of_count_of_balls;
    public int _price_of_exp_get;
    public int _price_of_crit;
    public int _price_of_crit_chance;
    public int _price_of_oneshot_chance;

    private void Awake()
    {
        GetDataFromSave();
    }

    public void GetDataFromSave()
    {
        _wins = YandexGame.savesData._wins;
        _money = YandexGame.savesData._money;
        _damage = YandexGame.savesData._damage;
        _count_of_balls = YandexGame.savesData._count_of_balls;
        _getXP = YandexGame.savesData._getXP;
        _crit = YandexGame.savesData._crit;
        _crit_chance = YandexGame.savesData._crit_chance;
        _oneshot_chance = YandexGame.savesData._oneshot_chance;

        _price_of_damage = YandexGame.savesData._price_of_damage;
        _price_of_count_of_balls = YandexGame.savesData._price_of_count_of_balls;
        _price_of_exp_get = YandexGame.savesData._price_of_exp_get;
        _price_of_crit = YandexGame.savesData._price_of_crit;
        _price_of_crit_chance = YandexGame.savesData._price_of_crit_chance;
        _price_of_oneshot_chance = YandexGame.savesData._price_of_oneshot_chance;
    }

    private void OnEnable()
    {
        RoundsHandler.onGameOver += SetData;
    }

    private void OnDisable()
    {
        RoundsHandler.onGameOver -= SetData;
    }


    private void SetData()
    {
        YandexGame.savesData._money = _money;
        YandexGame.SaveProgress();
    }
}