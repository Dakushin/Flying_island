using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_rot : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 0.1f ;
    float rot;
    public float x;
    public float z;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (rb.constraints == RigidbodyConstraints.None)
        {
            rot += speed;
            rb.rotation = Quaternion.Euler(x , rot, z);
        }
    }

}
