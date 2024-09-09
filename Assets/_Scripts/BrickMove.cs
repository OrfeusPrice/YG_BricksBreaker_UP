using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMove : MonoBehaviour
{
    public static Action onBrickFinished;
    private void OnEnable()
    {
        RoundsHandler.onRoundOver += Move;
    }

    private void OnDisable()
    {
        RoundsHandler.onRoundOver -= Move;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,-10,0), 1f);
        if (transform.position.y < -7.1f)
        {
            onBrickFinished?.Invoke();
        }
    }
}
