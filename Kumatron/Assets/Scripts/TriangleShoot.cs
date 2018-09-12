using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleShoot : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Translate(Vector2.right * -_speed * Time.deltaTime);
    }
    public void DestroyingShoot()
    {
        Destroy(this.gameObject);
    }
    public void DestroyingShootAnimation()
    {
        _speed = 0;
        _animator.SetBool("isDestroying", true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tilemap"))
        {
            DestroyingShootAnimation();
        }
    }
}
