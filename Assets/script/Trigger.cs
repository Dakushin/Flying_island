using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject loder;
    public int nbdash;
    public int nbBouclier;
    public int nbgrab;
    public int nblock;
    public int nbscene;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("test");
            stateManager.instance.nbdSaut = 20;
            stateManager.instance.nbDash = nbdash;
            stateManager.instance.nbDash2 = nbdash;
            stateManager.instance.nbGrab = nbgrab;
            stateManager.instance.nbGrab2 = nbgrab;
            stateManager.instance.nbLock = nblock;
            stateManager.instance.nbLock2 = nblock;
            stateManager.instance.nbBouclier = nbBouclier;
            stateManager.instance.nbBouclier2 = nbBouclier;
            loder.gameObject.GetComponent<loaderscenehub>().LoadLevel(nbscene);
        }
    }
}
