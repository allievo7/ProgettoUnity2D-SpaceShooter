
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 movimento;
    public float velocità;

    public float accelerazione;

    public Proiettile proiettile;

    public float frequenzaDiSparo;
    private float tempoDiSparo;

    private GameManager gameManager;
    private bool isBuffed;
    public GameObject navePiùA;
    public GameObject navePiùB;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movimento = Vector2.zero;
        velocità = 12f;
        accelerazione = 90f;
        frequenzaDiSparo = 0.5f;
        gameManager = FindObjectOfType<GameManager>();
        isBuffed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //movimento.y = Input.GetAxisRaw("Vertical");

        //movimento.x = Input.GetAxisRaw("Horizontal");

        movimento.x = 0;
        movimento.y = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movimento.x--;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movimento.x++;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movimento.y++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movimento.y--;
        }

        //Vector2 movimentoInBaseAllaVelocità = movimento * velocità * Time.deltaTime;

        //transform.Translate(movimentoInBaseAllaVelocità);

        Vector3 posizioneRispettoAllaCamera = Camera.main.WorldToViewportPoint(rb2d.position);

        posizioneRispettoAllaCamera.x = Mathf.Clamp01(posizioneRispettoAllaCamera.x);
        posizioneRispettoAllaCamera.y = Mathf.Clamp01(posizioneRispettoAllaCamera.y);

        Vector3 velocitàAttuale = rb2d.velocity;

        if (posizioneRispettoAllaCamera.x == 0 || posizioneRispettoAllaCamera.x == 1)
        {
            velocitàAttuale.x = -velocitàAttuale.x;
        }

        if (posizioneRispettoAllaCamera.y == 0 || posizioneRispettoAllaCamera.y == 1)
        {
            velocitàAttuale.y = -velocitàAttuale.y;
        }

        rb2d.velocity = velocitàAttuale;

        rb2d.position = Camera.main.ViewportToWorldPoint(posizioneRispettoAllaCamera);

        tempoDiSparo = tempoDiSparo + Time.deltaTime;

        if (!isBuffed)
        {
            navePiùA.SetActive(false);
            navePiùB.SetActive(false);
            if (Input.GetKey(KeyCode.Space) && tempoDiSparo > frequenzaDiSparo)
            {
                tempoDiSparo = 0;
                Instantiate(proiettile, transform.position, Quaternion.identity);
            }
        }
        else
        {
            navePiùA.SetActive(true);
            navePiùB.SetActive(true);
            if (Input.GetKey(KeyCode.Space) && tempoDiSparo > frequenzaDiSparo)
            {
                tempoDiSparo = 0;
                Instantiate(proiettile, transform.position, Quaternion.identity);
                Instantiate(proiettile, navePiùA.transform.position, Quaternion.identity);
                Instantiate(proiettile, navePiùB.transform.position, Quaternion.identity);
            }
        }

        if (!gameManager.ilGiocatoreEVivo())
        {
            AutoDistruzione();
        }

    }

    private void FixedUpdate()
    {
        //rb2d.MovePosition(rb2d.position + movimento * velocità * Time.fixedDeltaTime);

        Vector2 forza = movimento * accelerazione * Time.fixedDeltaTime;

        rb2d.AddForce(forza);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Proiettile(Clone)"))
        {

        }
        else if (collision.collider.name.Equals("A(Clone)") || collision.collider.name.Equals("B(Clone)"))
        {
            gameManager.DiminuisciLife(3);
            if (!gameManager.ilGiocatoreEVivo())
            {
                AutoDistruzione();
            }
        }
        else if (collision.collider.name.Equals("Scudo"))
        {

        }
        else if (collision.collider.name.Equals("EnemyProjectile(Clone)"))
        {

        }
        else if (collision.collider.name.Equals("NavePiù(Clone)"))
        {
            if (!isBuffed)
            {
                isBuffed = true;
            }
            else
            {
                gameManager.AggiungiPunti(10);
            }
        }
        else if (collision.collider.name.Equals("LifePiù(Clone)"))
        { 
        }
        else if (collision.collider.name.Equals("EnergiaPiù(Clone)"))
        {
        }
        else
        {
            gameManager.DiminuisciLife(1);
            if (!gameManager.ilGiocatoreEVivo())
            {
                AutoDistruzione();
            }
        }
    }

    void AutoDistruzione()
    {
        isBuffed = false;
        gameObject.SetActive(false);
    }
}
