using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Playercontroler : MonoBehaviour
{
    private Animator animator;
    public float dashforce = 5;
    public float jump = 6;
    public float speed = 5;
    public float divdespeedwhenjump = 3;
    public float dashtime = 2;
    public float cooldashtime = 2;
    public float turnSmoothTime = 0.1f;
    public Transform cam;
    public LayerMask groundlayer;
    private float horizontalInput;
    private float forwardInput;
    private float tps;
    private float ctps;
    private float turnSmoothVelocity;
    private Rigidbody rb;
    private Collider col;
    private Renderer rend;
    public GameObject Model;
    bool rotation = true;
    bool b_jump = false;
    bool doublejump = false;
    bool isgrounded = false;
    bool b_dash = false;
    bool b_cdash = false;
    float proportion_Dash;
    public AudioSource audio1;
    public AudioSource audio2;
    public Image Dash;
    public Text inbdash;
    public GameObject sondpl;
    private GameObject instance;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rend = GetComponentInChildren<Renderer>();
        proportion_Dash = 1.0f / stateManager.instance.nbDash;
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            inbdash.text = stateManager.instance.nbDash.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //on crée une sphere au pied du personnage qui tchek si on est sur le sol

        animator.SetBool("grounded", isgrounded);
        isgrounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), .3f, groundlayer);


        if (!isgrounded) { 
            if (instance)
            {
                Destroy(instance);
            }
        }
        if (Input.GetKeyDown("space"))
        {
            if (isgrounded)
            {
                audio1.PlayOneShot(audio1.clip);
                b_jump = true;
                doublejump = true;
                //speed = speed / divdespeedwhenjump;
            }
            else if (doublejump)
            {
                rb.velocity = Vector3.zero;
                b_jump = true;
                doublejump = false;
                animator.SetTrigger("jump");
            }
        }

        if (Input.GetMouseButtonDown(0) && stateManager.instance.nbDash > 0 && !b_cdash)
        {
            audio2.PlayOneShot(audio2.clip);
            rb.useGravity = false;
            b_dash = true;
            b_cdash = true;
            Dash.fillAmount -= proportion_Dash;
            stateManager.instance.nbDash--;
            inbdash.text = stateManager.instance.nbDash.ToString();
        }

        
    }
    private void FixedUpdate()
    {
        animator.SetFloat("deplacement_v", rb.velocity.y);
        if (isgrounded) animator.SetFloat("deplacement_v", -5);

        Vector3 inputvec = new Vector3(horizontalInput, 0f, forwardInput).normalized; //recupère les input pour le deplacer, les ajoute a un vecteur (vecteur de deplacement)
        float targetAngle = Mathf.Atan2(inputvec.x, inputvec.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        if (rotation)
        {
            if (inputvec.magnitude >= 0.9f && !b_dash)
            {
                if (!instance)
                {
                    instance = Instantiate(sondpl);
                }
                animator.SetFloat("deplacement", inputvec.magnitude);
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            else { 
                animator.SetFloat("deplacement", 0);
                if (instance)
                {
                    Destroy(instance);
                }              
            }
        } else rb.velocity = new Vector3(0f, rb.velocity.y, 0f) + (Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(inputvec.x, 0f, inputvec.z)) * speed;
               
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f) + (Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0f, 0f, inputvec.magnitude)) * speed;


        
        
        if (b_jump)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.VelocityChange); // force qui va faire sauté le personnage
            b_jump = false;

        }

        if(b_dash)
        {
            //Vector3 vecdash = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0f, 0f, 1f);
            rb.AddForce(gameObject.transform.forward * dashforce, ForceMode.VelocityChange);
            //rb.velocity = new Vector3(horizontalInput * dashforce, 0, forwardInput * dashforce);
            tps += Time.fixedDeltaTime;
            //Debug.Log(tps);
            if (tps >= dashtime)
            {
                b_dash = false;
                tps = 0;
            }
            else rb.useGravity = true;

        }

        if (b_cdash)
        {
            ctps += Time.fixedDeltaTime;
            if (ctps >= cooldashtime)
            {
                b_cdash = false;
                ctps = 0;
            }
        }
    }
    
    public void SetRotation(bool x)
    {
        rotation = x;
    }
}
   