using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool gameOver = false;
    private GameObject[] _factories;
    public int factoryHP;
    void Start()
    {
        _factories = GameObject.FindGameObjectsWithTag("Factory");

        if (_factories != null)
        {
            foreach (GameObject _factory in _factories)
            {
                factoryHP++;
            }
        }

    }
    void Update()
    {

    }

    public void FactoryDestroyed()
    {
        factoryHP--;
    }
}
