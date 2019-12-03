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

    public float cameraHealth = 50f;
    public bool cameraDestroyed = false;


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

    public bool GetPlayerDetected()
    {
        return PlayerDetected;
    }

    public void TakeDamage(float damage)
    {
        cameraHealth -= damage;
        if (cameraHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void destroyCamera()
    {
        Destroy(this.gameObject);
    }
    private bool guardResponse = false;
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

                if (!guardResponse && distFromAlert <= alertRadius)
                {
                    Debug.Log("ALERTED");
                    //guard.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(transform.position);
                    guard.GetComponent<SecurityGuardAI>().aiState = SecurityGuardAI.AIState.search;
                    guardResponse = true;
                }
            }
            guardResponse = false;

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

