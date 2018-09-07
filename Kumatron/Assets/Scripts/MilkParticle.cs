using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkParticle : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem[] _milkParticles;
    [SerializeField]
    private Player _player;
    void Update()
    {
        print(_player.isMoving);
        if (_player.isMoving == true)
        {
            if (!_milkParticles[0].isPlaying && !_milkParticles[1].isPlaying && !_milkParticles[2].isPlaying)
            {
                _milkParticles[0].Play();
                _milkParticles[1].Play();
                _milkParticles[2].Play();
                Debug.Log("its raining milk");
            }
        }
        else
        {
            _milkParticles[0].Stop();
            _milkParticles[1].Stop();
            _milkParticles[2].Stop();
        }
    }
}
