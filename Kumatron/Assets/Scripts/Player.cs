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
    public bool isDashing, isMoving, isDefeated;
    public int playerHP;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private AudioClip _damagePlayer, _dashPlayer;
    private UIManager _uiManager;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _auxSpeed = _playerSpeed;
        _cowSpeed = _playerSpeed * 1.8f;
        isDefeated = false;
    }
    void Update()
    {
        PlayerAttack();
        MovementAnimationsControl();
        if (playerHP <= 0 && isDefeated == false)
        {
            PlayerDefeated();
        }
        if (rayFinished != true)
        {
            this.gameObject.transform.position = this.gameObject.transform.position;
        }
        if (rayFinished != true)
        {
            rb.mass = 50;
        }
        else
        {
            rb.mass = 1;
        }
        if (withAnimal == true)
        {
            CheckAnimal();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameManager.LoadMenu();
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
            _uiManager.CheckAnimal(animalWithMe);
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
            _animator.SetBool("withCow", true);
        }
        else
        {
            _playerSpeed = _auxSpeed;
            _animator.SetBool("withCow", false);
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
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.name == "SquareEnemy" && rayFinished == true && isDashing == false)
        {
            SquareEnemy squareEnemy = other.gameObject.GetComponent<SquareEnemy>();
            if (squareEnemy != null && squareEnemy.enemyDashing == true && playerHP > 0)
            {
                if (other.gameObject.transform.position.x >= this.gameObject.transform.position.x)
                {
                    playerHP--;
                    playerCanMove = false;
                    Instantiate(_explosion, this.transform.position, Quaternion.identity);
                    rb.AddForce(Vector2.left * 22, ForceMode2D.Impulse);
                    DamagePlayerSound();
                }
                else if (other.gameObject.transform.position.x < this.gameObject.transform.position.x)
                {
                    playerHP--;
                    playerCanMove = false;
                    Instantiate(_explosion, this.transform.position, Quaternion.identity);
                    rb.AddForce(Vector2.right * 22, ForceMode2D.Impulse);
                    DamagePlayerSound();
                }
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
        if (playerHP <= 0)
        {
            isDefeated = true;
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.25f);
        }
    }


    private IEnumerator RightDashCoroutine()
    {
        while (isDashing == true)
        {
            rb.velocity = new Vector2(dashSpeed, 0);
            playerCanMove = false;
            yield return new WaitForSeconds(0.25f);
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
            yield return new WaitForSeconds(0.25f);
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
        if (other.gameObject.CompareTag("Enemy_Shoot") && playerHP > 0)
        {
            triangleShoot = other.gameObject.GetComponent<TriangleShoot>();
            triangleShoot.DestroyingShootAnimation();
            playerHP--;
            DamagePlayerSound();
        }
    }
    private void DamagePlayerSound()
    {
        AudioSource.PlayClipAtPoint(_damagePlayer, Camera.main.transform.position);
        _uiManager.UpdateColor(playerHP);
    }
    public void BullDashSound()
    {
        AudioSource.PlayClipAtPoint(_dashPlayer, Camera.main.transform.position);
    }
    private void CheckAnimal()
    {
        _uiManager.CheckAnimal(animalWithMe);
    }
}