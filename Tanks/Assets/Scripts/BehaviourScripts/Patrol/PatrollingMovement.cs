using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingMovement : MonoBehaviour
{
    // Patrolling Movement Variables
    private NavMeshAgent agent;
    private int currentWaypoint;
    private bool isTravelling;
    private bool isWaiting;
    private bool nextWaypoint = false;
    private float waitTimer;
    private Vector3 target;

    // Public
    public bool patrolWaiting; // if agents is waiting or not in a waypoint
    public float totalWaitTime; // time that agent waits in each waypoint
    //public float changeWaypointProbability = 0.2f; // probability to change direction
    public List<WayPoints> waypoints; // List of all waypoints in the game

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (waypoints != null && waypoints.Count >= 2)
        {
            currentWaypoint = 0;
            SetDestination();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTravelling && agent.remainingDistance <= 0.5f)
        {
            isTravelling = false;

            if (patrolWaiting)
            {
                isWaiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                isWaiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if (waypoints != null)
        {
            target = waypoints[currentWaypoint].transform.position;
            agent.SetDestination(target);
            isTravelling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        /*if (UnityEngine.Random.Range(0f, 1f) <= changeWaypointProbability) ---> Probability to change patrolPoint
            nextWaypoint = !nextWaypoint;*/

        if (nextWaypoint)
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        else
        {
            if (--currentWaypoint < 0)
                currentWaypoint = waypoints.Count - 1;
        }
    }
}
