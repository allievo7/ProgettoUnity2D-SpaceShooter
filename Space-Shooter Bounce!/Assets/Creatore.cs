using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creatore : MonoBehaviour
{
    public Nemico[] nemico;

    public float tempoDiCreazione;
    private GameManager gameManager;
    private float nemicoForteCD;

    // Start is called before the first frame update
    void Start()
    {
        nemicoForteCD = 20;
        gameManager = FindObjectOfType<GameManager>();
        tempoDiCreazione = 1f;
        InvokeRepeating("GeneraNemico", 2, tempoDiCreazione);   
    }

    // Update is called once per frame
    void Update()
    {
        nemicoForteCD -= Time.deltaTime;
    }

    void GeneraNemico()
    {
        Vector2 posizioneSprite = transform.position;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float larghezzaCreatore = renderer.bounds.size.x;
        float maxX = larghezzaCreatore / 2;
        float minX = -maxX;
        float xLancio = Random.Range(minX, maxX);
        Vector2 posizioneDiLancio = Vector2.zero;
        posizioneDiLancio.x = xLancio;
        posizioneDiLancio.y = posizioneSprite.y;

        if (gameManager.PossoMandareUnNuovoNemico())
        {
            Instantiate(nemico[0], posizioneDiLancio, Quaternion.identity);
            gameManager.AumentaNemici();
        }

        if (nemicoForteCD <= 0)
        {
            Instantiate(nemico[Random.Range(1,3)], posizioneDiLancio, Quaternion.identity);
            nemicoForteCD = 20;
        }
    }
}
