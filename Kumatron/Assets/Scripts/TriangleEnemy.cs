using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : MonoBehaviour
{

    [SerializeField]
    private float _enemySpeed;
    private Rigidbody2D _rb;
    private float _timeCounter;

    private EnemyElectrified _enemyElectrified;
    void Start()
    {

        this.gameObject.name = "TriangleEnemy";
        _rb = GetComponent<Rigidbody2D>();
        _enemyElectrified = GetComponent<EnemyElectrified>();
    }
    void FixedUpdate()
    {
        if (_enemyElectrified.electrified == false)
        {
            _timeCounter += Time.deltaTime;
            float x = Mathf.Sin(_timeCounter) / 6;
            float y = Mathf.Cos(_timeCounter) / 6;
            _rb.MovePosition(this._rb.position + (new Vector2(x, y) * _enemySpeed) * Time.deltaTime);
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