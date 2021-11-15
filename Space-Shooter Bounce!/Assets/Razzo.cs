using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razzo : Nemico
{
    // Start is called before the first frame update
    public override void AutoDistruzione()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
