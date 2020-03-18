using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    public int animalsInside;
    private UIManager _uiManager;
    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Animal"))
        {
            animalsInside++;
            _uiManager.TextAnimalQuantity(animalsInside);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Animal"))
        {
            animalsInside--;
            _uiManager.TextAnimalQuantity(animalsInside);
        }
    }
}