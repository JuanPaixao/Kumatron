using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject _circleEnemy;
    private Player _player;
    public float distanceToDetect, distance;
    public float timeToSpawn, spawnActualTime;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spawnActualTime = timeToSpawn;
    }
    void Update()
    {
        spawnActualTime -= Time.deltaTime;
        if (_player != null)
        {
            distance = Vector2.Distance(_player.transform.position, this.transform.position);
            GameObject circle = GameObject.Find("CircleEnemy");
            if (circle == null && distance < distanceToDetect && spawnActualTime < 0)
            {
                Instantiate(_circleEnemy, this.transform.position, Quaternion.identity);
                spawnActualTime = timeToSpawn;
            }
        }
    }
}
