using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouclierdestruction : MonoBehaviour
{
    public float projpow = 10;
    public float tpsdepow = 5;
    Rigidbody parentrb;
    Transform parentpos;
    PouvoirBouclier parscrpt;
    float t;


    private void Start()
    {
        parentrb = GetComponentInParent<Rigidbody>();
        parentpos = GetComponentInParent<Transform>();
        parscrpt = GetComponentInParent<PouvoirBouclier>();
    }
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        Debug.Log(t);
        if (t >= tpsdepow)
        {
            Destroy(this.gameObject);
            parscrpt.SetFalse();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Piège")
        {
            Vector3 direction = parentpos.position - other.gameObject.transform.position;
            parentrb.AddForce(direction.normalized * projpow);
            Destroy(this.gameObject);
            parscrpt.SetFalse();
        }

    }
}