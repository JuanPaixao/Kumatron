using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _playerSpeed, _auxSpeed, _cowSpeed;
    public bool withAnimal = false;
    public bool rayFinished = true;
    [SerializeField]
    private GameObject _kumatronRay;
    [SerializeField]
    private ReleaseAnimal _releaseAnimal;
    public string animalWithMe, playerDirection;
    private RaycastHit2D _hit;
    public bool playerCanMove = true;
    public GameObject[] animalPowerUp;
    [SerializeField]
    private PlayerAnimations[] playerAnimations;
    private float _cooldown = 0f;
    private float _nextTime = 0.5f;
    public Rigidbody2D rb;
    public float dashSpeed;
    [SerializeField]
    private AbduptionRange abduptionRange;
    [SerializeField]
    private Animator _animator;
    public bool isDashing, isMoving;
    public float playerHP = 5;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _auxSpeed = _playerSpeed;
        _cowSpeed = _playerSpeed * 1.5f;
    }
    void Update()
    {
        PlayerAttack();
        MovementAnimationsControl();
        if (playerHP < 0)
        {
            PlayerDefeated();
        }
    }


    void FixedUpdate()
    {
        if (playerDirection != "none")
        {
            PlayerMovement();
        }
    }
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && withAnimal == true && rayFinished == true)
        {
            _releaseAnimal.ReleasePlayerAnimal();
            _animator.SetBool("isMovingLeft", false);
            _animator.SetBool("isMovingRight", false);
            _animator.SetBool("isMovingDown", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _cooldown)
        {
            _releaseAnimal.Attack();
            _cooldown = Time.time + _nextTime;

            if (animalWithMe == "Chicken")
            {
                playerAnimations[0].AttackAnimationPlay_Chicken();
            }
            else if (animalWithMe == "Bull")
            {
                playerAnimations[1].AttackAnimationPlay_Bull();
            }
        }

        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (animalWithMe == "Chicken")
                playerAnimations[0].AttackAnimationStop_Chicken();
        }
    }

    private void PlayerMovement()
    {
        if (playerCanMove == true)
        {
            rb.velocity = Vector2.zero;
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalmovement = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(horizontalMovement, verticalmovement);
            rb.MovePosition(rb.position + movement * _playerSpeed * Time.deltaTime);
        }
        if (playerCanMove != true)
        {
            _animator.SetBool("isMovingRight", false);
            _animator.SetBool("isMovingLeft", false);
        }

        if (animalWithMe == "Cow")
        {
            _playerSpeed = _cowSpeed;
        }
        else
        {
            _playerSpeed = _auxSpeed;
        }


        if (isMoving != true && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            isMoving = true;
        }
        else
        {
            if (!Input.anyKey)
            {
                isMoving = false;
            }
        }

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tilemap"))
        {
            rb.transform.position = (new Vector2(this.transform.position.x, this.transform.position.y));
        }
        if (other.gameObject.CompareTag("Enemy") && rayFinished == true)
        {
            playerCanMove = false;
            if (other.gameObject.transform.position.x > this.gameObject.transform.position.x)
            {
                rb.AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            }
            else if (other.gameObject.transform.position.x < this.gameObject.transform.position.x)
            {
                rb.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
            StartCoroutine(PlayerCanMoveAgain());
        }
    }

    public void TurnRayOn(string animalName)
    {
        if (withAnimal == false && rayFinished == true)
        {
            _kumatronRay.SetActive(true);
            withAnimal = true;
            animalWithMe = animalName;
            playerCanMove = false;
            rayFinished = false;

            if (animalWithMe == "Chicken" || animalWithMe == "Chicken_Collision")
            {
                StartCoroutine(AnimalPowerUp(0));
            }
            if (animalWithMe == "Bull" || animalWithMe == "Bull_Collision")
            {
                StartCoroutine(AnimalPowerUp(1));
            }
            if (animalWithMe == "Cow" || animalWithMe == "Cow_Collision")
            {
                StartCoroutine(AnimalPowerUp(2));
            }
        }
    }

    private void MovementAnimationsControl()
    {
        if (animalWithMe != "Cow")
        {
            if (Input.GetKey(KeyCode.D) && playerCanMove == true && !Input.GetKey(KeyCode.A))
            {
                _animator.SetBool("isMovingRight", true);
                _animator.SetBool("isMovingLeft", false);
                _animator.SetBool("isMovingDown", false);
                playerDirection = "right";
            }
            else if (Input.GetKey(KeyCode.A) && playerCanMove == true && !Input.GetKey(KeyCode.D))
            {
                _animator.SetBool("isMovingLeft", true);
                _animator.SetBool("isMovingRight", false);
                playerDirection = "left";
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                _animator.SetBool("isMovingLeft", false);
                _animator.SetBool("isMovingRight", false);
                _animator.SetBool("isMovingDown", false);
                playerDirection = "none";
            }
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            _animator.SetBool("isMovingLeft", false);
            _animator.SetBool("isMovingRight", false);
            _animator.SetBool("isMovingDown", false);
            playerDirection = "none";
        }
        else if (Input.GetKey(KeyCode.S) && playerCanMove == true)
        {
            _animator.SetBool("isMovingDown", true);
            playerDirection = "down";
        }
        else if (Input.GetKey(KeyCode.D) && playerCanMove == true)
        {
            _animator.SetBool("isMovingRight", true);
            playerDirection = "downRight";
        }
        else if (Input.GetKey(KeyCode.A) && playerCanMove == true)
        {
            _animator.SetBool("isMovingLeft", true);
            playerDirection = "downLeft";
        }

        else if (!Input.GetKey(KeyCode.S))
        {
            _animator.SetBool("isMovingDown", false);
        }

        if (Input.GetKeyUp(KeyCode.D) && playerCanMove == true)
        {
            _animator.SetBool("isMovingRight", false);
        }
        else if (Input.GetKeyUp(KeyCode.A) && playerCanMove == true)
        {
            _animator.SetBool("isMovingLeft", false);
        }
        else if (Input.GetKeyUp(KeyCode.S) && playerCanMove == true)
        {
            _animator.SetBool("isMovingDown", false);
        }
        //bull attack

        if (Input.GetKeyDown(KeyCode.Mouse0) && animalWithMe == "Bull" && playerCanMove == true)
        {
            playerAnimations[1].AttackAnimationPlay_Bull();
            if (Input.GetKeyDown(KeyCode.A) || playerDirection == "left" && playerCanMove == true)
            {
                _animator.SetBool("isDashingLeft", true);
                Dash();
            }
            else if (Input.GetKeyDown(KeyCode.D) || playerDirection == "right" && playerCanMove == true)
                _animator.SetBool("isDashing", true);
            Dash();
        }
    }

    private IEnumerator AnimalPowerUp(int animal)
    {
        yield return new WaitForSeconds(1);
        animalPowerUp[animal].SetActive(true);
    }

    public void Dash()
    {
        if (animalWithMe == "Bull")
        {
            if (playerDirection == "right")
            {
                isDashing = true;
                StartCoroutine(RightDashCoroutine());
            }
            else if (playerDirection == "left")
            {
                isDashing = true;
                StartCoroutine(LeftDashCoroutine());
            }
        }
    }

    private void PlayerDefeated()
    {

    }


    private IEnumerator RightDashCoroutine()
    {
        while (isDashing == true)
        {
            rb.velocity = new Vector2(dashSpeed, 0);
            playerCanMove = false;
            yield return new WaitForSeconds(0.2f);
            playerAnimations[1].AttackAnimationStop_Bull();
            _animator.SetBool("isDashing", false);
            isDashing = false;
            rb.velocity = Vector2.zero;
            playerCanMove = true;
        }
    }

    private IEnumerator LeftDashCoroutine()
    {
        while (isDashing == true)
        {
            rb.velocity = new Vector2(-dashSpeed, 0);
            playerCanMove = false;
            yield return new WaitForSeconds(0.2f);
            playerAnimations[1].AttackAnimationStop_Bull();
            _animator.SetBool("isDashingLeft", false);
            isDashing = false;
            rb.velocity = Vector2.zero;
            playerCanMove = true;

        }
    }
    private IEnumerator PlayerCanMoveAgain()
    {
        yield return new WaitForSeconds(0.25f);
        playerCanMove = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        TriangleShoot triangleShoot;
        if (other.gameObject.CompareTag("Enemy_Shoot"))
        {
            triangleShoot = other.gameObject.GetComponent<TriangleShoot>();
            triangleShoot.DestroyingShootAnimation();
            playerHP--;
        }
    }
}