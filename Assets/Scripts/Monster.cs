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

    }

    void Start()
    {
        target = Player.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
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
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
