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
        electrified = true;
        _enemyAnimator.SetBool("isElectrified", true);
    }
    public void stopElectrified()
    {
        electrified = false;
        _enemyAnimator.SetBool("isElectrified", false);
    }
    void OnParticleCollision(GameObject other)
    {
        isElectrified();
    }
}
