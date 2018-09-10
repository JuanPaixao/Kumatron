using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCannon : MonoBehaviour
{
    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (_player != null)
        {
            LookPosition();
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
