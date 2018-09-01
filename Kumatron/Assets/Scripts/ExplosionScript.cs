using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{

    public void DestroyExplosion()
    {
        Destroy(this.gameObject);
    }
}
