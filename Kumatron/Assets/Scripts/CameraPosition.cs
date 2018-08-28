using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField]
    public float _xMax;
    [SerializeField]
    public float _yMax;
    [SerializeField]
    public float _xMin;
    [SerializeField]
    public float _yMin;
    private Transform _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(_player.position.x, _xMin, _xMax), Mathf.Clamp(_player.position.y, _yMin, _yMax), transform.position.z);

    }

  
}


