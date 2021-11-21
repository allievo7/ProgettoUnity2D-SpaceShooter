using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProiettileTorre : MonoBehaviour
{
    public float velocità = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.forward * velocità * Time.deltaTime);
    }
}
