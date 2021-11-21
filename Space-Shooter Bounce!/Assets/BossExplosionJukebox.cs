using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosionJukebox : MonoBehaviour
{
    public BossSetUp bossBody;
    public AudioClip explo;
    public AudioSource audios;
    private float CD = 5;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlayExplosion", 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        CD -= Time.deltaTime;
        if (CD <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void PlayExplosion()
    {
        audios.PlayOneShot(explo);
    }
}
