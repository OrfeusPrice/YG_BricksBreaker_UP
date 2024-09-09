using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class BallPusher : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _ball;
    [SerializeField] private UIManager _uiManager;
    public static int _cur_count_of_ball;
    [SerializeField] private TMP_Text _count_text;
    private bool _can_spawn;
    private bool _isMouseNotOnUI;
    private Vector3 _mouse_pos;
    private RaycastHit _ray_hit;
    [SerializeField] private Collider _hit_col;


    public static Action onAllBallRemoved;
    public static bool _is_game_not_over;
    public static bool _is_no_returned;
    public static Vector3 _direction;

    private void OnEnable()
    {
        Ball.onBallRemove += ChangeCurCountOfBall;
    }

    private void OnDisable()
    {
        Ball.onBallRemove -= ChangeCurCountOfBall;
    }

    void Start()
    {
        Time.timeScale = 1;
        _target.SetActive(false);
        _can_spawn = false;
        Invoke("CanSpawn", 0.5f);
        _is_no_returned = true;
        _is_game_not_over = true;
        _cur_count_of_ball = Attributes._count_of_balls;
        _count_text.text = _cur_count_of_ball.ToString() + "x";
        _uiManager._return_balls_button.interactable = false;
        _uiManager._can_click_Particle_Return.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _ray_hit))
            {
                if (_ray_hit.collider == _hit_col)
                    _isMouseNotOnUI = false;
                else _isMouseNotOnUI = true;
            }
            else
                _isMouseNotOnUI = true;
        }


        if (Input.GetKey(KeyCode.Mouse0) && _can_spawn && _is_game_not_over && _is_no_returned && _isMouseNotOnUI)
        {
            _mouse_pos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

            _target.SetActive(true);
            Cursor.visible = false;
            if (_mouse_pos.y <= -8)
                _mouse_pos = new Vector3(_mouse_pos.x, -8, _mouse_pos.y);
            transform.LookAt(_mouse_pos);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && _can_spawn && _is_game_not_over && _is_no_returned &&
                 _isMouseNotOnUI)
        {
            _can_spawn = false;
            _target.SetActive(false);
            _direction = _mouse_pos;
            Cursor.visible = true;
            StartCoroutine(Spawn());
        }
    }

    void ChangeCurCountOfBall()
    {
        if (_cur_count_of_ball < Attributes._count_of_balls)
        {
            _cur_count_of_ball++;
            _count_text.text = _cur_count_of_ball.ToString() + "x";
        }

        if (_cur_count_of_ball >= Attributes._count_of_balls)
        {
            onAllBallRemoved?.Invoke();
            _can_spawn = true;
            _uiManager._return_balls_button.interactable = false;
            _uiManager._can_click_Particle_Return.SetActive(false);
        }
    }

    void SpawnBall()
    {
        Instantiate(_ball);
    }

    void CanSpawn()
    {
        _can_spawn = true;
    }

    IEnumerator Spawn()
    {
        int temp = Attributes._count_of_balls;
        for (int i = 0; i < temp; i++)
        {
            SpawnBall();
            _cur_count_of_ball--;
            _count_text.text = _cur_count_of_ball.ToString() + "x";
            yield return new WaitForSeconds(0.1f);
        }

        _uiManager._return_balls_button.interactable = true;
        _uiManager._can_click_Particle_Return.SetActive(true);
        StopCoroutine(Spawn());
    }
}