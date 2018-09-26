using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Explosion : MonoBehaviour
{
    private GameManager _gameManager;

    // Use this for initialization
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(GoToMenu());
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(2);
        _gameManager.LoadMenu();
    }

}
