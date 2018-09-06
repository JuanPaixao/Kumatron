using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkParticles : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem[] _milkParticles;
    void Update()
    {
        if (_milkParticles != null)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
            {
                _milkParticles[0].Play(true);
                _milkParticles[1].Play(true);
                _milkParticles[2].Play(true);
                Debug.Log("its raining milk");
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
            {
                _milkParticles[0].Stop();
                _milkParticles[1].Stop();
                _milkParticles[2].Stop();

                Debug.Log("its notraining milk");
            }
        }

    }
}
