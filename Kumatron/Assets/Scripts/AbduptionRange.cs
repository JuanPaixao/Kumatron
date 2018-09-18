using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbduptionRange : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    public string otherObject;

    void LateUpdate()
    {
        if (_player != null)
        {
            this.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y - 2.06f);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (otherObject != null)
        {
            if (other.gameObject.CompareTag("Animal") && Input.GetKey(KeyCode.Space))
            {
                AnimalScript animal = other.GetComponent<AnimalScript>();
                if (animal != null && animal.caged != true)
                {
                    otherObject = other.gameObject.name;
                    _player.TurnRayOn(other.gameObject.name);
                }
            }
        }
    }
}

