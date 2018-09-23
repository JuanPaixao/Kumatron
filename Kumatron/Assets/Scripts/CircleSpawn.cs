using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject _circleEnemy;
    private Player _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (_player != null)
        {
            GameObject circle = GameObject.Find("CircleEnemy");
            if (circle == null)
            {
                Instantiate(_circleEnemy, this.transform.position, Quaternion.identity);
            }
        }
	}
}
