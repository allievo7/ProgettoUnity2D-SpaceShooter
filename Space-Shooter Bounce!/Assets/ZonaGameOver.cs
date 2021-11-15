using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaGameOver : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Proiettile(Clone)"))
        {

        }
        if (collision.collider.name.Equals("Starship"))
        {

        }
        else
        {
            gameManager.GameOver();
        }
    }
}
