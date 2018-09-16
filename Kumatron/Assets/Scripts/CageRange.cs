using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageRange : MonoBehaviour
{

    [SerializeField]
    private AnimalScript _animal;
    [SerializeField]
    private GameObject _cage;
    private bool _withAnimal;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Animal") && _withAnimal == false)
        {
            if (_cage != null)
            {
                CageOn();
            }
        }
    }
    private void CageOn()
    {
        _cage.SetActive(true);
        _withAnimal = true;
    }
}