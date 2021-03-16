using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changementcamera : MonoBehaviour
{

    CinemachineFreeLook player;
    CinemachineFreeLook épaule;
    // Update is called once per frame
    void PassageCameraEpaule()
    {
        player.gameObject.SetActive(false);
        épaule.gameObject.SetActive(true);
    }

    void PassageCameraPlayer()
    {
        player.gameObject.SetActive(true);
        épaule.gameObject.SetActive(false);
    }

}
