using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalScript : MonoBehaviour
{

    [SerializeField]
    private float _animalSpeed, _movingTime, _toggleMove, randomMoveTime;
    private Rigidbody2D _rb;
    [SerializeField]
    private bool _movingRight = false;
    public Transform hitDetection;
    public Transform hitDetectionUpLevel;
    public Transform hitDetectionDownLevel;
    public Transform hitHeightCheckLevel;
    public Transform middle;
    private LayerMask _layerMask = 1 << 10;
    [SerializeField]
    private RaycastHit2D _hit, _hitUp, _hitDown, _hitCheckHeight, _hitLeft, _hitRight;
    private ChickenAnimationControl _chickenAnimation;
    private BullAnimationControl _bullAnimation;
    private CowAnimationControl _cowAnimation;
    [SerializeField]
    private GameObject[] _circleEnemies;

    [SerializeField]
    private bool _isWalking;
    public bool caged;
    public bool chased;
    public bool canBeCaptured;
    public bool beingAbducted;
    public LayerMask canBeCapturedCondition;

    void Start()
    {
        if (this.gameObject.name == "Chicken")
        {
            randomMoveTime = Random.Range(5f, 9);
        }
        else if (this.gameObject.name == "Bull")
        {
            randomMoveTime = Random.Range(6.5f, 8);
        }
        else if (this.gameObject.name == "Cow")
        {
            randomMoveTime = Random.Range(4, 9);
        }
        _rb = GetComponent<Rigidbody2D>();
        _chickenAnimation = GetComponent<ChickenAnimationControl>();
        _bullAnimation = GetComponent<BullAnimationControl>();
        _cowAnimation = GetComponent<CowAnimationControl>();
        _toggleMove = randomMoveTime;
    }
    void FixedUpdate()
    {
        if (caged == false)
        {
            CheckCollision();
            CheckGrounded();
            Walk();
            checkTime();
            _rb.mass = 1;
        }
        else
        {
            _isWalking = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ray"))
        {
            StartCoroutine(AnimalAbducted());
        }
        if (other.CompareTag("Cage"))
        {
            caged = true;
            _rb.mass = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ray"))
        {
            if (!beingAbducted)
            {
                AnimalScript[] animals = FindObjectsOfType<AnimalScript>();
                foreach (var animal in animals)
                {
                    Debug.Log(animal.name);
                    animal.beingAbducted = true;
                }
                StartCoroutine(AnimalAbducted());
            }
        }
        if (other.CompareTag("Exit") && caged == true)
        {
            Destroy(gameObject, 1.5f);
        }
    }
    private void Walk()
    {
        if (_isWalking == true)
        {
            if (_hitDown == true)
            {
                //chicken
                if (this.gameObject.name == "Chicken")
                {
                    _chickenAnimation.ChickenCanMove(true);
                }
                //bull
                else if (this.gameObject.name == "Bull")
                {
                    _bullAnimation.BullCanMove(true);
                }
                //cow
                else if (this.gameObject.name == "Cow")
                {
                    _cowAnimation.CowCanMove(true);
                }
            }
            _rb.velocity = new Vector2(-_animalSpeed * Time.deltaTime, 0);
        }
        else
        {
            if (this.gameObject.name == "Chicken")
            {
                _chickenAnimation.ChickenCanMove(false);
            }
            else if (this.gameObject.name == "Bull")
            {
                _bullAnimation.BullCanMove(false);
            }
            else if (this.gameObject.name == "Cow")
            {
                _cowAnimation.CowCanMove(false);
            }
        }
    }

    private void CheckCollision()
    {
        //turn
        _hit = Physics2D.Raycast(hitDetection.position, Vector2.zero, _animalSpeed, ~_layerMask);
        //check if theres free space to jump
        _hitUp = Physics2D.Raycast(hitDetectionUpLevel.position, Vector2.zero, _animalSpeed, ~_layerMask);
        //check if i conclude my jump
        _hitCheckHeight = Physics2D.Raycast(hitHeightCheckLevel.position, Vector2.zero, _animalSpeed, ~_layerMask);
        //down
        _hitDown = Physics2D.Raycast(hitDetectionDownLevel.position, Vector2.zero, _animalSpeed, ~_layerMask);

        //left
        _hitLeft = Physics2D.Raycast(middle.position, Vector2.left, 1.25f, canBeCapturedCondition);

        //right
        _hitRight = Physics2D.Raycast(middle.position, Vector2.right, 1.25f, canBeCapturedCondition);


        if (_hitLeft.collider == true && _hitRight.collider == true)
        {
            canBeCaptured = false;
        }
        else
        {
            canBeCaptured = true;
        }
        Debug.DrawRay(middle.position, Vector2.right, Color.yellow, 0.1f);
        Debug.DrawRay(middle.position, Vector2.left, Color.cyan, 0.1f);


        if (_hitCheckHeight.collider == true && _hitUp.collider != true && _hitCheckHeight.collider.tag != this.gameObject.tag)
        {
            if (this.gameObject.name == "Chicken")
            {
                _chickenAnimation.ChickenIsFalling(true);
            }
            _rb.AddForce(Vector2.up * 2000);
            _isWalking = true;

        }
        else if (_hit.collider == true)
        {
            if (_isWalking == true && !_hit.collider.CompareTag("Cage"))
            {
                if (_movingRight == true)
                {
                    _movingRight = false;
                    transform.eulerAngles = new Vector2(0, 0);
                    _animalSpeed = -_animalSpeed;
                }
                else
                {
                    _movingRight = true;
                    transform.eulerAngles = new Vector2(0, 180);
                    _animalSpeed = -_animalSpeed;
                }
            }
        }
        else if (_hitDown.collider == true && _hitDown.collider.tag == this.gameObject.tag)
        {
            if (_isWalking == true)
            {
                if (_movingRight == true)
                {
                    _isWalking = !_isWalking;
                    _rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
                    _rb.AddForce(Vector2.right * 1000);
                    _movingRight = false;
                    transform.eulerAngles = new Vector2(0, 0);
                    _animalSpeed = -_animalSpeed;

                }
                else
                {
                    _isWalking = !_isWalking;
                    _rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
                    _rb.AddForce(Vector2.right * 1000);
                    _movingRight = true;
                    transform.eulerAngles = new Vector2(0, 180);
                    _animalSpeed = -_animalSpeed;
                }
            }
        }
    }
    private void CheckGrounded()
    {
        if (this.gameObject.name == "Chicken")
        {
            if (_hitDown == false)
            {
                _chickenAnimation.ChickenIsFalling(true);
            }
            else
            {
                _chickenAnimation.ChickenIsFalling(false);
            }
        }
        else if (this.gameObject.name == "Bull")
        {
            if (_hitDown == false)
            {
                _bullAnimation.BullIsFalling(true);
            }
            else
            {
                _bullAnimation.BullIsFalling(false);
            }
        }
        else if (this.gameObject.name == "Cow")
        {
            if (_hitDown == false)
            {
                _cowAnimation.CowIsFalling(true);
            }
            else
            {
                _cowAnimation.CowIsFalling(false);
            }
        }
    }
    private void checkTime()
    {
        if (Time.time > _movingTime)
        {
            _movingTime = Time.time + _toggleMove;
            _isWalking = !_isWalking;
        }
    }
    private IEnumerator AnimalAbducted()
    {
        beingAbducted = true;
        while (1 < 2) //placeholder to gameover
        {
            _isWalking = false;
            _animalSpeed = 5f;
            //chicken
            if (this.gameObject.name == "Chicken")
            {
                _chickenAnimation.ChickenIsAbducting(true);
            }
            else if (this.gameObject.name == "Bull")
            {
                _bullAnimation.BullIsAbducting(true);
            }
            else if (this.gameObject.name == "Cow")
            {
                _cowAnimation.CowIsAbducting(true);
            }

            yield return new WaitForSeconds(1);
            AnimalScript[] animals = FindObjectsOfType<AnimalScript>();
            foreach (var animal in animals)
            {
               animal.beingAbducted = false;
            }
            Destroy(this.gameObject);
        }
    }
}