using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _speed;
    private float _lastPlayerPosX;
    void Start()
    {
        if (_speed < 1)
        {
            _speed = 1;
        }
        if (_player != null)
        {
            _lastPlayerPosX = _player.position.x;
        }
    }
    void Update()
    {
        if (_player != null)
        {
            Parallax();
        }
    }
    private void Parallax()
    {

        transform.Translate((_player.position.x - _lastPlayerPosX) / _speed, 0, 0);
        _lastPlayerPosX = _player.position.x;
    }
}
