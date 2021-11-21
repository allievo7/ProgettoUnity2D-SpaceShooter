using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torretta : MonoBehaviour
{
    private Transform target;
    public GameObject aProiettile;
    public GameObject bProiettile;
    public Transform aFire;
    public Transform bFire;
    public AudioSource audios;
    public AudioClip shot;
    public float aFireCD = 5;
    public float bFireCD;
    public float setCD;

    public int TorrettaVita = 10;

    public BossSetUp bossBody;

    //public float velocit‡Proiettile = 20;
    // Start is called before the first frame update
    void Start()
    {
        bossBody = FindObjectOfType<BossSetUp>();
        target = GameObject.Find("Starship").transform;
        //InvokeRepeating("AttackA", 4, 6f);
        //InvokeRepeating("AttackB", 5, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(6, 6);

        if (bossBody.phases == BossSetUp.Phases.MOVING)
        {
            RotateTowardsTarget();
        }
        else if (bossBody.phases == BossSetUp.Phases.PHASE1)
        {
            RotateTowardsTarget();
            AttackSequenza();
        }

        if (TorrettaVita <= 0)
        {
            bossBody.PlayExplosion();
            Destroy(gameObject);
        }
    }

    private void RotateTowardsTarget()
    {
        var offset = 90f;
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    public void AttackSequenza()
    {
        if (aFireCD > 0)
        {
            aFireCD -= Time.deltaTime;
            if (aFireCD <= 0)
            {
                AttackA();
                bFireCD = setCD;
            }
        }

        if (bFireCD > 0)
        {
            bFireCD -= Time.deltaTime;
            if (bFireCD <= 0)
            {
                AttackB();
                aFireCD = setCD;
            }
        }
    }
    public void AttackA()
    {
        audios.PlayOneShot(shot);
        Instantiate(aProiettile, aFire.position, aFire.rotation);
    }

    public void AttackB()
    {
        audios.PlayOneShot(shot);
        Instantiate(bProiettile, bFire.position, bFire.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Proiettile(Clone)") && bossBody.phases != BossSetUp.Phases.MOVING)
        {
            TorrettaVita = TorrettaVita - 1;
        }
    }
}
