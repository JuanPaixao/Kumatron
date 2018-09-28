using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameOver = false;
    private GameObject[] _factories, _chickens, _bulls, _cows;
    public int factoryHP, chickenNumber, bullNumber, cowNumber;
    public bool pause;
    private UIManager _uiManager;
    void Start()
    {
        _factories = GameObject.FindGameObjectsWithTag("Factory");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_factories != null)
        {
            foreach (GameObject _factory in _factories)
            {
                factoryHP++;
            }
        }
        Time.timeScale = 1;
        pause = false;
        AudioListener.volume = 1;
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Stage_1");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void RestartLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void PauseGame()
    {
        pause = true;
        Time.timeScale = 0;
        _uiManager.pauseMenu.gameObject.SetActive(true);
        AudioListener.volume = 0.3f;
    }
    public void ResumeGame()
    {
        pause = false;
        Time.timeScale = 1;
        _uiManager.pauseMenu.gameObject.SetActive(false);
        AudioListener.volume = 1;
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
    }
}

