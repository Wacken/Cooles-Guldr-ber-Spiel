using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Makes enemies follow and attack the player */

public class Monster : MonoBehaviour
{
    public float _smellRadius = 10f;

    public AudioClip _monsterSound;

    Transform _target;
    NavMeshAgent _agent;

    [SerializeField]
    Transform[] _patroulPath;
    int _currentPatroulPoint = -1;

    enum MonsterState
    {
        Idle,
        Searching,
        Hunting
    }

    MonsterState _state = MonsterState.Searching;

    void Start()
    {
        _target = Player.instance.player.transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        switch (_state)
        {
            case MonsterState.Idle:

                break;
            case MonsterState.Searching:
                patroul();
                break;
            case MonsterState.Hunting:
                chasePlayer();
                break;
        }
    }

    private void chasePlayer()
    {
        // Get the distance to the player
        _agent.speed *= 2;
        float distance = Vector3.Distance(_target.position, transform.position);

        // If inside the radius
        if (distance <= _smellRadius)
        {
            // Move towards the player+
            _agent.SetDestination(_target.position);
            if (distance <= _agent.stoppingDistance)
            {
                // Attack
                
            }
        }
        else
        {
            float destDistance = Vector3.Distance(_agent.destination, transform.position);
            if(destDistance <= _agent.stoppingDistance)
            {
                _agent.speed /= 2;
                _currentPatroulPoint = -1;
                _state = MonsterState.Searching;
            }
        }
    }

    void patroul()
    {
        float destDistance = Vector3.Distance(_agent.destination, transform.position);
        if (_currentPatroulPoint == -1 || destDistance <= _agent.stoppingDistance)
        {
            _agent.SetDestination(_patroulPath[(_currentPatroulPoint + 1)%_patroulPath.Length].position);
        }

        float distance = Vector3.Distance(_target.position, transform.position);
        if (distance <= _smellRadius)
        {
            _state = MonsterState.Hunting;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _smellRadius);
    }
}
