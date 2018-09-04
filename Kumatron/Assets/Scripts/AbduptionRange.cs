using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbduptionRange : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    public string otherObject;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Animal"))
        {
            otherObject = other.gameObject.name;
            if (Input.GetKeyDown(KeyCode.Space) && otherObject != null)
            {
                _player.TurnRayOn(other.gameObject.name);
            }
        }
        else
        {
            otherObject = null;

        }
    }
}
