using UnityEngine;
using System.Collections;
using System;

public class scr_camera : MonoBehaviour
{

    public float rotate_amount;
    public bool enter;
    private int count;
    public AudioClip alarm;
    private AudioSource source;
    public bool PlayerDetected;
    public bool AlarmActive;
    public float alertRadius;


    // Use this for initialization
    void Start()
    {
        enter = false;
        AlarmActive = false;
        source = GetComponent<AudioSource>();
        PlayerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDetected == false)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, (Mathf.Sin(Time.realtimeSinceStartup) * rotate_amount) + transform.eulerAngles.y, transform.eulerAngles.z);
        }

        //for testing purposes
        if (Input.GetKeyDown(KeyCode.F))
        {
            source.Stop();
        }
    }

    public bool getPlayerDetected()
    {
        return PlayerDetected;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerDetected = true;
            if (AlarmActive == false)
            {
                // Play the sound only on the trigger
                source.Play();
                AlarmActive = true;
                count -= 1;
            }

            var guards = GameObject.FindGameObjectsWithTag("Guard");

            foreach (GameObject guard in guards)
            {
                var distFromAlert = Vector3.Distance(guard.transform.position, transform.position);

                Debug.Log("DISTANCE: " + distFromAlert);

                if (distFromAlert <= alertRadius)
                {
                    Debug.Log("ALERTED");
                    guard.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(transform.position);

                    try
                    {
                       // guard.GetComponent<SecurityGuardAI>().aiState = SecurityGuardAI.AIState.search;
                    }
                    catch(Exception e)
                    {
                       // guard.GetComponent<SecurityGuardAI2>().aiState = SecurityGuardAI2.AIState.search;
                    }
                }
            }

        }
        Debug.Log("Entered");
    }
    /*
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = false;
            count = 1;
        }
        Debug.Log("Exited");
    }
    */
}

