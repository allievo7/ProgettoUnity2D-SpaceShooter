using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnim : MonoBehaviour
{
    public bool isNewGame = false;
    public GameObject vaiQui;
    public float velocit� = 1.2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isNewGame)
        {
            transform.position = Vector2.MoveTowards(transform.position, vaiQui.transform.position, velocit� * Time.deltaTime);
        }

    }
}
