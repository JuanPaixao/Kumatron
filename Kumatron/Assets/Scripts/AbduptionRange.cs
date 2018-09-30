using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AbduptionRange : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    public string otherObject;
    public bool canAbduct;
    private UIManager _uIManager;
    private RaycastHit2D _hit;
    [SerializeField]
    private LayerMask _layer;
    [SerializeField]
    private float _rangeAbudction;


    void Start()
    {
        _uIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
    }
    void LateUpdate()
    {
        if (_player != null)
        {
            this.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y - 1.10f);
        }
    }
    void Update()
    {
        if (_player != null && _player.withAnimal == true)
        {
            canAbduct = false;
        }
        else if (_hit.collider == null)
        {
            canAbduct = false;
        }
        _hit = Physics2D.Raycast(transform.position, Vector2.down, _rangeAbudction, _layer);
        {
            if (_hit.collider != null)
            {
                Debug.Log(_hit.collider.name);
                {
                    if (_hit.collider != null)
                    {
                        if (_hit.collider.name == "Bull" || _hit.collider.name == "Cow" || _hit.collider.name == "Chicken")
                        {
                            AnimalScript animal = _hit.collider.GetComponent<AnimalScript>();
                            if (animal.caged != true)
                            {
                                canAbduct = true;
#if UNITY_ANDROID
                                if (CrossPlatformInputManager.GetButtonDown("RayButton") && canAbduct == true)
                                {
                                    Debug.Log("Etapa 2");
                                    otherObject = _hit.collider.name;
                                    _player.TurnRayOn(_hit.collider.name);
                                }
#else
                                if (Input.GetKey(KeyCode.Space) && canAbduct == true)
                                {
                                    Debug.Log("Etapa 2");
                                    otherObject = _hit.collider.name;
                                    _player.TurnRayOn(_hit.collider.name);
                                }
#endif
                            }
                        }
                    }
                }

            }
        }


        _uIManager.UpdateRayCheck(canAbduct);


    }
}
