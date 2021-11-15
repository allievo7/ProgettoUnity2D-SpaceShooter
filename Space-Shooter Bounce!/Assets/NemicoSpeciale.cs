using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemicoSpeciale : MonoBehaviour
{
    [SerializeField]public GameManager gameManager;
    public float naveLife;
    public float tempoDiVita;
    public GameObject[] buffini;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        tempoDiVita = 15;
    }

    // Update is called once per frame
    void Update()
    {
        tempoDiVita -= Time.deltaTime;

        if (tempoDiVita <= 0)
        {
            AutoDistruzione();
        }
        Move();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Proiettile(Clone)"))
        {
            naveLife--;
            if (naveLife <= 0)
            {
                Instantiate(buffini[Random.Range(0, 3)], transform.position, Quaternion.identity);
                gameManager.AggiungiPunti(100);
                AutoDistruzione();
            }
            
        }

        if (collision.collider.name.Equals("Nemico(Clone)") || collision.collider.name.Equals("Matrioska(Clone)") || collision.collider.name.Equals("MatrioskaII(Clone)"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    public virtual void AutoDistruzione()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public virtual void Move()
    {
        transform.Translate(Vector2.left * 2 * Time.deltaTime);
    }
}
