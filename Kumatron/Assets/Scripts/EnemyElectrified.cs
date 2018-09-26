using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElectrified : MonoBehaviour
{
    private Animator _enemyAnimator;
    public bool electrified = false;
    [SerializeField]
    private AudioClip[] _electrifiedSounds;
    private AudioSource _audioSource;
    private Enemy _enemy;
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _audioSource = GetComponent<AudioSource>();
        _enemyAnimator = GetComponent<Animator>();
    }

    public void isElectrified()
    {
        electrified = true;
        _enemyAnimator.SetBool("isElectrified", true);
    }
    public void stopElectrified()
    {
        _enemyAnimator.SetBool("isElectrified", false);
        _enemy.enemyHP--;
        electrified = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        isElectrified();
    }
    private void ElectrifiedSound()
    {
        int electrifiedSound = Random.Range(0, 4);
        _audioSource.PlayOneShot(_electrifiedSounds[electrifiedSound], 1f);
    }
}
