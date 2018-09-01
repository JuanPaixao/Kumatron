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
}
