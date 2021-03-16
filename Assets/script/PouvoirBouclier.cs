using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PouvoirBouclier : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bouclier;
    bool activer = false;
    float proportion_Bouclier;
    public Image Bouclier;
    public Text inbbouclier;
    // Update is called once per frame

    void Start()
    {
        proportion_Bouclier = 1.0f / stateManager.instance.nbBouclier;
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            inbbouclier.text = stateManager.instance.nbBouclier.ToString();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !activer && stateManager.instance.nbBouclier > 0)
        {
            Bouclier.fillAmount -= proportion_Bouclier;
            stateManager.instance.nbBouclier--;
            inbbouclier.text = stateManager.instance.nbBouclier.ToString();
            Instantiate(bouclier, this.transform);
            activer = true;
        }        
    }

    public void SetFalse()
    {
        activer = false;
    }
    public bool GetIsShielding()
    {
        return activer;
    }
}
