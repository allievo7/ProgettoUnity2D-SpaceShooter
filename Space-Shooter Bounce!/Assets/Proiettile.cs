using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proiettile : MonoBehaviour
{
    public float velocità;
    // Start is called before the first frame update
    void Start()
    {
        velocità = 25f;

        Invoke("AutoDistruzione", 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * velocità * Time.deltaTime);
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

        }
        else
        {
            AutoDistruzione();
        }
    }
}
