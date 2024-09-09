using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BallMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _direction;

    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private GameObject _hit;
    Vector3 v;

    private void Start()
    {
        _direction = BallPusher._direction;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        SpawnParticle(other);

        var _normal = other.contacts[0].normal;

        _direction = Vector3.Reflect(_direction, _normal);
        transform.position = Vector2.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);
    }

    private void SpawnParticle(Collision contact)
    {
        GameObject hit = Instantiate(_hit, new Vector3(transform.position.x, transform.position.y, -3),
            Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, Vector2.MoveTowards(transform.position, _direction, _speed));
    }
}