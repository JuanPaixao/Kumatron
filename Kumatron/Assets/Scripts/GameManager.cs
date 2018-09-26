using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameOver = false;
    private GameObject[] _factories, _chickens, _bulls, _cows;
    public int factoryHP, chickenNumber, bullNumber, cowNumber;
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
    public void LoadGame()
    {
        SceneManager.LoadScene("Stage_1");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void FactoryDestroyed()
    {
        factoryHP--;
    }
    public void AnimalsNumbers()
    {
        _chickens = GameObject.FindGameObjectsWithTag("Chicken");
        foreach (GameObject _chicken in _chickens)
        {
            if (_chickens != null)
            {
                chickenNumber = _chickens.Length;
            }
        }
        _bulls = GameObject.FindGameObjectsWithTag("Bull");
        foreach (GameObject _bull in _bulls)
        {
            if (_bulls != null)
            {
                bullNumber = _bulls.Length;
            }
        }

        _cows = GameObject.FindGameObjectsWithTag("Cow");
        foreach (GameObject _cow in _cows)
        {
            if (_cows != null)
            {
                cowNumber = _cows.Length;
            }
        }
        if (cowNumber <= 0 && chickenNumber <= 0 && bullNumber <= 0) // add variable to know if my player has an animal, in this case it will not be true either
        {
            SceneManager.LoadScene("Menu");
        }
    }
}

