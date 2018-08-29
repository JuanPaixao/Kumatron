using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _playerSpeed;
    private Vector3 _targetPos;
    [SerializeField]
    private bool _withAnimal = false;
    [SerializeField]
    private GameObject _kumatronRay;
    private float _kumatronRayRange = 4.04f;
    [SerializeField]
    private GameObject _animalPos;
    [SerializeField]
    private string _animalTarget, _animalWithme;
    private RaycastHit2D _hit;
    public bool playerCanMove = true;
    void Start()
    {
        _targetPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

    }
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetMouseButton(0) && playerCanMove == true)
        {
            //check if has target
            CheckObject();
        }
        transform.position = Vector2.MoveTowards(transform.position, _targetPos, _playerSpeed * Time.deltaTime);


    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tilemap"))
        {
            _targetPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }

    private void CheckObject()
    {

        _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (_hit.collider != null)
        {
            if (_hit.collider.CompareTag("Animal"))
            {
                _animalPos = _hit.collider.gameObject;
                _animalTarget = _animalPos.gameObject.name;
                _animalPos.transform.position = new Vector3(_animalPos.transform.position.x, _animalPos.transform.position.y, _animalPos.transform.position.z);
                _targetPos = new Vector3(_animalPos.transform.position.x, _animalPos.transform.position.y + _kumatronRayRange, _animalPos.transform.position.z);
                StartCoroutine(Lock());

            }
        }
        else if (_hit.collider == null)
        {
            StopCoroutine(Lock());
            _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _animalPos = null;
            _targetPos.z = this.gameObject.transform.position.z;
            _animalTarget = null;
            Debug.Log("nothing");
        }
    }

    private void TurnRayOn()
    {
        if (this.transform.position.y == _targetPos.y && _withAnimal == false)
        {
            _kumatronRay.SetActive(true);
            _withAnimal = true;
            _animalWithme = _animalTarget;
            playerCanMove = false;
        }
    }
    private IEnumerator Lock()
    {
        while (_hit.collider != null && _hit.collider.CompareTag("Animal"))
        {
            _animalPos = _hit.collider.gameObject;
            _animalTarget = _animalPos.gameObject.name;
            _animalPos.transform.position = new Vector3(_animalPos.transform.position.x, _animalPos.transform.position.y, _animalPos.transform.position.z);
            _targetPos = new Vector3(_animalPos.transform.position.x, _animalPos.transform.position.y + _kumatronRayRange, _animalPos.transform.position.z);
            TurnRayOn();
            yield return new WaitForSeconds(0.01f);
        }
    }
}

