using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseAnimal : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GameObject[] animals;
    public void ReleasePlayerAnimal()
    {
        if (_player.animalWithMe != "")
        {
            if (_player.animalWithMe == "Chicken" || _player.animalWithMe == "Chicken_Collision")
            {
                Instantiate(animals[0], this.transform.position, Quaternion.identity);
                Debug.Log("Throwing the " + _player.animalWithMe);
                _player.animalWithMe = null;
                _player.withAnimal = false;
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
}
