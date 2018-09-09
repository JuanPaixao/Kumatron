using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Egg : MonoBehaviour
{
    private Tilemap _tilemapGround;
    Vector3 hitPosition = Vector3.zero;
    [SerializeField]
    private GameObject _explosion;


    void Awake()
    {
        _tilemapGround = GameObject.Find("Tilemap_Ground").GetComponent<Tilemap>();
        hitPosition = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tilemap") || other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.name == "Tilemap_Ground" || other.gameObject.name == "Enemy")
            {
                foreach (ContactPoint2D hit in other.contacts)
                {
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                    _tilemapGround.SetTile(_tilemapGround.WorldToCell(hitPosition), null);
                    Instantiate(_explosion, this.transform.position, Quaternion.identity);
                    Destroy(this.gameObject, 0.05f);
                }
            }
            else
            {
                Destroy(this.gameObject, 0.35f);
            }
        }
    }
}
