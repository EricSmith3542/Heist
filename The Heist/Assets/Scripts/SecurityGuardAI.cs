using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityGuardAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private ArrayList patrolPoints = new ArrayList();
    public Transform patrollingTo;
    private int nextPatrolIndex = 0;

    public Transform player;

    //How far in front the guard will detect
    public float sightDistance = 1.5f;

    //How far to each side the guard will see; if set to 30, guard will detect player 25 degrees to right or left
    //That is within the sightDistance
    public float sightAngle = 25f;

    //Distance to kill player if using lethal/non force
    public float lethalKillDistance = .3f;
    public float nonLethalKillDistance = .1f;

    //How far away the player has to get before the guard stops chasing
    public float chaseDistance = 1.5f;

    //A game object holding patrol points that draw a guards path
    //For an example, see Patrol Points 1 and Patrol Points 2 in the Hierarchy
    public GameObject patrolPointObject;

    //Bool for determining if using lethal force or not
    public bool lethalForce = false;

    public enum AIState { patrol, alert, search, chase, stunned, attack, dead }

    public AIState aiState = AIState.patrol;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        foreach (Transform patrolPoint in patrolPointObject.transform)
        {
            patrolPoints.Add(patrolPoint);
        }

        patrollingTo = (Transform)patrolPoints[nextPatrolIndex];

        StartCoroutine(Think());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool Seen()
    {
        var directionToPlayer = player.position - transform.position;
        var distFromPlayer = Vector3.Distance(player.position, transform.position);

        bool sight = Vector3.Angle(directionToPlayer, transform.forward) <= sightAngle && distFromPlayer <= sightDistance;
        return sight;
    }

    IEnumerator Think()
    {
        while (true)
        {
            switch (aiState)
            {
                case AIState.patrol:

                    if (Seen())
                    {
                        aiState = AIState.chase;
                        anim.SetBool("chase", true);
                    }
                    else
                    {
                        if (Vector3.Distance(patrollingTo.position, transform.position) < .3f)
                        {
                            if (nextPatrolIndex == patrolPoints.Count - 1)
                            {
                                nextPatrolIndex = 0;
                            }
                            else
                            {
                                nextPatrolIndex += 1;
                            }
                            patrollingTo = (Transform)patrolPoints[nextPatrolIndex];
                        }
                        navMeshAgent.SetDestination(patrollingTo.position);
                    }
                    break;
                case AIState.search:
                    if (Seen())
                    {
                        navMeshAgent.SetDestination(player.position);
                        aiState = AIState.chase;
                        anim.SetBool("chase", true);
                    }
                    break;
                case AIState.chase:
                    var dist = Vector3.Distance(player.position, transform.position);
                    if ((lethalForce && lethalKillDistance >= dist) || nonLethalKillDistance >= dist)
                    {
                        aiState = AIState.attack;
                    }
                    else if (dist > chaseDistance)
                    {
                        aiState = AIState.patrol;
                        anim.SetBool("chase", false);
                    }
                    else
                    {
                        navMeshAgent.SetDestination(player.position);
                    }
                    break;
                case AIState.stunned:
                    anim.SetBool("stunned", true);
                    yield return new WaitForSeconds(30f);
                    aiState = AIState.patrol;
                    anim.SetBool("stunned", false);
                    break;
                case AIState.attack:
                    Debug.Log("DEAD");
                    Application.Quit();
                    break;
                case AIState.dead:
                    anim.SetBool("dead", true);
                    break;
            }
            yield return new WaitForSeconds(.3f);
        }
    }
}
