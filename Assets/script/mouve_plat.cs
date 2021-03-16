using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouve_plat : MonoBehaviour
{
    public Transform pos1, pos2;// point 1 et point 2
    public Transform startPos; // qu'elle point la platforme vas en permiere
    Rigidbody rb;
    public float speed = 0.1f;
    Vector3 nextPos;
    BoxCollider col;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        nextPos = startPos.position;//platforme se deplace vers le point du depart
    }

    void Update()
    {        




        if (transform.position == pos1.position)// si platform et arrive au point 1 
        {
            nextPos = pos2.position;// alors doit se dirige vers le point 2
        }

        if (transform.position == pos2.position)// si platform et arrive au point 2
        {
            nextPos = pos1.position;// alors doit se dirige vers le point 1
        }

        if (rb.constraints == RigidbodyConstraints.None)
        {
            rb.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);// defini la vite de deplacement de la platforme

        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("bonsoir");
        if (other.gameObject.tag == "Player")
        {

            Debug.Log("toi");
            other.transform.parent = this.transform.parent;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    other.GetComponent<Rigidbody>().velocity = (rb.velocity + other.GetComponent<Rigidbody>().velocity) / 2;
    //}



    // private void OnDrawGizmos()// appele l'image de la platforme pour la repositione n'est
    // {
    //     Gizmos.DrawLine(pos1.position, pos2.position);
    // }
}
