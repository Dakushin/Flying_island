using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReglageSon : MonoBehaviour
{
    public Slider niveauson;
    
    public void Update()
    {
        AudioManager.instance.niveauSon = niveauson.value;
    }
}
