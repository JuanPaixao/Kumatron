using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleShoot : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private float _speed;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        transform.Translate(Vector2.right * -_speed * Time.deltaTime);
    }
}
