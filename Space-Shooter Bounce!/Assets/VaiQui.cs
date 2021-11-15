using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaiQui : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private float moveCD;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveCD -= Time.deltaTime;

        if (moveCD <= 0)
        {
            transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            moveCD = 3;
        }
    }
}
