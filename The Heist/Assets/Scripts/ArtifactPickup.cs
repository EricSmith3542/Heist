using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown(KeyCode.E.ToString()) && other.gameObject.tag == "Player")
        {
            GameObject bust = GameObject.Find("JuliusCaesarBust");
            bust.GetComponent<MeshRenderer>().enabled = false;

            Application.Quit();
        }
    }

    //private void OnTriggerStay)
    //{
    //    Ray ray;
    //    RaycastHit hit;
    //    float distance; // distance you want it to work


    //    if (Input.GetButtonDown("e"))
    //    {
    //        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        if (Physics.Raycast(ray, hit, distance))
    //        {
    //            Debug.Log(hit.name);

    //        }

    //    }
    //}
}
