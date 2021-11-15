using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAsteroidi : MonoBehaviour
{
    public GameObject spwnPA;
    public GameObject spwnPB;
    public Asteroide asteroideA;
    public Asteroide asteroideB;
    private GameManager gameManager;
    private int spawnerCasuale;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InvokeRepeating("GeneraAsteroide", 2, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GeneraAsteroide()
    {
        if (gameManager.PossoMandareAsteroide())
        {
            spawnerCasuale = Random.Range(1, 3);

            if (spawnerCasuale == 1)
            {
                Vector2 posizioneSprite = spwnPA.transform.position;
                SpriteRenderer renderer = spwnPA.GetComponent<SpriteRenderer>();
                float lunghezzaCreatore = renderer.bounds.size.y;
                float maxY = lunghezzaCreatore / 2;
                float minY = -maxY;
                float yLancio = Random.Range(minY, maxY);
                Vector2 posizioneDiLancio = Vector2.zero;
                posizioneDiLancio.y = yLancio;
                posizioneDiLancio.x = posizioneSprite.x;
                Instantiate(asteroideA, posizioneDiLancio, Quaternion.identity);
                gameManager.AumentaAsteroide();
            }
            else
            {
                Vector2 posizioneSprite = spwnPB.transform.position;
                SpriteRenderer renderer = spwnPB.GetComponent<SpriteRenderer>();
                float lunghezzaCreatore = renderer.bounds.size.y;
                float maxY = lunghezzaCreatore / 2;
                float minY = -maxY;
                float yLancio = Random.Range(minY, maxY);
                Vector2 posizioneDiLancio = Vector2.zero;
                posizioneDiLancio.y = yLancio;
                posizioneDiLancio.x = posizioneSprite.x;
                Instantiate(asteroideB, posizioneDiLancio, Quaternion.identity);
                gameManager.AumentaAsteroide();
            }
        }
    }
}
