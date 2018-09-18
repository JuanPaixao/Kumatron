using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyExplosion : MonoBehaviour
{
    public void DestroyingShoot()
    {
        Destroy(this.gameObject);
    }
}
