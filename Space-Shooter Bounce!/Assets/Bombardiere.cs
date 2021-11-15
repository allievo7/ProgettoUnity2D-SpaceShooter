using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombardiere : Nemico
{
    public EnemyProjectile proiettile;
    public float posizionamentoCD = 2f;
    public VaiQui vaiQui;
    private float distance;
    public GameObject[] buffini;

    public float frequenzaDiSparo = 2;
    private float tempoDiSparo;
    
    // Start is called before the first frame update
    public override void AutoDistruzione()
    {
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
        if (collision.collider.name.Equals("Proiettile(Clone)"))
        {
            enemyLife--;
            if (enemyLife <= 0)
            {
                Instantiate(buffini[Random.Range(0, 3)], transform.position, Quaternion.identity);
                gameManager.AggiungiPunti(10);
            }
        }

        if (collision.collider.name.Equals("Starship"))
        {
            enemyLife--;
            if (enemyLife <= 0)
            {
                gameManager.AggiungiPunti(10);
            }
        }

        if (collision.collider.name.Equals("Scudo"))
        {
            gameManager.DiminuisciEnergia(1);
            enemyLife--;
            if (enemyLife <= 0)
            {
                AutoDistruzione();
            }
        }
    }

    public override void Attack()
    {
        tempoDiSparo = tempoDiSparo + Time.deltaTime;

        if (tempoDiSparo >= frequenzaDiSparo)
        {
            Instantiate(proiettile, transform.position, Quaternion.identity);
            tempoDiSparo = 0;
        }
    }
}
