using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DogAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    public Transform patrollingTo;
    private int nextPatrolIndex = 0;
    private SecurityGuardAI guardAI;

    public float doggoHealth = 50f;

    bool isDeadBool = false;
    bool isTazedBool = false;

    public Transform player;
    public float chaseDistance = 2f;
    public float killDistance = .1f;

    //The guard that the dog is initially assigned to
    public GameObject guard;

    public enum AIState { follow, lost, chase, attack, tazed, dead }

    public AIState aiState = AIState.follow;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        guardAI = guard.GetComponent<SecurityGuardAI>();
        StartCoroutine(Think());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage) {
        doggoHealth -= damage;
        if (doggoHealth <= 0) {
            isDeadBool = true;
        }
    }

    public void SetTazed() {
        isTazedBool = true;
    }
    void isTazed()
    {
        if (isTazedBool)
        {
            aiState = AIState.tazed;
        }
    }

    void isDead()
    {
        if (isDeadBool)
        {
            aiState = AIState.dead;
        }
    }
    IEnumerator Think()
    {
        while (true)
        {
            switch (aiState)
            {
                case AIState.follow:
                    isDead();
                    isTazed();

                    if(guardAI.aiState == SecurityGuardAI.AIState.chase)
                    {
                        aiState = AIState.chase;
                    }
                    else
                    {
                        navMeshAgent.SetDestination(guard.transform.position);
                    }
                    break;

                case AIState.lost:
                    isDead();
                    isTazed();

                    var dist = -1f;
                    foreach(GameObject g in GameObject.FindGameObjectsWithTag("Guard"))
                    {
                        var currentDist = Vector3.Distance(g.transform.position, transform.position);

                        if(dist == -1 || dist > currentDist)
                        {
                            guard = g;
                            dist = currentDist;
                        }
                    }
                    aiState = AIState.follow;
                    break;

                case AIState.chase:
                    isDead();
                    isTazed();

                    navMeshAgent.SetDestination(player.position);
                    if(Vector3.Distance(player.position, transform.position) <= killDistance)
                    {
                        aiState = AIState.attack;
                    }
                    break;

                case AIState.attack:
                    isDead();
                    isTazed();

                    //Debug.Log("DEAD");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                    break;

                case AIState.tazed:
                    yield return new WaitForSeconds(30f);
                    isTazedBool = false;
                    aiState = AIState.lost;
                    break;

                case AIState.dead:
                    Destroy(this.gameObject);
                    break;
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
