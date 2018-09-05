using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator animalWithPlayer;

    void Start()
    {
        animalWithPlayer = GetComponent<Animator>();

    }
    public void StartIdleAnimation_Chicken()
    {
        animalWithPlayer.SetBool("IdleAnimation", true);
    }
    public void AttackAnimationPlay_Chicken()
    {
        animalWithPlayer.SetBool("Shooting", true);
    }
    public void AttackAnimationStop_Chicken()
    {
        animalWithPlayer.SetBool("Shooting", false);
    }


    public void StartIdleAnimation_Bull()
    {
        animalWithPlayer.SetBool("IdleAnimation", true);
    }
    public void AttackAnimationPlay_Bull()
    {
        animalWithPlayer.SetBool("isDashing", true);
    }
    public void AttackAnimationPlayLeft_Bull()
    {
        animalWithPlayer.SetBool("isDashingLeft", true);
    }
    public void AttackAnimationStop_Bull()
    {
        animalWithPlayer.SetBool("isDashing", false);
        animalWithPlayer.SetBool("isDashingLeft", false);
    }
}
