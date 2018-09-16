using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkParticle : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem[] _milkParticles;
    [SerializeField]
    private Player _player;
    private Animator _cowAnimator;

    void Awake()
    {
        _cowAnimator = GetComponent<Animator>();
    }
    void Update()
    {

        if (_player.isMoving == true)
        {
            CowCanMove(true);
            if (!_milkParticles[0].isPlaying && !_milkParticles[1].isPlaying && !_milkParticles[2].isPlaying)
            {

                _milkParticles[0].Play();
                _milkParticles[1].Play();
                _milkParticles[2].Play();
            }
        }
        else
        {
            CowCanMove(false);
            _milkParticles[0].Stop();
            _milkParticles[1].Stop();
            _milkParticles[2].Stop();
        }
    }
    public void CowCanMove(bool attacking)
    {
        _cowAnimator.SetBool("isAttacking", attacking);
    }
}
