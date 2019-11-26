using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityGuardAIDamage : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    public Transform player;
    public float chaseDistance = 5;

    public float health = 100f;

    bool isDead = false;
    bool isTazed = false;

    public enum AIState {patrol, alert, taze, lethalForce, tazed, death}

    public AIState aiState = AIState.patrol;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Think());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            isDead = true;
        }
    }

    public void SetTazed() {
        isTazed = true;
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
                //Guard is simply patrolling around the mansion
                case AIState.patrol:

                    //Check if guard is dead
                    if (isDead) {
                        aiState = AIState.death;
                    }

                    //Check if guard is tazed
                    if (isTazed)
                    {
                        aiState = AIState.tazed;
                    }

                    break;

                //Guard is on high alert
                case AIState.alert:
                    var distFromPlayer = Vector3.Distance(player.position, transform.position);
                    if(distFromPlayer <= chaseDistance)
                    {
                        navMeshAgent.SetDestination(player.position);
                        aiState = AIState.taze;
                    }

                    //Check if guard is dead
                    if (isDead)
                    {
                        aiState = AIState.death;
                    }

                    //Check if guard is tazed
                    if (isTazed)
                    {
                        aiState = AIState.tazed;
                    }


                    break;

                //Guard is chasing player, and will taze when he reaches player
                case AIState.taze:
                    //Guard is chasing the player with his tazer

                    //Check if guard is dead
                    if (isDead)
                    {
                        aiState = AIState.death;
                    }

                    //Check if guard is tazed
                    if (isTazed)
                    {
                        aiState = AIState.tazed;
                    }

                    break;

                //Guard has been tazed by player
                case AIState.tazed:
                    //Guard has been tazed by the player

                    //Check if guard is dead
                    if (isDead)
                    {
                        aiState = AIState.death;
                    }
                    break;

                //Guard is shooting player
                case AIState.lethalForce:
                    //Guard is shooting at the player

                    //Check if guard is dead
                    if (isDead)
                    {
                        aiState = AIState.death;
                    }

                    //Check if guard is tazed
                    if (isTazed)
                    {
                        aiState = AIState.tazed;
                    }

                    break;

                //Guard is dead
                case AIState.death:
                    //Guard has died


                    break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
