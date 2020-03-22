using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkMachine : MonoBehaviour
{
    public float level, maxLevel;
    public bool isFull;
    public Animator doorAnimator;
    private Animator _myAnimator;

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.gameObject);

        if (other.gameObject.CompareTag("Milk"))
        {
            level+=0.1f;
        }
        if (level > maxLevel)
        {
            isFull = true;
            _myAnimator.SetBool("Full", true);
        }
    }
    public void OpenDoor()
    {
        doorAnimator.SetBool("Open", true);
    }
}
