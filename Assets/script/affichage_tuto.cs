using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_tuto : MonoBehaviour
{
    public GameObject panelTuto;
    public void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            panelTuto.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            panelTuto.SetActive(false);
        }
    }
}
