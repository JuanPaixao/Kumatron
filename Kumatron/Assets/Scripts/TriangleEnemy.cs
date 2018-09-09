using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _enemySpeed;
    private float _timeCounter;
    private Rigidbody2D _rb;
    [SerializeField]
    private Transform _playerDistance;
    private float _shootTime = 0.5f;
    private float _nextFire;
    private EnemyElectrified _enemyElectrified;
    void Start()
    {
        _timeCounter = 0;
        _nextFire = 0;
        _rb = GetComponent<Rigidbody2D>();
        _enemyElectrified = GetComponent<EnemyElectrified>();
        _playerDistance = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        if (_playerDistance != null)
        {
            if (_enemyElectrified.electrified == false)
            {
                _timeCounter += Time.deltaTime;
                float x = Mathf.Sin(_timeCounter) / 6;
                float y = Mathf.Cos(_timeCounter) / 6;
                _rb.MovePosition(this._rb.position + (new Vector2(x, y) * _enemySpeed) * Time.deltaTime);
            }

            if (_nextFire < Time.time && Vector2.Distance(this.transform.position, _playerDistance.position) < 5.8f)
            {
                Instantiate(_laser, this.transform.position, Quaternion.identity);
                _nextFire = Time.time + _shootTime;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tilemap"))
        {
            this.gameObject.transform.position = this.gameObject.transform.position;
        }
    }
}