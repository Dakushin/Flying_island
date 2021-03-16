using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerendGame : MonoBehaviour
{
    public GameObject screeninterface;
    public GameObject screenEndGame;
    public int level;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            if(stateManager.instance.level < level)
            {
                stateManager.instance.level = level;
            }
            screeninterface.SetActive(false);
            screenEndGame.SetActive(true);
        }
    }
}
