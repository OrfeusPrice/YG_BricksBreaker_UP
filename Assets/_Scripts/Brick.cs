using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using YG;
using Random = System.Random;

public class Brick : MonoBehaviour
{
    public static Action onDestroyBrick;

    [SerializeField] private TMP_Text text;
    [SerializeField] private int _hp;
    [SerializeField] private GameObject _hit_crit;
    [SerializeField] private GameObject _hit_oneShot;
    private Random _rand = new Random();

    private void Start()
    {
        _hp = _rand.Next(1 + YandexGame.savesData._wins * 2, 5 + YandexGame.savesData._wins * 3);
        text.text = _hp.ToString();
    }

    private void OnEnable()
    {
        RoundsHandler.onGameOver += BrickSetActiveFalse;
        SpawnBricks.onGetAliveBricks += IamAlive;
    }

    private void OnDisable()
    {
        RoundsHandler.onGameOver -= BrickSetActiveFalse;
        SpawnBricks.onGetAliveBricks -= IamAlive;
    }

    private void OnDestroy()
    {
        onDestroyBrick?.Invoke();
    }

    public void TakeDamage(int _damage)
    {
        _hp -= _damage;
        if (_rand.Next(0, 100) <= Attributes._crit_chance)
        {
            Instantiate(_hit_crit, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
            _hp -= Mathf.RoundToInt(_damage * Attributes._crit / 100);
        }

        if (_hp <= 0 || _rand.Next(0, 100) <= Attributes._oneShot_chance)
        {
            Instantiate(_hit_oneShot, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
            Destroy(this.gameObject);
        }

        text.text = _hp.ToString();
    }

    public void BrickSetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }

    private void IamAlive()
    {
        SpawnBricks._count_of_alive_bricks++;
    }
}