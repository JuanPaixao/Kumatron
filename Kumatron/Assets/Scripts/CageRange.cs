using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageRange : MonoBehaviour
{

    [SerializeField]
    private AnimalScript _animal;
    [SerializeField]
    private GameObject _cage;
    [SerializeField]
    private GameObject[] _animalCaptured;
    private bool _withAnimal;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Animal") && _withAnimal == false)
        {
            if (_cage != null)
            {
                _animal = other.GetComponent<AnimalScript>();
                CageOn();
            }
        }
    }
    private void CageOn()
    {
        _cage.SetActive(true);
        _withAnimal = true;
    }
    private void OnDestroy() {
        _animal.caged = false;
    }
}