using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateManager : MonoBehaviour
{
    public static stateManager instance;

    private bool mouseActive = true;
    public int nbdSaut;
    public int nbDash;
    public int nbDash2;
    public int nbBouclier;
    public int nbBouclier2;
    public int nbLock;
    public int nbLock2;
    public int nbGrab;
    public int nbGrab2;
    public int level = 0;

    void Awake()
    {
        MakeSingleton();
    }
    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void setMouseActive(bool b)
    {
        mouseActive = b;
    }

    public bool getMouseActive()
    {
        return mouseActive;
    }
}
