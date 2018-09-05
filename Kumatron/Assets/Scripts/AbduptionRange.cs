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
        if (otherObject != null)
        {
            if (other.gameObject.CompareTag("Animal") && Input.GetKey(KeyCode.Space))
            {
                otherObject = other.gameObject.name;

                _player.TurnRayOn(other.gameObject.name);
            }
        }

    }
}

