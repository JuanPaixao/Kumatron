using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryScript : MonoBehaviour
{

    [SerializeField]
    private int _factoryHP;
    [SerializeField]
    private GameObject _explosion;
    private Player _player;
    private GameManager _gameManager;
    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    // Update is called once per frame
    void Update()
    {
        if (_factoryHP <= 0)
        {
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Egg"))
        {
            _factoryHP--;
        }
        else if (_player != null & other.gameObject.CompareTag("Player") && _player.isDashing)
        {
            _factoryHP -= 2;
        }
    }
    private void OnDestroy()
    {
        _gameManager.FactoryDestroyed();
    }
}
