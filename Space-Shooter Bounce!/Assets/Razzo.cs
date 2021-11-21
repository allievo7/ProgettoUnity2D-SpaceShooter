using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razzo : Nemico
{
    // Start is called before the first frame update
    public override void AutoDistruzione()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.isDead)
        {
            if (collision.collider.name.Equals("Proiettile(Clone)"))
            {
                gameManager.AggiungiPunti(2);
                gameManager.PlayDeathSound();
            }

            if (collision.collider.name.Equals("Starship"))
            {
                gameManager.AggiungiPunti(2);
                gameManager.PlayDeathSound();
            }

            if (collision.collider.name.Equals("Scudo"))
            {
                gameManager.DiminuisciEnergia(1);
                gameManager.PlayShieldHit();
            }

            if (gameManager.ilGiocatoreEVivo())
            {
                Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            }

            animator.SetTrigger("Dead");

            this.isDead = true;
        }
        
        Invoke("AutoDistruzione", 0.5f);
    }
}
