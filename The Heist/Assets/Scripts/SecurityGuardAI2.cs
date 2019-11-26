using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityGuardAI2 : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private ArrayList patrolPoints = new ArrayList();
    public Transform patrollingTo;
    private int nextPatrolIndex = 0;

    public Transform player;
    public float chaseDistance = 5;

    public enum AIState { patrol, alertSounded, search, chase, stunned, attacking }

    public AIState aiState = AIState.patrol;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        foreach (Transform patrolPoint in GameObject.Find("Patrol Points 2").transform)
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

    //public void respondToAlert()
    //{
    //    var alertPostion = AlertScript.alerter.transform.position;
    //    var distFromAlert = Vector3.Distance(alertPostion, transform.position);
    //    if(distFromAlert <= alertRadius)
    //    {
    //        navMeshAgent.SetDestination(alertPostion);
    //        aiState = AIState.search;
    //    }
    //}

    IEnumerator Think()
    {
        while (true)
        {
            switch (aiState)
            {
                case AIState.patrol:

                    if (Vector3.Distance(player.position, transform.position) <= chaseDistance)
                    {
                        aiState = AIState.chase;
                    }
                    else
                    {
                        if (Vector3.Distance(patrollingTo.position, transform.position) < .05)
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
                    var distFromPlayer = Vector3.Distance(player.position, transform.position);
                    if (distFromPlayer <= chaseDistance)
                    {
                        navMeshAgent.SetDestination(player.position);
                        aiState = AIState.chase;
                    }
                    break;
                case AIState.chase:
                    navMeshAgent.SetDestination(player.position);
                    break;
                case AIState.stunned:
                    break;
                case AIState.attacking:
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
