using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _playerSpeed;
    private Vector3 targetPos;
    [SerializeField]
    private bool withChicken = false;
    private GameObject animal;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetMouseButton(0))
        {
            checkObject();


        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, _playerSpeed * Time.deltaTime);

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tilemap"))
        {
            targetPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }

    private void checkObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Chicken"))
            {
                Vector2 chickenPos = hit.collider.transform.position;
                chickenPos.y = chickenPos.y+2f;
                targetPos = chickenPos;
                Debug.Log("chicken");
            }
        }
        else if (hit.collider == null)
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = this.gameObject.transform.position.z;
            Debug.Log("nothing");
        }

    }
}
