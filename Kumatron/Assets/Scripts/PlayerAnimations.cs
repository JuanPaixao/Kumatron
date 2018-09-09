using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator animalWithPlayer;

    void Awake()
    {
        animalWithPlayer = GetComponent<Animator>();

    }
    public void StartIdleAnimation_Chicken()
    {
        if (animalWithPlayer != null)
        {
            animalWithPlayer.SetBool("IdleAnimation", true);
        }
    }
    public void AttackAnimationPlay_Chicken()
    {
        if (animalWithPlayer != null)
        {
            animalWithPlayer.SetBool("Shooting", true);
        }
    }
    public void AttackAnimationStop_Chicken()
    {
        if (animalWithPlayer != null)
        {
            animalWithPlayer.SetBool("Shooting", false);
        }
    }
    public void StartIdleAnimation_Bull()
    {
        if (animalWithPlayer != null)
        {
            animalWithPlayer.SetBool("IdleAnimation", true);
        }
    }
    public void AttackAnimationPlay_Bull()
    {
        if (animalWithPlayer != null)
        {
            animalWithPlayer.SetBool("isDashing", true);
        }
    }
    public void AttackAnimationStop_Bull()
    {
        if (animalWithPlayer != null)
        {
            animalWithPlayer.SetBool("isDashing", false);
        }
    }
    public void StartIdleAnimation_Cow()
    {
        if (animalWithPlayer != null)
        {
            animalWithPlayer.SetBool("IdleAnimation", true);
        }
    }
}
