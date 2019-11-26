using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertScript : MonoBehaviour
{
    public float alertRadius = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Temporary alert method for testing AI
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            var guards = GameObject.FindGameObjectsWithTag("Guard");

            foreach (GameObject guard in guards)
            {
                var distFromAlert = Vector3.Distance(guard.transform.position, transform.position);

                Debug.Log("DISTANCE: " + distFromAlert);

                if (distFromAlert <= alertRadius)
                {
                    Debug.Log("ALERTED");
                    guard.GetComponent<NavMeshAgent>().SetDestination(transform.position);
                    guard.GetComponent<SecurityGuardAI>().aiState = SecurityGuardAI.AIState.search;
                }
            }
        }
    }
}
