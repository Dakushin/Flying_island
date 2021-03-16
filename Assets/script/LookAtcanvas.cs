using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtcanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public GameObject platasuivre = null;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(cam.transform.position);
        gameObject.transform.position = platasuivre.transform.position;
    }

}
