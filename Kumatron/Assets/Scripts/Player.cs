using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _playerSpeed;
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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        PlayerMovement();
        PlayerAttack();
        MovementAnimationsControl();
    }
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.R) && rayFinished == true)
        {
            _releaseAnimal.ReleasePlayerAnimal();
        }

        if (Input.GetKeyDown(KeyCode.E) && Time.time > _cooldown)
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

        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (animalWithMe == "Chicken")
                playerAnimations[0].AttackAnimationStop_Chicken();
        }
    }

    private void PlayerMovement()
    {
        if (playerCanMove == true)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalmovement = Input.GetAxis("Vertical");
            rb.transform.Translate(new Vector2(horizontalMovement, verticalmovement) * _playerSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tilemap"))
        {
            this.transform.position = (new Vector2(this.transform.position.x, this.transform.position.y));
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
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            playerDirection = "right";
            isMoving = true;
            _animator.SetBool("isMovingRight", true);
            _animator.SetBool("isMovingLeft", false);

        }
        else if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            playerDirection = "left";
            isMoving = true;
            _animator.SetBool("isMovingLeft", true);
            _animator.SetBool("isMovingRight", false);
        }
        else if (!Input.anyKey && isMoving == true && withAnimal == true && animalWithMe == "Bull")
        {
            isMoving = false;
            _animator.SetBool("isMovingRight", false);
            _animator.SetBool("isMovingLeft", false);
            playerAnimations[1].AttackAnimationStop_Bull();
        }
        else if (!Input.anyKey && isMoving == true)
        {
            isMoving = false;
            _animator.SetBool("isMovingRight", false);
            _animator.SetBool("isMovingLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && withAnimal == true && animalWithMe == "Bull" && isMoving == true)
        {
            playerAnimations[1].AttackAnimationPlay_Bull();
            if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.LeftArrow) || playerDirection == "left"))
            {
                _animator.SetBool("isDashingLeft", true);
                Dash();
            }
            else if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.RightArrow) || playerDirection == "right"))
                _animator.SetBool("isDashing", true);
            Dash();
        }


        if (isDashing == false)
        {
            rb.velocity = Vector2.zero;

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


    private IEnumerator RightDashCoroutine()
    {
        while (isDashing == true)
        {
            rb.velocity = new Vector2(dashSpeed, 0);
            yield return new WaitForSeconds(0.2f);
            playerAnimations[1].AttackAnimationStop_Bull();
            _animator.SetBool("isDashing", false);
            isDashing = false;
        }
    }

    private IEnumerator LeftDashCoroutine()
    {
        while (isDashing == true)
        {
            rb.velocity = new Vector2(-dashSpeed, 0);
            yield return new WaitForSeconds(0.2f);
            playerAnimations[1].AttackAnimationStop_Bull();
            _animator.SetBool("isDashingLeft", false);
            isDashing = false;

        }
    }
}