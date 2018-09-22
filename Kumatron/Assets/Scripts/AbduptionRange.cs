using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbduptionRange : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    public string otherObject;
    public bool canAbduct;
    private UIManager _uIManager;

    void Start()
    {
        _uIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
    }
    void LateUpdate()
    {
        if (_player != null)
        {
            this.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y - 2.06f);
        }
    }
    void Update()
    {
        if (_player != null && _player.withAnimal == true)
        {
            canAbduct = false;
        }
        _uIManager.UpdateRayCheck(canAbduct);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (otherObject != null)
        {
            if (other.gameObject.CompareTag("Animal") && _player.withAnimal != true)
            {
                AnimalScript animal = other.GetComponent<AnimalScript>();
                if (animal != null && animal.caged != true)
                {
                    canAbduct = true;
                    if (Input.GetKey(KeyCode.Space) && canAbduct == true)
                    {
                        otherObject = other.gameObject.name;
                        _player.TurnRayOn(other.gameObject.name);
                    }
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (otherObject != null)
        {
            if (other.gameObject.CompareTag("Animal") && _player.withAnimal != true)
            {
                canAbduct = false;
                _uIManager.UpdateRayCheck(canAbduct);
            }
        }
    }
}
