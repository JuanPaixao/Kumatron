using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseAnimal : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GameObject[] animals;
    [SerializeField]
    private GameObject _egg;

    public void ReleasePlayerAnimal()
    {
        if (_player.animalWithMe != "")
        {
            if (_player.animalWithMe == "Chicken")
            {
                Instantiate(animals[0], this.transform.position, Quaternion.identity);
                Debug.Log("Throwing the " + _player.animalWithMe);
                _player.animalWithMe = null;
                _player.withAnimal = false;
                _player.animalPowerUp[0].SetActive(false);
                StartCoroutine(ControlRayRoutine());
            }
            else if (_player.animalWithMe == "Bull")
            {
                Instantiate(animals[1], this.transform.position, Quaternion.identity);
                Debug.Log("Throwing the " + _player.animalWithMe);
                _player.animalWithMe = null;
                _player.withAnimal = false;
                _player.animalPowerUp[1].SetActive(false);
                StartCoroutine(ControlRayRoutine());
            }
            else if (_player.animalWithMe == "Cow")
            {
                Instantiate(animals[2], this.transform.position, Quaternion.identity);
                Debug.Log("Throwing the " + _player.animalWithMe);
                _player.animalWithMe = null;
                _player.withAnimal = false;
                _player.animalPowerUp[2].SetActive(false);
                StartCoroutine(ControlRayRoutine());
            }
        }
    }
    private IEnumerator ControlRayRoutine()
    {
        _player.rayFinished = false;
        yield return new WaitForSeconds(1.5f);
        _player.rayFinished = true;
    }

    public void Attack()
    {
        if (_player.withAnimal == true)
        {
            if (_player.animalWithMe == "Chicken")
            {
                Instantiate(_egg, this.transform.position, Quaternion.identity);
            }
        }
    }
}
