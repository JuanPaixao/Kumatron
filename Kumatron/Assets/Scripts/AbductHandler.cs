using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbductHandler : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    public void AbductingEventEnd()
    {
        this.gameObject.SetActive(false);
        _player.playerCanMove = true;
        _player.rayFinished = true;
    }
}
