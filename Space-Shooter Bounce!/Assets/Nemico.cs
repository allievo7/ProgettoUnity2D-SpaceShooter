using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nemico : MonoBehaviour
{
    [SerializeField]public float velocit‡;
    [SerializeField] public float accelerazione;
    [SerializeField]public Vector2 direzione;
    protected Rigidbody2D rigid;
    [SerializeField]public GameManager gameManager;
    [SerializeField] public float enemyLife;
    // Start is called before the first frame update
    void Start()
    {
        //velocit‡ = 2f;
        //accelerazione = 50f;
        rigid = GetComponent<Rigidbody2D>();
        //Invoke("AutoDistruzione", 10);
        direzione = Vector2.zero;
        direzione.y = -1;
        direzione.x = 0;

        gameManager = FindObjectOfType<GameManager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(6, 6);
        Vector3 posizioneCamera = Camera.main.WorldToViewportPoint(rigid.position);

        posizioneCamera.x = Mathf.Clamp01(posizioneCamera.x);
        posizioneCamera.y = Mathf.Clamp01(posizioneCamera.y);

        Vector3 velocit‡NemicoAtt = rigid.velocity;

        if (posizioneCamera.x == 0 || posizioneCamera.x == 1)
        {
            velocit‡NemicoAtt.x = -velocit‡NemicoAtt.x;
        }

        if (posizioneCamera.y == 0 || posizioneCamera.y == 1)
        {
            velocit‡NemicoAtt.y = -velocit‡NemicoAtt.y;
        }

        rigid.velocity = velocit‡NemicoAtt;

        rigid.position = Camera.main.ViewportToWorldPoint(posizioneCamera);

        Attack();

        if (enemyLife <= 0)
        {
            AutoDistruzione();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void AutoDistruzione()
    {
        gameManager.DiminuisciNemici();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Proiettile(Clone)"))
        {
            enemyLife--;
            gameManager.AggiungiPunti(2);
        }

        if (collision.collider.name.Equals("Starship"))
        {
            enemyLife--;
            gameManager.AggiungiPunti(2);
        }

        if (collision.collider.name.Equals("Scudo"))
        {
            gameManager.DiminuisciEnergia(1);
            AutoDistruzione();
        }
    }

    public virtual void Move()
    {
        Vector2 forza = direzione * accelerazione * Time.fixedDeltaTime;

        rigid.AddForce(forza);
    }

    public virtual void Attack()
    {

    }
}
