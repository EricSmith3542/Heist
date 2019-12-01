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

    //Check if AI is dead or stunned
    private bool isDead = false;
    private bool isTazed = false;

    //Check if guard has been attacked
    private bool guardAttacked = false;

    //AI health
    public float health = 100f;

    //Chick if player has been detected by a camera
    scr_camera playerDetected;

    //Time before guard gives up on a search
    private bool timerSet = false;
    public float searchTimer = 30f;

    //How far in front the guard will detect
    public float sightDistance = 1.5f;

    //How far to each side the guard will see; if set to 30, guard will detect player 25 degrees to right or left
    //That is within the sightDistance
    public float sightAngle = 25f;

    //Distance to kill player if using lethal/non force
    public float lethalKillDistance = .3f;
    public float nonLethalKillDistance = .1f;

    //How far away the player has to get before the guard stops chasing
    public float chaseDistance = 3f;

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
        //Countdown timer for search state before guard gives up
        if (timerSet)
        {
            searchTimer -= Time.deltaTime;

            if(searchTimer <= 0f)
            {
                timerSet = false;
                searchTimerEnded();
            }
        }

    }

    //Calculate life remaining after taking damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
        }
    }

    //AI is tazed if the player shoots it with tazer
    public void SetTazed()
    {
        isTazed = true;
    }

    //Change state if guard is dead
    public void IsDead()
    {
        if (isDead)
        {
            aiState = AIState.dead;
        }
    }

    //Change state if guard is tazed
    public void IsTazed()
    {
        if (isTazed)
        {
            aiState = AIState.stunned;
        }
    }

    //Change state after guard fails to find player
    public void searchTimerEnded()
    {
        Debug.Log("Back to patrol state");
        aiState = AIState.patrol;
    }

    public void IsAttacked() {
        if (guardAttacked)
        {
            Debug.Log("Guard attacked");
            navMeshAgent.SetDestination(player.position);
            aiState = AIState.chase;
            anim.SetBool("chase", true);
        }
    }

    //Change state if player is detected by the camera
    public void IsDetected()
    {
        if (playerDetected.getPlayerDetected())
        {
            Debug.Log("Player Detected");
            navMeshAgent.SetDestination(player.position);
            aiState = AIState.search;
            anim.SetBool("chase", true);
        }
    }

//If the guard sees the player
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
                //The guard is doing his normal patrol
                case AIState.patrol:
                    //Check if guard is dead and change state
                    IsDead();

                    //Check if guard is tazed and change state
                    IsTazed();

                    //Change states if it sees the player with a weapon or after an artifact has been stolen
                    if (Seen() && (Weapons.currentWeapon != 0 || ArtifactStolen.artifactStolen == true))
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

                    //Check if guard has been attacked
                    IsAttacked();

                    //Check if camera has detected the guard
                    IsDetected();
                    break;

                //The guard is actively seeking out the player
                case AIState.search:
                    //Check if guard is dead and change state
                    IsDead();

                    //Check if guard is tazed and change state
                    IsTazed();

                    if (Seen())
                    {
                        navMeshAgent.SetDestination(player.position);
                        aiState = AIState.chase;
                        anim.SetBool("chase", true);
                    }

                    //Set 30 second timer for search time for the Guard. The guard gives up after the time ends and goes back to patrol state
                    if (timerSet == false)
                    {
                        timerSet = true;
                    }

                    //Check if camera has detected the guard
                    IsDetected();

                    //Check if guard has been attacked
                    IsAttacked();
                    break;

                //The guard is chasing the player
                case AIState.chase:
                    //Check if guard is dead and change state
                    IsDead();

                    //Check if guard is tazed and change state
                    IsTazed();

                    var dist = Vector3.Distance(player.position, transform.position);

                    /* 
                    if ((lethalForce && lethalKillDistance >= dist) || nonLethalKillDistance >= dist)
                    {
                        aiState = AIState.attack;
                    }
                    */

                    //Attack player if player is within kill distance
                    if ((guardAttacked || ArtifactStolen.artifactStolen) && lethalKillDistance <= dist) {
                        Debug.Log("Player Killed");
                        aiState = AIState.attack;
                    }

                    //If guard is too far away from player then enter patrol state
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
                
                //The guard has been hit by a tazer
                case AIState.stunned:
                    //Check if guard is dead and change state
                    IsDead();

                    anim.SetBool("stunned", true);
                    yield return new WaitForSeconds(30f);
                    aiState = AIState.patrol;
                    anim.SetBool("stunned", false);
                    break;
                //The guard has attacked, and the player is dead
                case AIState.attack:
                    Debug.Log("DEAD");
                    Application.Quit();
                    break;
                //The guard has died
                case AIState.dead:
                    anim.SetBool("dead", true);
                    break;
            }
            yield return new WaitForSeconds(.3f);
        }
    }
}
