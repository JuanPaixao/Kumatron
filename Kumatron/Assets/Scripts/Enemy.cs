using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private int _enemyHP;
    private Player _player;
    [SerializeField]
    private GameObject _enemyExplosion;
    public Rigidbody2D rb;
    [SerializeField]
    public Transform LeftDetection;
    [SerializeField]
    public Transform RightDetection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (_enemyHP <= 0)
        {
            Instantiate(_enemyExplosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_player != null)
        {
            if (other.gameObject.CompareTag("Player") && _player.isDashing == true)
            {
                _enemyHP -= 2;
            }
            else if (other.gameObject.CompareTag("Egg"))
            {
                _enemyHP--;
            }
        }
    }
}
