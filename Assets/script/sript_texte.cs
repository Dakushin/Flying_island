using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sript_texte : MonoBehaviour
{
    public Transform lookTarget;
    public string type_de_map;
    public int limitlevel;

    private TextMesh textMesh;
    void Start()
    {
        textMesh = transform.GetChild(0).GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

        if (stateManager.instance.level > limitlevel)
        {
            textMesh.text = "MAP "+ type_de_map +
                "\n \n" +
                "Réussie !!";
        }
        else
        {
            textMesh.text = "MAP " + type_de_map +
    "\n \n" +
    "Pas réalisé";
        }
    }
}
