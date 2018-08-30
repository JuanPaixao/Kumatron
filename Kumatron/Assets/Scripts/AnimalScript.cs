using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalScript : MonoBehaviour
{

    [SerializeField]
    private float _animalSpeed, _forceJump;
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
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private RaycastHit2D _hit, _hitUp, _hitDown, _hitCheckHeight;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = new Vector2(-_animalSpeed * Time.fixedDeltaTime, 0);
        CheckJumpCollision();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        playerRay = other.gameObject.GetComponent<GameObject>();
        if (other.CompareTag("Ray"))
        {
            StartCoroutine(AnimalAbducted());
        }
    }

    private IEnumerator AnimalAbducted()
    {
        while (this.gameObject != null)
        {
            _animalSpeed = 10f;
            _spriteRenderer.color -= new Color(0f, 0f, 0f, 0.025f);
            yield return new WaitForSeconds(1);
            this.gameObject.SetActive(false);
        }
    }
    private void CheckJumpCollision()
    {   //turn
        _hit = Physics2D.Raycast(hitDetection.position, Vector2.zero, _animalSpeed, ~_layerMask);
        Debug.DrawRay(hitDetection.transform.position, Vector2.right, Color.yellow, Mathf.Infinity);
        //check if theres free space to jump
        _hitUp = Physics2D.Raycast(hitDetectionUpLevel.position, Vector2.zero, _animalSpeed, ~_layerMask);
        Debug.DrawRay(hitDetectionUpLevel.transform.position, Vector2.right, Color.yellow, Mathf.Infinity);
        //check if i conclude my jump
        _hitCheckHeight = Physics2D.Raycast(hitHeightCheckLevel.position, Vector2.zero, _animalSpeed, ~_layerMask);
        Debug.DrawRay(hitHeightCheckLevel.transform.position, Vector2.zero, Color.yellow, Mathf.Infinity);
        //down
        _hitDown = Physics2D.Raycast(hitDetectionDownLevel.position, Vector2.zero, _animalSpeed, ~_layerMask);
        Debug.DrawRay(hitDetectionDownLevel.transform.position, Vector2.zero, Color.yellow, Mathf.Infinity);



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
                _rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                _movingRight = false;
                transform.eulerAngles = new Vector2(0, 0);
                _animalSpeed = -_animalSpeed;
            }
            else
            {
                _rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                _movingRight = true;
                transform.eulerAngles = new Vector2(0, 180);
                _animalSpeed = -_animalSpeed;
            }
        }
    }
}

