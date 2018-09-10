using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleShootSpawn : MonoBehaviour
{

    private float _shootTime = 1.25f;
    private float _nextFire;
    [SerializeField]
    private GameObject _shoot;
    private Transform _player;
    void Start()
    {
        _nextFire = 0;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (_player != null)
        {
            LookPosition();
            if (_nextFire < Time.time && Vector2.Distance(this.transform.position, _player.position) < 5.8f)
            {
                Instantiate(_shoot, transform.position, transform.rotation);
                _nextFire = Time.time + _shootTime;
            }
        }
    }
    public void LookPosition()
    {
        Vector3 lookPosition = _player.transform.position;
        Vector2 direction = new Vector2(lookPosition.x - transform.position.x, lookPosition.y - transform.position.y);
        transform.right = -direction;
        Debug.Log(transform.rotation.z);
    }
}




