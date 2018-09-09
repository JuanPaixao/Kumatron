using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _enemySpeed;
    public float stoppingDistance;
    [SerializeField]
    private float _retreatingDistance;
    [SerializeField]
    private Transform _player;
    void Start()
    {
        this.gameObject.name = "Enemy";
    }

    void Update()
    {
        if (Vector2.Distance(this.transform.position, _player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _player.position, _enemySpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(this.transform.position, _player.position) < stoppingDistance && Vector2.Distance(this.transform.position, _player.position) > _retreatingDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(this.transform.position, _player.position) < _retreatingDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _player.position, -_enemySpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tilemap")){
            this.gameObject.transform.position = this.gameObject.transform.position;
        }
    }
}