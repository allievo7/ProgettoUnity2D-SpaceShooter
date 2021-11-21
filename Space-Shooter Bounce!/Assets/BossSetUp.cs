using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSetUp : MonoBehaviour
{
    private GameObject ancora;
    public float velocità;
    public AudioSource audios;
    public AudioClip bossArrive;
    public AudioClip explo;
    public int torrette;
    public int bossLife;
    public float spawnCD = 0.5f;
    public GameObject[] spawnersPhase2;
    public Nemico minion;
    private GameManager gameManager;
    public GameObject exploJuke;

    public enum Phases
    {
        MOVING,
        PHASE1,
        PHASE2,
        DEATH
    }

    public Phases phases;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ancora = GameObject.FindGameObjectWithTag("Anchor");
        this.transform.Rotate(0, 0, 90);
        audios.PlayOneShot(bossArrive);
        phases = Phases.MOVING;
        bossLife = 70;
        
    }

    // Update is called once per frame
    void Update()
    {
        torrette = GameObject.FindObjectsOfType<Torretta>().Length;
        Physics2D.IgnoreLayerCollision(6, 6);
        switch (phases)
        {
            case Phases.MOVING:
                transform.position = Vector2.MoveTowards(transform.position, ancora.transform.position, velocità * Time.deltaTime);
                if (transform.position == ancora.transform.position)
                {
                    phases = Phases.PHASE1;
                }
                break;
            case Phases.PHASE1:
                if (torrette == 0)
                {
                    phases = Phases.PHASE2;
                }
                break;
            case Phases.PHASE2:
                spawnCD -= Time.deltaTime;
                if (spawnCD <= 0)
                {
                    Instantiate(minion, spawnersPhase2[Random.Range(0, 6)].transform.position, Quaternion.identity);
                    spawnCD = 2;
                }
                if (bossLife <= 0)
                {
                    phases = Phases.DEATH;
                }
                break;
            case Phases.DEATH:
                exploJuke.SetActive(true);
                ancora.transform.position = new Vector3(10.18f, 13.21f, 0f);
                transform.position = Vector2.MoveTowards(transform.position, ancora.transform.position, velocità * Time.deltaTime);
                if (transform.position == ancora.transform.position)
                {
                    gameManager.GameOver();
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Proiettile(Clone)") && phases == BossSetUp.Phases.PHASE2)
        {
            bossLife = bossLife - 1;
        }
    }

    public void PlayExplosion()
    {
        audios.PlayOneShot(explo);
    }
}
