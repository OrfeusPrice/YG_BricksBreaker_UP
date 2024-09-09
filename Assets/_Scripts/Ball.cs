using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Action onBallRemove;
    [SerializeField] private AudioSource _audioSource;
    private AudioClip _hit_sound;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _hit_sound = _audioSource.clip;
    }

    private void OnCollisionEnter(Collision other)
    {
        _audioSource.PlayOneShot(_hit_sound);
        if (other.gameObject.tag == "RemoveBall")
        {
            BallTouchRemove();
        }

        if (other.gameObject.tag == "Brick")
        {
            other.gameObject?.GetComponent<Brick>().TakeDamage(Attributes._damage);
        }
    }

    private void BallTouchRemove()
    {
        onBallRemove?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        RoundsHandler.onGameOver += BallSetActiveFalse;
        UIManager.onClickReturnButton += BallTouchRemove;
    }

    private void OnDisable()
    {
        RoundsHandler.onGameOver -= BallSetActiveFalse;
        UIManager.onClickReturnButton -= BallTouchRemove;
    }

    public void BallSetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }
}