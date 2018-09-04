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
    private float _shootFixedCooldown = 1.00f;
    private float _eggCooldown = 0f;
    private Rigidbody2D _rb;
    [SerializeField]
    private float _dashSpeed;
    private float _dashTime;
    [SerializeField]
    private float _startDashTime;
    [SerializeField]
    private AbduptionRange abduptionRange;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _dashTime = _startDashTime;

    }
    void Update()
    {
        PlayerMovement();
        PlayerAttack();
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dash();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _rb.velocity = _rb.velocity = Vector2.zero;
        }
    }
    private void PlayerMovement()
    {
        if (playerCanMove == true)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalmovement = Input.GetAxis("Vertical");
            _rb.transform.Translate(new Vector2(horizontalMovement, verticalmovement) * _playerSpeed * Time.deltaTime);
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

    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.R) && rayFinished == true)
        {
            _releaseAnimal.ReleasePlayerAnimal();
        }

        if (Input.GetKeyDown(KeyCode.E) && Time.time > _eggCooldown)
        {
            _releaseAnimal.Attack();
            _eggCooldown = _shootFixedCooldown + _eggCooldown;
            if (animalWithMe == "Chicken" || animalWithMe == "Chicken_Collision")
                playerAnimations[0].AttackAnimationPlay_Chicken();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (animalWithMe == "Chicken" || animalWithMe == "Chicken_Collision")
                playerAnimations[0].AttackAnimationStop_Chicken();
        }

    }
    public void Dash()
    {
        if (_dashTime <= 0)
        {
            playerDirection = "";
        }
        else
        {
            _dashTime -= Time.deltaTime;

            if (playerDirection == "right")
            {

            }
            else if (playerDirection == "left")
            {

            }
        }
    }

    private IEnumerator AnimalPowerUp(int animal)
    {
        yield return new WaitForSeconds(1);
        animalPowerUp[animal].SetActive(true);
    }
}

