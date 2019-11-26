using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    public Transform patrollingTo;
    private int nextPatrolIndex = 0;
    private SecurityGuardAI guardAI;

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

    IEnumerator Think()
    {
        while (true)
        {
            switch (aiState)
            {
                case AIState.follow:
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
                    navMeshAgent.SetDestination(player.position);
                    if(Vector3.Distance(player.position, transform.position) <= killDistance)
                    {
                        aiState = AIState.attack;
                    }
                    break;

                case AIState.attack:
                    Debug.Log("DEAD");
                    Application.Quit();
                    break;

                case AIState.tazed:
                    yield return new WaitForSeconds(30f);
                    aiState = AIState.lost;
                    break;
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
