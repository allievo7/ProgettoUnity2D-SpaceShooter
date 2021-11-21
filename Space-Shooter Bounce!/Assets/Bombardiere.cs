using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombardiere : Nemico
{
    public EnemyProjectile proiettile;
    public float posizionamentoCD = 2f;
    public VaiQui vaiQui;
    private float distance;
    public AudioSource audios;
    public GameObject[] buffini;
    public bool againstShield = false;

    public float frequenzaDiSparo = 2;
    private float tempoDiSparo;
    
    // Start is called before the first frame update
    public override void AutoDistruzione()
    {
        if (!this.againstShield)
        {
            Instantiate(buffini[Random.Range(0, 3)], transform.position, Quaternion.identity);
            gameManager.AggiungiPunti(10);
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public override void Move()
    {
        if (posizionamentoCD > 0)
        {
            posizionamentoCD -= Time.deltaTime;
            base.Move();
        }
        else if (posizionamentoCD <= 0)
        {
            vaiQui = FindObjectOfType<VaiQui>();
            posizionamentoCD = 0;

            transform.position = Vector2.MoveTowards(transform.position, vaiQui.transform.position, velocità * Time.deltaTime);
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.isDead)
        {
            if (collision.collider.name.Equals("Proiettile(Clone)"))
            {
                enemyLife--;
                gameManager.PlayExplosion();
            }

            if (collision.collider.name.Equals("Starship"))
            {
                enemyLife--;
            }

            if (collision.collider.name.Equals("Scudo"))
            {
                gameManager.DiminuisciEnergia(1);
                enemyLife--;
                this.againstShield = true;
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
            Invoke("AutoDistruzione", 0.5f);
        }
    }

    public override void Attack()
    {
        tempoDiSparo = tempoDiSparo + Time.deltaTime;

        if (tempoDiSparo >= frequenzaDiSparo)
        {
            Instantiate(proiettile, transform.position, Quaternion.identity);
            audios.PlayOneShot(audios.clip);
            tempoDiSparo = 0;
        }
    }
}
