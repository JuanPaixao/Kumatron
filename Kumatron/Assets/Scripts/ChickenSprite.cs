using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSprite : MonoBehaviour
{

    private GameManager gameManager;
    [SerializeField]
    private float chickenSpeed;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		StartCoroutine(ChickenRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator ChickenRoutine()
    {
        while (gameManager.gameOver != true)
            transform.position = Vector2.right * chickenSpeed * Time.deltaTime;
        yield return new WaitForSeconds(3f);
        transform.position = Vector2.left * chickenSpeed * Time.deltaTime;
    }
}
