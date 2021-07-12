using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float destroyTime = 2;
    private float temp;

    // Start is called before the first frame update
    void Start()
    {
        temp = destroyTime;
    }

    // Update is called once per frame
    void Update()
    {
        temp -= Time.deltaTime;
        if (temp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
