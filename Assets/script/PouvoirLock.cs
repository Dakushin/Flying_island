using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PouvoirLock : MonoBehaviour
{
    Canvas lockui;
    public Canvas cadnaouvert;
    public Sprite sp_cadnaouvert;
    public Sprite sp_cadnafermer;
    public Sprite sp_cadnabriser;
    private List<Canvas> canvas;
    public Camera cam;
    public CinemachineFreeLook player;
    public CinemachineFreeLook épaule;
    public float swl;
    float savespeedpc;
    float savespeedp;
    bool pouvoir = false;
    bool instancie = false;
    GameObject Lockobj;
    Transform savepos;
    bool locke = false;
    public float tpsdelock = 5f;
    float timer = 0;
    float proportion_lock;
    public Image Lock;
    public Text inbLock;
    public AudioSource audio;
    // Update is called once per frame

    private void Start()
    {
        proportion_lock = 1.0f / stateManager.instance.nbLock;
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            inbLock.text = stateManager.instance.nbLock.ToString();
        }
        canvas = new List<Canvas>();
        ///sp_cadnaouvert = cadnaouvert.transform.GetComponentInChildren<UnityEngine.UI.Image>().sprite;
    }
    void PassageCameraEpaule()
    {
        player.gameObject.SetActive(false);
        épaule.gameObject.SetActive(true);
        épaule.m_YAxis.Value = 0.5f;
        épaule.m_XAxis.Value = -15f;
        savespeedpc = gameObject.GetComponent<Playercontroler>().speed;
        gameObject.GetComponent<Playercontroler>().SetRotation(false);
    }

    void PassageCameraPlayer()
    {
        player.gameObject.SetActive(true);
        player.m_YAxis.Value = 0.5f;
        player.m_XAxis.Value = 0f;
        épaule.gameObject.SetActive(false);
        gameObject.GetComponent<Playercontroler>().SetRotation(true);
        gameObject.GetComponent<Playercontroler>().speed = savespeedpc;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !locke && !pouvoir && stateManager.instance.nbLock > 0)
        {
            Lock.fillAmount -= proportion_lock;
            stateManager.instance.nbLock--;
            inbLock.text = stateManager.instance.nbLock.ToString();
            pouvoir = true;
            PassageCameraEpaule();
            gameObject.GetComponent<Playercontroler>().speed = swl;
            
        }

        if (pouvoir)
        {
            RaycastHit hit;
            Ray ray = new Ray(gameObject.transform.position, cam.transform.forward);
            if (!instancie)
            {
                GameObject[] list = GameObject.FindGameObjectsWithTag("lockable");
                foreach (GameObject obj in list)
                {
                    obj.layer = 9;
                    cadnaouvert.GetComponent<LookAtcanvas>().cam = cam;
                    Canvas can = Instantiate(cadnaouvert, obj.transform.position, obj.transform.rotation);
                    can.GetComponent<LookAtcanvas>().platasuivre = obj;
                    canvas.Add(can);
                    
                    
                    /*Material[] matlist = obj.GetComponent<Renderer>().materials;
                    foreach (Material mat in matlist)
                    {
                        mat.ToFadeMode();
                    }*/

                }
                instancie = true;
            }
            int layerMask = LayerMask.GetMask("lockable");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                foreach (Canvas can in canvas)
                {

                    if (hit.transform.position.x - 0.1 <= can.transform.position.x && can.transform.position.x <= hit.transform.position.x + 0.1 && hit.transform.position.y - 0.1 <= can.transform.position.y && can.transform.position.y <= hit.transform.position.y + 0.1 && hit.transform.position.z - 0.1 <= can.transform.position.z && can.transform.position.z <= hit.transform.position.z + 0.1)
                    {
                        Debug.Log("ici");
                        can.GetComponentInChildren<UnityEngine.UI.Image>().sprite = sp_cadnafermer;
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Lockobj = GameObject.Find(hit.transform.name);
                            Debug.Log(hit.transform.name);
                            Debug.Log(Lockobj);
                            audio.PlayOneShot(audio.clip);
                            ///cadnaouvert.transform.GetComponentInChildren<UnityEngine.UI.Image>().sprite = sp_cadnafermer;
                            lockui = Instantiate(cadnaouvert, Lockobj.transform.position, Lockobj.transform.rotation);
                            lockui.GetComponent<LookAtcanvas>().platasuivre = Lockobj;
                            lockui.transform.GetComponentInChildren<UnityEngine.UI.Image>().sprite = sp_cadnafermer;
                            pouvoir = false;
                            locke = true;
                            PassageCameraPlayer();
                            gameObject.GetComponent<Playercontroler>().speed = savespeedpc;
                        }
                    }


                }

                if (locke)
                {
                    foreach (Canvas can in canvas)
                    {
                        Destroy(can.gameObject);
                    }
                    canvas.Clear();
                    GameObject[] list = GameObject.FindGameObjectsWithTag("lockable");
                    foreach (GameObject obj in list)
                    {
                        obj.layer = 8;

                    }

                }
            }
            else foreach (Canvas can in canvas)
                { can.GetComponentInChildren<UnityEngine.UI.Image>().sprite = sp_cadnaouvert; }
                    
                    


        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            timer = 0;
            locke = false;
            pouvoir = false;
            instancie = false;
            foreach (Canvas can in canvas)
            {
                Destroy(can.gameObject);
            }
            canvas.Clear();
            PassageCameraPlayer();
            GameObject[] list = GameObject.FindGameObjectsWithTag("lockable");
            foreach (GameObject obj in list)
            {
                /*Material[] matlist = obj.GetComponent<Renderer>().materials;
                foreach (Material mat in matlist)
                {
                    mat.ToOpaqueMode();
                }*/
                obj.layer = 8;

            }
        }
      
        if(locke)
        {
            timer += Time.deltaTime;
            if (timer <= tpsdelock)
            {
                Debug.Log(timer);
                Lockobj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                lockui.transform.GetComponentInChildren<UnityEngine.UI.Image>().sprite = sp_cadnafermer;
            } else
            {
                if(timer <= tpsdelock + 2)
                {
                    lockui.transform.GetComponentInChildren<UnityEngine.UI.Image>().sprite = sp_cadnabriser;
                    Lockobj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    GameObject[] list = GameObject.FindGameObjectsWithTag("lockable");
                    foreach (GameObject obj in list)
                    {
                        obj.layer = 8;
                    }
                } else {
                    timer = 0;
                    locke = false;
                    pouvoir = false;
                    instancie = false;
                    Destroy(lockui.gameObject);
                    /*GameObject[] list = GameObject.FindGameObjectsWithTag("lockable");
                    foreach (GameObject obj in list)
                    {
                        Material[] matlist = obj.GetComponent<Renderer>().materials;
                        foreach (Material mat in matlist)
                        {
                            mat.ToOpaqueMode();
                        }

                    }*/
                }
            }
        }    
    }
}

public static class MaterialExtensions
{
    public static void ToOpaqueMode(this Material material)
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }

    public static void ToFadeMode(this Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }
}
