using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocità;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        velocità = 10f;

        Invoke("AutoDistruzione", 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * velocità * Time.deltaTime);
    }

    void AutoDistruzione()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    AutoDistruzione();
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Proiettile(Clone)"))
        {
            AutoDistruzione();
        }

        if (collision.collider.name.Equals("Starship"))
        {
            gameManager.DiminuisciLife(1);
            AutoDistruzione();
        }

        if (collision.collider.name.Equals("Scudo"))
        {
            gameManager.DiminuisciEnergia(1);
            AutoDistruzione();
        }

    }
}
