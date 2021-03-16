using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortController : MonoBehaviour
{
    public Animator animator;
    bool isShielding;
    bool isHurt;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isShielding = GetComponent<PouvoirBouclier>().GetIsShielding();
        isHurt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHurt == true)animator.SetTrigger("dies");
    }
    public void hurt()
    {
        isHurt = true;
    }
}
