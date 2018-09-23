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
    private Vector2 _targetPos;
    private Animator _animator;
    private EnemyElectrified _enemyElectrified;
    [SerializeField]
    private bool _electrified;
    void Start()
    {

        this.gameObject.name = "SquareEnemy";
        _enemyElectrified = GetComponent<EnemyElectrified>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        _electrified = _enemyElectrified.electrified;
        if (_player != null && _electrified == false)
        {
            Movement();
        }
        if (enemyDashing == false)
        {
            _rb.velocity = Vector2.zero;
            SetIdle();
            StartCoroutine(DashTimeOn());
        }
    }
    private void Movement()
    {
        if (_electrified == false)
        {
            if (Vector2.Distance(this.transform.position, _player.transform.position) < 7.5f && Vector2.Distance(this.transform.position,
             _player.transform.position) > 7.0f)
            {
                _rb.MovePosition(Vector2.MoveTowards(this.transform.position, _player.transform.position, _speed * Time.deltaTime));
            }
            else if (Vector2.Distance(this.transform.position, _player.transform.position) < 7.0f && enemyDashing == false)
            {
                _rb.MovePosition(Vector2.MoveTowards(this.transform.position, _player.transform.position, _speed * Time.deltaTime));
            }
            else if (Vector2.Distance(this.transform.position, _player.transform.position) < 7.0f && enemyDashing == true)
            {
                if (_player.transform.position.x > this.transform.position.x)
                {
                    SetDashRight();
                    _rb.AddForce(Vector2.right * _dashSpeed * Time.deltaTime);
                    StartCoroutine(DashTimeOff());
                }
                else if (_player.transform.position.x < this.transform.position.x)
                {
                    SetDashLeft();
                    _rb.AddForce(Vector2.left * _dashSpeed * Time.deltaTime);
                    StartCoroutine(DashTimeOff());
                }
            }
        }
    }
    private IEnumerator DashTimeOff()
    {
        yield return new WaitForSeconds(0.5f);
        enemyDashing = false;
        SetIdle();
    }
    private IEnumerator DashTimeOn()
    {
        yield return new WaitForSeconds(2.5f);
        enemyDashing = true;
    }
    private void SetIdle()
    {
        _animator.SetBool("isIdle", true);
        _animator.SetBool("isDashingRight", false);
        _animator.SetBool("isDashingLeft", false);
    }
    private void SetDashRight()
    {
        _animator.SetBool("isIdle", false);
        _animator.SetBool("isDashingRight", true);
        _animator.SetBool("isDashingLeft", false);
    }
    private void SetDashLeft()
    {
        _animator.SetBool("isIdle", false);
        _animator.SetBool("isDashingRight", false);
        _animator.SetBool("isDashingLeft", true);
    }
}


