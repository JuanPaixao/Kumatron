using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject _laser;
    void Start()
    {
        this.gameObject.name = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {

    }
}