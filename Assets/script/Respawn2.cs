using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject spawn;
    private float limite;
    void Start()
    {
        limite = player.GetComponent<Transform>().position.z - 20;
    }
    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Transform>().position.y < limite)
        {
            player.GetComponent<Playercontroler>().speed = 0;
            player.GetComponent<Transform>().SetPositionAndRotation(spawn.GetComponent<Transform>().position, Quaternion.identity);
        }
    }

}

