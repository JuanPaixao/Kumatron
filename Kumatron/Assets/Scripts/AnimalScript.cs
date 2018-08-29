using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalScript : MonoBehaviour
{

    [SerializeField]
    private float _animalSpeed;
    private Rigidbody2D _rb;
    [SerializeField]
    private bool _movingRight = false;
    public Transform hitDetection;
    private LayerMask _layerMask = 1 << 10;
    [SerializeField]
    private GameObject playerRay;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
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
        RaycastHit2D hit = Physics2D.Raycast(hitDetection.position, Vector2.zero, _animalSpeed, ~_layerMask);
        Debug.DrawRay(hitDetection.transform.position, Vector2.right, Color.yellow, Mathf.Infinity);

        if (hit.collider == true)
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
}

