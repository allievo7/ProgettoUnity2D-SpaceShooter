using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrioska : Nemico
{
    public GameObject matrioska2;
    public int matrioskaDanno;
    // Start is called before the first frame update

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Proiettile(Clone)"))
        {
            enemyLife--;
            if (enemyLife <= 0)
            {
                gameManager.AggiungiPunti(2);
                Instantiate(matrioska2, transform.position, Quaternion.identity);
            }
        }

        if (collision.collider.name.Equals("Scudo"))
        {
            gameManager.DiminuisciEnergia(matrioskaDanno);
            AutoDistruzione();
        }

        if (collision.collider.name.Equals("Starship"))
        {
            enemyLife--;
        }
    }

    public override void AutoDistruzione()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
