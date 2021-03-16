using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mur_dead : MonoBehaviour
{
    private Transform tr;
    private float limite;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        limite = tr.position.y - 20;

    }

    // Update is called once per frame
    void Update()
    {
        if (tr.position.y < limite)
        {
            Destroy(gameObject);
        }
    }
}
