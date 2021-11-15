using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpeciale : MonoBehaviour
{
    public NemicoSpeciale naveSpeciale;
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

    public void GeneraSpeciale()
    {
        Instantiate(naveSpeciale, transform.position, Quaternion.identity);
    }
}
