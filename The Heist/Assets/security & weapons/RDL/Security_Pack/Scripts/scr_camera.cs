using UnityEngine;
using System.Collections;
using System;

public class scr_camera : MonoBehaviour
{

    public float rotate_amount;
    public bool enter;
    private int count;
    public AudioClip alarm;
    public AudioClip beep;
    public AudioClip cameraDestroy;
    private AudioSource source;
    public AudioSource source2;
    public GameObject CameraShock;
    public bool PlayerDetected;
    public bool AlarmActive;
    public float alertRadius;
    public float cameraHealth = 50f;
    private int alarmTimerBeeper = 0;
    private int alarmTimerMain = 0;


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
            AlarmActive = false;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            DestroyCamera();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerDetected = true;
            alarmTimerBeeper++;
            alarmTimerMain++;

            if (AlarmActive == false && alarmTimerBeeper >= 90)
            {
                source.PlayOneShot(beep);
                alarmTimerBeeper = 0;
            }

            if (AlarmActive == false && alarmTimerMain >= 480)
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
                        guard.GetComponent<SecurityGuardAI>().aiState = SecurityGuardAI.AIState.search;
                    }
                    catch (Exception e)
                    {
                        guard.GetComponent<SecurityGuardAI2>().aiState = SecurityGuardAI2.AIState.search;
                    }
                }
            }

        }
        Debug.Log("Entered");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            alarmTimerMain = 0;
            alarmTimerBeeper = 0;
        }
    }

    public void DestroyCamera()
    {
        source2.Play();
        Instantiate(CameraShock, transform.position, transform.rotation);
        transform.Rotate(0, 0, -70, Space.Self);
    }

    public void TakeDamage(float damage)
    {
        cameraHealth -= damage;
        if(cameraHealth <= 0)
        {
            DestroyCamera();
        }
    }

}

