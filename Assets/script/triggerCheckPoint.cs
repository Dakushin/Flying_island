using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class triggerCheckPoint : MonoBehaviour
{
    public GameObject active;
    public GameObject spawn;
    private bool newspawn = false;
    public Image Dash;
    public Text inbdash;
    public Image bouclier;
    public Text inbbouclier;
    public Image grab;
    public Text inbgrab;
    public Image Lock;
    public Text inbLock;
    private int nbTrigger = 0;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && nbTrigger ==0)
        {
            nbTrigger++;
            MortTrigger();
            print("Checkpoint_called");
            Instantiate(active, transform.position,transform.rotation);
            spawn.GetComponent<Transform>().SetPositionAndRotation(transform.position, transform.rotation);
            stateManager.instance.nbDash = stateManager.instance.nbDash2;
            Dash.fillAmount = 1;
            inbdash.text = stateManager.instance.nbDash2.ToString();
            stateManager.instance.nbBouclier = stateManager.instance.nbBouclier2;
            bouclier.fillAmount = 1;
            inbbouclier.text = stateManager.instance.nbBouclier2.ToString();
            stateManager.instance.nbGrab = stateManager.instance.nbGrab2;
            grab.fillAmount = 1;
            inbgrab.text = stateManager.instance.nbGrab2.ToString();
            stateManager.instance.nbLock = stateManager.instance.nbLock2;
            Lock.fillAmount = 1;
            inbLock.text = stateManager.instance.nbLock2.ToString();
        }
    }
    public void MortTrigger()
    {
        print("Destroy_called");
        Destroy(gameObject,0.1f);
    }
}

