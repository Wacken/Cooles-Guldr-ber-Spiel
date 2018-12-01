using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Makes enemies follow and attack the player */

public class Monster : MonoBehaviour
{

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;

    enum MonsterState
    {
        Idle,
        Searching,
        Hunting
    }

    MonsterState _state = MonsterState.Searching;

    void Start()
    {
        target = Player.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        switch (_state)
        {
            case MonsterState.Idle:

                break;
            case MonsterState.Searching:
                // Get the distance to the player
                float distance = Vector3.Distance(target.position, transform.position);

                // If inside the radius
                if (distance <= lookRadius)
                {
                    // Move towards the player+
                    agent.SetDestination(target.position);
                    if (distance <= agent.stoppingDistance)
                    {
                        // Attack

                    }
                }
                break;
            case MonsterState.Hunting:

                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
