using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElectrified : MonoBehaviour
{
    private Animator _enemyAnimator;
    public bool electrified = false;
    void Start()
    {
        _enemyAnimator = GetComponent<Animator>();
    }

    public void isElectrified()
    {
        _enemyAnimator.SetBool("isElectrified", true);
        electrified = true;
    }
    public void stopElectrified()
    {
        _enemyAnimator.SetBool("isElectrified", false);
        electrified = false;
    }
    void OnParticleCollision(GameObject other)
    {
        isElectrified();
    }

}
