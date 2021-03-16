using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class test_ui : MonoBehaviour
{
    public Image rouge;
    public Image bleu;
    public Image rose;
    public Image vert;
    float proportion_rouge;
    float proportion_bleu;

    // Start is called before the first frame update
    void Start()
    {
        proportion_rouge = 1.0f / stateManager.instance.nbdSaut;
        proportion_bleu = 1.0f / stateManager.instance.nbDash;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rouge.fillAmount -= proportion_rouge;
            bleu.fillAmount -= proportion_bleu;
        }
    }
}
