using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpeciale : MonoBehaviour
{
    public NemicoSpeciale naveSpeciale;
    public BossSetUp boss;
    private GameManager gameManager;
    private Vector3 bossPosition;
    private Quaternion bossRotation;
    public bool callBoss = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        bossPosition = new Vector3(25.77f, 4.73f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (callBoss)
        {
            Invoke("GeneraBoss", 10f);
            callBoss = false;
        }
    }

    public void GeneraSpeciale()
    {
        Instantiate(naveSpeciale, transform.position, naveSpeciale.transform.rotation);
    }

    public void GeneraBoss()
    {
        Instantiate(boss, bossPosition, Quaternion.identity);
    }
}
