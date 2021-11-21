using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrioska : Nemico
{
    public GameObject matrioska2;
    public int matrioskaDanno;
    public bool againstShield = false;
    // Start is called before the first frame update

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.isDead)
        {
            if (collision.collider.name.Equals("Proiettile(Clone)"))
            {
                enemyLife--;
                gameManager.PlayExplosion();
            }

            if (collision.collider.name.Equals("Scudo"))
            {
                this.againstShield = true;
                gameManager.DiminuisciEnergia(matrioskaDanno);
                enemyLife = 0;
            }

            if (collision.collider.name.Equals("Starship"))
            {
                enemyLife--;
            }

            if (enemyLife <= 0)
            {
                this.isDead = true;
            }
        }

        if (enemyLife <= 0)
        {
            if (gameManager.ilGiocatoreEVivo())
            {
                Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            }

            animator.SetTrigger("Dead");
            Invoke("AutoDistruzione", 0.35f);
        }
    }

    public override void AutoDistruzione()
    {
        if (!this.againstShield)
        {
            gameManager.AggiungiPunti(2);
            Instantiate(matrioska2, transform.position, Quaternion.identity);
        }
        gameManager.PlayDeathSound();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
