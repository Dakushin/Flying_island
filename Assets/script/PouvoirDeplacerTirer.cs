using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PouvoirDeplacerTirer : MonoBehaviour
{
    public float taillecollider;
    public float forcevoulue = 5;
    public float distance = 2;
    public float forceejectionitem = 10;
    float timer;
    float rotation;
    private List<GameObject> objrecup;
    float proportion_grab;
    public Image grab;
    public Text inbgrab;
    public float distanceDeRécupe;

    private void Start()
    {
        proportion_grab = 1.0f / stateManager.instance.nbGrab;
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            inbgrab.text = stateManager.instance.nbGrab.ToString();
        }
        objrecup = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.W) && stateManager.instance.nbGrab > 0)
        {
            grab.fillAmount -= proportion_grab;
            stateManager.instance.nbGrab--;
            inbgrab.text = stateManager.instance.nbGrab.ToString();
            if (!gameObject.GetComponent<SphereCollider>())
            {
                gameObject.AddComponent<SphereCollider>();
            }
            gameObject.GetComponent<SphereCollider>().radius = taillecollider;
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
            GameObject[] list = GameObject.FindGameObjectsWithTag("Deplacable");
            foreach (GameObject obj in list)
            {
                if ((Vector3.Distance(obj.transform.position, GetComponent<Transform>().position)) < distanceDeRécupe)
                {

                    Vector3 addforce = gameObject.transform.position - obj.transform.position;
                    obj.GetComponent<Rigidbody>().AddForce(addforce * forcevoulue);
                }
            }
        }

        if (objrecup.Count != 0)
        {

            rotation += 2 * Mathf.PI / 300;
            if(rotation >= 2* Mathf.PI)
            {
                rotation = 0;
            }
            float position = 2 * Mathf.PI / objrecup.Count;
            for(int i = 1; i <= objrecup.Count; i++)
            {
                objrecup[i - 1].transform.position = new Vector3(transform.position.x + Mathf.Cos(position * i + rotation) * distance , transform.position.y + gameObject.GetComponentInChildren<Renderer>().bounds.size.y, transform.position.z + Mathf.Sin(position * i + rotation) * distance);

            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject.GetComponent<SphereCollider>());
            GameObject objalancer = objrecup[objrecup.Count - 1];
            objrecup.RemoveAt(objrecup.Count - 1);
            objalancer.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z);
            objalancer.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            Debug.Log(gameObject.transform.forward * forceejectionitem);
            objalancer.gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * forceejectionitem;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deplacable" && !objrecup.Contains(other.gameObject))
        {
            objrecup.Add(other.gameObject);
        }
    }


}
