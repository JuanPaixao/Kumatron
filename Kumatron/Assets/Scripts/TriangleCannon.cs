using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCannon : MonoBehaviour
{
    private Transform _player;
    [SerializeField]
    private EnemyElectrified _enemyElectrified;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (_player != null && _enemyElectrified != null)
        {
            if (_enemyElectrified.electrified == false)
                LookPosition();
        }
    }

    public void LookPosition()
    {
        Vector3 lookPosition = _player.transform.position;
        Vector2 direction = new Vector2(lookPosition.x - transform.position.x, lookPosition.y - transform.position.y).normalized;
        transform.right = -direction;
    }

}
