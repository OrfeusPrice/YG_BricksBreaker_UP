using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnBricks : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bricks;
    [SerializeField] private List<Vector3> _spawners;
    [SerializeField] private List<Material> _materials;
    private int _material_index;
    private Random rand;

    public static int _count_of_alive_bricks = 0;
    public static Action onGetAliveBricks;

    private void Start()
    {
        _material_index = 0;
        rand = new Random();

        RoundsHandler.onRoundOver?.Invoke();
        RoundsHandler.onRoundOver?.Invoke();
        RoundsHandler.onRoundOver?.Invoke();
    }

    private void OnEnable()
    {
        RoundsHandler.onRoundOver += SpawnBrick;
    }

    private void OnDisable()
    {
        RoundsHandler.onRoundOver -= SpawnBrick;
    }

    private void SpawnBrick()
    {
        _count_of_alive_bricks = 0;
        onGetAliveBricks?.Invoke();

        foreach (Vector3 spawn in _spawners)
        {
            if (rand.Next(0, 100) > 30)
            {
                GameObject _spawned_brick = _bricks[rand.Next(0, _bricks.Count)];
                _spawned_brick.GetComponent<Renderer>().material = _materials[_material_index % 7];
                Instantiate(_spawned_brick, spawn, _spawned_brick.transform.rotation);
            }
        }

        _material_index++;

        if (_count_of_alive_bricks == 0)
        {
            RoundsHandler.onRoundOver?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (Vector3 spawn in _spawners)
        {
            Gizmos.DrawWireCube(spawn, Vector3.one);
        }
    }
}