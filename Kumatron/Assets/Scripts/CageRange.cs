using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageRange : MonoBehaviour
{

    [SerializeField]
    private AnimalScript _animal;
    [SerializeField]
    private GameObject _cage;
    private bool _withAnimal;
    [SerializeField]
    private GameObject[] _animalTargets;
    [SerializeField]
    private AnimalScript _allAnimals;
    [SerializeField]
    private GameObject closetAnimal = null;
    public bool hasTarget = false;
    private Rigidbody2D _rb;
    private Vector2 difference;
    [SerializeField]
    private float _speed;
    private Transform _exitPosition;
    [SerializeField]
    private Enemy _enemy;
    public Transform downLeftHitCollider;
    public Transform downRightHitCollider;
    private EnemyElectrified _enemyElectrified;
    private RaycastHit2D _leftHit, _rightHit, _downLeftHit, _downRightHit;
    private LayerMask _layerMask = 1 << 10;
    private bool _electrified;
    void Start()
    {
        this.gameObject.name = "CircleEnemy";
        _enemyElectrified = GetComponent<EnemyElectrified>();
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CircleEnemyCoroutine());
        _exitPosition = GameObject.FindGameObjectWithTag("Exit").transform;
    }
    void Update()
    {
        _electrified = _enemyElectrified.electrified;
        if (_electrified == false)
        {
            CircleMovement();
            CheckCollisions();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Animal") && _withAnimal == false)
        {
            if (_cage != null)
            {
                _animal = other.GetComponent<AnimalScript>();
                CageOn();
            }
        }
    }
    private void CageOn()
    {
        _cage.SetActive(true);
        StartCoroutine(CageDelay());
    }
    private void OnDestroy()
    {
        if (_animal != null)
        {
            _animal.caged = false;
        }
        if (_allAnimals != null)
        {
            _allAnimals.chased = false;
        }
    }
    public GameObject FindClosestAnimal()
    {
        _animalTargets = GameObject.FindGameObjectsWithTag("Animal");
        float distance = Mathf.Infinity;
        Vector2 position = this.transform.position;
        if (_animalTargets != null)
        {
            foreach (GameObject _animalTarget in _animalTargets)
            {
                difference = _animalTarget.transform.position - transform.position;
                float currentDistance = difference.sqrMagnitude;

                if (currentDistance < distance)
                {
                    _allAnimals = _animalTarget.GetComponent<AnimalScript>();
                    if (_allAnimals.caged != true)
                    {
                        _allAnimals.chased = true;
                        closetAnimal = _animalTarget;
                        distance = currentDistance;
                        hasTarget = true;
                    }
                }
            }
            return closetAnimal;
        }
        else
        {
            return null;
        }
    }
    private void CheckCollisions()
    {
        _leftHit = Physics2D.Raycast(_enemy.LeftDetection.position, Vector2.zero, 0, ~_layerMask);
        _rightHit = Physics2D.Raycast(_enemy.RightDetection.position, Vector2.zero, 0, ~_layerMask);
        if (_withAnimal == true)
        {
            _downLeftHit = Physics2D.Raycast(downLeftHitCollider.position, Vector2.zero, 0, ~_layerMask);
            _downRightHit = Physics2D.Raycast(downRightHitCollider.position, Vector2.zero, 0, ~_layerMask);
        }
        if (_leftHit.collider == true || _rightHit.collider == true || _downRightHit.collider == true || _downLeftHit.collider == true && _withAnimal == true)
        {
            _rb.AddForce(Vector2.up * 1500);
        }
    }
    private IEnumerator CircleEnemyCoroutine()
    {
        while (this.gameObject != null)
        {
            FindClosestAnimal();
            yield return new WaitForSeconds(0.3f);
        }
    }
    private IEnumerator CageDelay()
    {
        _rb.MovePosition(this.transform.position);
        _rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.35f);
        _withAnimal = true;
    }
    private void CircleMovement()
    {
        if (_withAnimal != true)
        {
            if (closetAnimal != null)
            {
                Vector2 capturePosition = new Vector2(closetAnimal.transform.position.x, closetAnimal.transform.position.y + 1.350f);
                _rb.MovePosition(Vector2.MoveTowards(this.transform.position, capturePosition, _speed * Time.deltaTime));
            }
        }
        else
        {
            Vector2 exitPosition = _exitPosition.transform.position;
            _rb.MovePosition(Vector2.MoveTowards(this.transform.position, exitPosition, _speed * Time.deltaTime));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exit") && _withAnimal == true)
        {
            Destroy(gameObject, 2f);
        }
    }
}