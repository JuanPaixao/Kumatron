using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbductHandler : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    public void AbductingEventEnd()
    {

        _player.playerCanMove = true;
        _player.rayFinished = true;
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Animal"))
        {
            this.GetComponent<Collider2D>().enabled = false;
        }
    }
}
