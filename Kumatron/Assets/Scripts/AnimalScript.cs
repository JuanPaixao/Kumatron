using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalScript : MonoBehaviour
{

    [SerializeField]
    private float _animalSpeed, _forceJump, _auxAnimalSpeed, _movingTime, _toggleMove, randomMoveTime;
    private Rigidbody2D _rb;
    [SerializeField]
    private bool _movingRight = false;
    public Transform hitDetection;
    public Transform hitDetectionUpLevel;
    public Transform hitDetectionDownLevel;
    public Transform hitHeightCheckLevel;
    private LayerMask _layerMask = 1 << 10;
    [SerializeField]
    private GameObject playerRay;
    [SerializeField]
    private RaycastHit2D _hit, _hitUp, _hitDown, _hitCheckHeight;
    private ChickenAnimationControl _chickenAnimation;
    [SerializeField]
    private bool _isWalking;

    void Start()
    {
        randomMoveTime = Random.RandomRange(3, 10);
        _rb = GetComponent<Rigidbody2D>();
        _chickenAnimation = GetComponent<ChickenAnimationControl>();
        _auxAnimalSpeed = _animalSpeed;
        _toggleMove = randomMoveTime;

    }
    void FixedUpdate()
    {
        CheckJumpCollision();
        CheckGrounded();
        Walk();
        checkTime();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        playerRay = other.gameObject.GetComponent<GameObject>();
        if (other.CompareTag("Ray"))
        {
            StartCoroutine(AnimalAbducted());
        }
    }


    private void Walk()
    {
        if (_isWalking == true)
        {
            if (_hitDown == true)
            {
                //chicken
                if (this.gameObject.name == "Chicken" || this.gameObject.name == "Chicken_Collision")
                {
                    _chickenAnimation.ChickenCanMove(true);
                }
            }
            _rb.velocity = new Vector2(-_animalSpeed * Time.deltaTime, 0);
        }
        else
        {
            if (this.gameObject.name == "Chicken" || this.gameObject.name == "Chicken_Collision")
            {
                _chickenAnimation.ChickenCanMove(false);
            }           
        }
    }

    private void CheckJumpCollision()
    {
        //turn
        _hit = Physics2D.Raycast(hitDetection.position, Vector2.zero, _animalSpeed, ~_layerMask);
        //check if theres free space to jump
        _hitUp = Physics2D.Raycast(hitDetectionUpLevel.position, Vector2.zero, _animalSpeed, ~_layerMask);
        //check if i conclude my jump
        _hitCheckHeight = Physics2D.Raycast(hitHeightCheckLevel.position, Vector2.zero, _animalSpeed, ~_layerMask);
        //down
        _hitDown = Physics2D.Raycast(hitDetectionDownLevel.position, Vector2.zero, _animalSpeed, ~_layerMask);


        if (_hitCheckHeight.collider == true && _hitUp.collider != true && _hitCheckHeight.collider.tag != this.gameObject.tag)
        {
            Debug.Log("Jump!");
            _rb.AddForce(Vector2.up * _forceJump);
        }
        else if (_hit.collider == true)
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
        else if (_hitDown.collider == true && _hitDown.collider.tag == this.gameObject.tag)
        {

            if (_movingRight == true)
            {
                _isWalking = !_isWalking;
                _rb.AddForce(Vector2.up * 60, ForceMode2D.Impulse);
                _rb.AddForce(Vector2.right * 30, ForceMode2D.Impulse);
                _movingRight = false;
                transform.eulerAngles = new Vector2(0, 0);
                _animalSpeed = -_animalSpeed;

            }
            else
            {
                _isWalking = !_isWalking;
                _rb.AddForce(Vector2.up * 60, ForceMode2D.Impulse);
                _rb.AddForce(Vector2.left * -30, ForceMode2D.Impulse);
                _movingRight = true;
                transform.eulerAngles = new Vector2(0, 180);
                _animalSpeed = -_animalSpeed;
            }
        }
    }
    private void CheckGrounded()
    {
        if (this.gameObject.name == "Chicken" || this.gameObject.name == "Chicken_Collision")
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
        while (1 < 2) //placeholder to gameover
        {
            _isWalking = false;
            _animalSpeed = 5f;
            //chicken
            if (this.gameObject.name == "Chicken" || this.gameObject.name == "Chicken_Collision")
            {
                _chickenAnimation.ChickenIsAbducting(true);
            }

            yield return new WaitForSeconds(1);
            Destroy(this.gameObject);
        }
    }

}

