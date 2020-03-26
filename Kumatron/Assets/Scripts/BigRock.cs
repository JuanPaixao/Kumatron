﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRock : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float rangeDetection;
    public LayerMask layerMask;
    public bool onCollisionWithPlayer;
    private Player _player;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        onCollisionWithPlayer = Physics2D.OverlapCircle(this.transform.position, rangeDetection, layerMask);

        if (_player != null && onCollisionWithPlayer && _player.isDashing)
        {
            _rb.mass = 3;
        }
        else
        {
            _rb.mass = 100;
        }
    }
    void OnDrawGizmos()
    {
        //Color of gizmos is blue.
        Gizmos.color = Color.blue;
        //Gizmos is to draw a wire sphere using the grounder.transform's position, and the radius value.
        Gizmos.DrawWireSphere(this.transform.position, rangeDetection);

    }
}