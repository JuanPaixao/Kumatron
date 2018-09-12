using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemy : MonoBehaviour
{

    private Transform _player;
    public bool enemyDashing = false;
    [SerializeField]
    private float _dashSpeed, _speed;
    private Rigidbody2D _rb;
    private float _dashCooldown = 1.0f;
    private float _nextDash;
    private Vector2 _targetPos;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        _nextDash = 0;
    }

    void Update()
    {
        if (_player != null)
        {
            Movement();
        }
    }
    private void Movement()
    {
        if (Vector2.Distance(this.transform.position, _player.transform.position) < 7.5f && Vector2.Distance(this.transform.position,
         _player.transform.position) > 3.5f)
        {
            _rb.MovePosition(Vector2.MoveTowards(this.transform.position, _player.transform.position, _speed * Time.deltaTime));

        }
        else if (Vector2.Distance(this.transform.position, _player.transform.position) < 3.5f)
        {
            _rb.MovePosition(Vector2.MoveTowards(this.transform.position, _player.transform.position, _dashSpeed * Time.deltaTime));

        }
    }
}


