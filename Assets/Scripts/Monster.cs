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

    [SerializeField]
    int _searchingSpeed = 10;
    [SerializeField]
    int _huntingSpeed = 20;
    AudioSource _audioSource;

    enum MonsterState
    {
        Idle,
        Searching,
        Hunting
    }

    [SerializeField]
    MonsterState _state = MonsterState.Hunting;

    void Start()
    {
        _target = Player.instance.player.transform;
        _agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _monsterSound;
        playSound(1.2f);
    }

    void Update()
    {
        switch (_state)
        {
            case MonsterState.Idle:

                break;
            case MonsterState.Searching:
                if (_patroulPath.Length <= 0) break;
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
        _agent.speed = _huntingSpeed;
        float distance = Vector3.Distance(_target.position, transform.position);

        // If inside the radius
        if (distance <= _smellRadius)
        {
            // Move towards the player+
            _agent.SetDestination(_target.position);
            if (distance <= _agent.stoppingDistance)
            {
                // Attack
                Death.instance.death();
            }
        }
        else
        {
            float destDistance = Vector3.Distance(_agent.destination, transform.position);
            if(destDistance <= _agent.stoppingDistance)
            {
                _agent.speed = _searchingSpeed;
                _currentPatroulPoint = -1;
                playSound(0.8f);
                _state = MonsterState.Searching;
            }
        }
    }

    void patroul()
    {
        float destDistance = Vector3.Distance(_agent.destination, transform.position);
        if (_currentPatroulPoint == -1 || destDistance <= _agent.stoppingDistance)
        {
            _agent.SetDestination(_patroulPath[(++_currentPatroulPoint)%_patroulPath.Length].position);
        }

        float distance = Vector3.Distance(_target.position, transform.position);
        if (distance <= _smellRadius)
        {
            playSound(1.2f);
            _state = MonsterState.Hunting;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _smellRadius);
    }

    void playSound(float pitch )
    {
        _audioSource.pitch = pitch;
        _audioSource.PlayScheduled(200);
    }
}
