using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroide : NemicoSpeciale
{
    public bool isFromA;
    // Start is called before the first frame update
    public override void AutoDistruzione()
    {
        gameManager.DiminuisciAsteroide();
        base.AutoDistruzione();
    }

    public override void Move()
    {
        if (isFromA)
        {
            transform.Translate(Vector2.right * 2 * Time.deltaTime);
        }
        else
        {
            base.Move();
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
