using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHP;
    [SerializeField] private Player _player;
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
        _player = FindObjectOfType<Player>();
    }
    void Update()
    {
        if (enemyHP <= 0)
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
                enemyHP -= 3;
            }
            else if (other.gameObject.CompareTag("Egg"))
            {
                enemyHP -= 2;
            }
        }
    }
}
