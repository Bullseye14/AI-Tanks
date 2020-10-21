using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingMovement : MonoBehaviour
{

    // Patrolling Movement Variables
    private NavMeshAgent _navMeshAgent;
    int _currentPatrolIndex;
    bool _travelling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;
    Vector3 targetVector;


    [SerializeField]
    bool _patrolWaiting; // if agents is waiting or not in a waypoint

    [SerializeField]
    float _totalWaitTime = 3f; // time that agent waits in eaach waypoint

    [SerializeField]
    float _switchProbability = 0.2f; // probability to change direction

    [SerializeField]
    List<WayPoints> _patrolPointsGame; // List of all waypoints in the game

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_patrolPointsGame != null && _patrolPointsGame.Count >= 2)
        {
            _currentPatrolIndex = 0;
            SetDestination();
        }
        else
        {
            Debug.Log("Not enough waypoints");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_travelling && _navMeshAgent.remainingDistance <= 0.5f)
        {
            _travelling = false;

            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (_patrolPointsGame != null)
        {
            targetVector = _patrolPointsGame[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        /*if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)   // Probability to change patrolPoint
            _patrolForward = !_patrolForward;*/

        if (_patrolForward)
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPointsGame.Count;
        else
        {
            if (--_currentPatrolIndex < 0)
                _currentPatrolIndex = _patrolPointsGame.Count - 1;
        }
    }
}
