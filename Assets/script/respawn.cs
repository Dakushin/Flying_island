using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class respawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject spawn;
    private float limite;
    public Image rouge;
    public Text inbdash;
    public Image bouclier;
    public Text inbbouclier;
    public Image grab;
    public Text inbgrab;
    public Image Lock;
    public Text inbLock;
    void Start()
    {
        limite = player.GetComponent<Transform>().position.y - 20;
    }
    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Transform>().position.y < limite)
        {
            stateManager.instance.nbDash = stateManager.instance.nbDash2;
            rouge.fillAmount = 1;
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
            player.GetComponent<Transform>().SetPositionAndRotation(new Vector3(spawn.GetComponent<Transform>().position.x, spawn.GetComponent<Transform>().position.y + 5, spawn.GetComponent<Transform>().position.z), Quaternion.identity);
        }
    }
}
