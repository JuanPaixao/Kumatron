using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _playerSpeed, _auxSpeed, _cowSpeed, horizontalMovement, verticalMovement, actualMovementSpeed;
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
    [SerializeField]
    private AbduptionRange abduptionRange;
    public bool isDashing, isMoving, isDefeated;
    public int playerHP;
    [SerializeField]
    private GameObject _explosion, _menuExplosion;
    [SerializeField]
    private AudioClip _damagePlayer, _dashPlayer;
    private UIManager _uiManager;
    private GameManager _gameManager;
    public Animator animator;
    public float rotation;
    private Vector2 _lastFramePos;

    public float dashSpeed, startDashTime;
    private float _dashTime;
    public int dashingValue;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        _auxSpeed = _playerSpeed;
        _cowSpeed = _playerSpeed * 1.8f;
        isDefeated = false;
        _dashTime = startDashTime;

    }
    void Update()
    {
        CalculateSpeed();
        PlayerAttack();
        PlayerMovement();
        animator.SetFloat("Horizontal", horizontalMovement);
        animator.SetFloat("Vertical", verticalMovement);
        animator.SetBool("isMoving", isMoving);
        _dashTime += Time.deltaTime;



#if UNITY_ANDROID

        AndroidAnimationsControl();
        if (CrossPlatformInputManager.GetButtonDown("PauseButton"))
        {
            _gameManager.PauseGame();
        }
#else
        MovementAnimationsControl();
#endif


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
    private void PlayerAttack()
    {
#if UNITY_ANDROID
        if (CrossPlatformInputManager.GetButtonDown("RayButton") && withAnimal == true && rayFinished == true)
        {
            _releaseAnimal.ReleasePlayerAnimal();
            _uiManager.CheckAnimal(animalWithMe);
            //left right down
  
        }
        if (CrossPlatformInputManager.GetButtonDown("XButton") && Time.time > _cooldown)
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
#else
        if (Input.GetKeyDown(KeyCode.Space) && withAnimal == true && rayFinished == true)
        {
            _releaseAnimal.ReleasePlayerAnimal();
            _uiManager.CheckAnimal(animalWithMe);
            //moving left down e right
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _cooldown)
        {
            _releaseAnimal.Attack();
            _cooldown = Time.time + _nextTime;

            if (animalWithMe == "Chicken")
            {
                playerAnimations[0].AttackAnimationPlay_Chicken();
            }
        }

        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (animalWithMe == "Chicken")
                playerAnimations[0].AttackAnimationStop_Chicken();
        }
#endif
    }

    private void PlayerMovement()
    {
        if (playerCanMove == true)
        {
            rb.velocity = Vector2.zero;
            horizontalMovement = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            verticalMovement = CrossPlatformInputManager.GetAxisRaw("Vertical");
            Vector2 movement = new Vector2(horizontalMovement, verticalMovement);
            transform.Translate(movement.normalized * _playerSpeed * Time.deltaTime);
            rb.velocity = movement.normalized * _playerSpeed * Time.deltaTime;

            if (horizontalMovement > 0.01f)
            {
                playerDirection = "right";
            }
            else if (horizontalMovement < -0.01f)
            {
                playerDirection = "left";
            }
            else
            {
                playerDirection = " ";
            }
            // rb.MovePosition(rb.position + movement.normalized * _playerSpeed * Time.fixedDeltaTime);
        }
        if (animalWithMe == "Cow")
        {
            _playerSpeed = _cowSpeed;
            animator.SetBool("withCow", true);
        }
        else
        {
            _playerSpeed = _auxSpeed;
            animator.SetBool("withCow", false);
        }
        if (animalWithMe == "Bull")
        {
            animator.SetBool("withBull", true);
        }
        else
        {
            animator.SetBool("withBull", false);
            animator.SetFloat("dashState", 0);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tilemap"))
        {
            transform.position = (new Vector2(this.transform.position.x, this.transform.position.y));
        }
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.name == "SquareEnemy" && rayFinished == true && isDashing == false)
        {
            SquareEnemy squareEnemy = other.gameObject.GetComponent<SquareEnemy>();
            if (squareEnemy != null && squareEnemy.enemyDashing == true && playerHP > 0)
            {
                if (withAnimal == true && rayFinished == true)
                {
                    if (other.gameObject.transform.position.x >= this.gameObject.transform.position.x && withAnimal == true && rayFinished == true)
                    {
                        _releaseAnimal.ReleasePlayerAnimal();
                        playerCanMove = false;
                        Instantiate(_explosion, this.transform.position, Quaternion.identity);
                        rb.AddForce(Vector2.left * 22, ForceMode2D.Impulse);
                        DamagePlayerSound();
                    }
                    else if (other.gameObject.transform.position.x < this.gameObject.transform.position.x && withAnimal == true && rayFinished == true)
                    {
                        _releaseAnimal.ReleasePlayerAnimal();
                        playerCanMove = false;
                        Instantiate(_explosion, this.transform.position, Quaternion.identity);
                        rb.AddForce(Vector2.right * 22, ForceMode2D.Impulse);
                        DamagePlayerSound();
                    }
                }

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
            isMoving = false;

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

        //bull attack

        if (Input.GetKeyDown(KeyCode.Mouse0) && animalWithMe == "Bull" && playerCanMove == true)
        {
            if (_dashTime > startDashTime)
            {
                if (Input.GetKeyDown(KeyCode.A) || playerDirection == "left" && playerCanMove == true)
                {
                    Dash();
                }
                else if (Input.GetKeyDown(KeyCode.D) || playerDirection == "right" && playerCanMove == true)
                    Dash();
            }
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
                animator.SetBool("isDashingRight", true);
                playerAnimations[1].AttackAnimationPlay_Bull();
                StartCoroutine(RightDashCoroutine());
            }
            else if (playerDirection == "left")
            {
                isDashing = true;
                animator.SetBool("isDashingLeft", true);
                playerAnimations[1].AttackAnimationPlay_Bull();
                StartCoroutine(LeftDashCoroutine());
            }
        }
    }

    private void PlayerDefeated()
    {
        if (playerHP <= 0)
        {
            isDefeated = true;
            Instantiate(_menuExplosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.25f);
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
        if (other.gameObject.CompareTag("Enemy_Shoot") && withAnimal == true && rayFinished == true)
        {
            triangleShoot = other.gameObject.GetComponent<TriangleShoot>();
            triangleShoot.DestroyingShootAnimation();
            _releaseAnimal.ReleasePlayerAnimal();
            DamagePlayerSound();
        }
        else if (other.gameObject.CompareTag("Enemy_Shoot") && playerHP > 0 && withAnimal == false)
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
    private void CalculateSpeed()
    {
        float movementPerFrame = Vector3.Distance(this.transform.position, _lastFramePos);
        actualMovementSpeed = movementPerFrame / Time.deltaTime;
        _lastFramePos = transform.position;
        if (playerCanMove)
        {
            isMoving = actualMovementSpeed > 0 ? true : false;
        }
    }
    private IEnumerator RightDashCoroutine()
    {
        while (isDashing == true)
        {
            dashingValue = 1;
            rb.velocity = new Vector2(dashSpeed, 0);
            playerCanMove = false;
            yield return new WaitForSeconds(0.3f);
            playerAnimations[1].AttackAnimationStop_Bull();
            isDashing = false;
            rb.velocity = Vector2.zero;
            playerCanMove = true;
            animator.SetBool("isDashingRight", false);
            _dashTime = 0;
            dashingValue = 0;
        }
    }

    private IEnumerator LeftDashCoroutine()
    {
        while (isDashing == true)
        {
            dashingValue = 1;
            rb.velocity = new Vector2(-dashSpeed, 0);
            playerCanMove = false;
            yield return new WaitForSeconds(0.3f);
            playerAnimations[1].AttackAnimationStop_Bull();
            isDashing = false;
            rb.velocity = Vector2.zero;
            playerCanMove = true;
            animator.SetBool("isDashingLeft", false);
            _dashTime = 0;
            dashingValue = 0;
        }
    }
}