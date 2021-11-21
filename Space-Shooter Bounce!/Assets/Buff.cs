using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public bool isEnergia;
    public bool isLife;
    public bool isNave;

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

    void AutoDistruzione()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Starship"))
        {
            Buffami();
            AutoDistruzione();
        }
    }

    public void Buffami()
    {
        if (isEnergia)
        {
            gameManager.AumentaEnergia(10);
        }
        if (isLife)
        {
            gameManager.AumentaLife(1);
        }
    }
}
