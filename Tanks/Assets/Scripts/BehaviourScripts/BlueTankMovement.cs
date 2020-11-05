using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlueTankMovement : MonoBehaviour
{
    public Transform RedTank;
    public Vector3 distanceBetweenTanks;
    public bool BluePatrol, BlueChase;
    public TanksMoveManager MoveManager;

    // Patrolling Movement Variables
    private NavMeshAgent agent;
    private int currentWaypoint;
    private bool isTravelling;
    private bool nextWaypoint = false;
    private Vector3 target;
    public List<WayPoints> waypoints; // List of all waypoints in the game

    private void Awake()
    {
        RedTank = GameObject.Find("Tank2").transform;
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
        //distanceBetweenTanks = RedTank.position - transform.position;

        if (MoveManager.BPatrol)
            PatrolMove();
        if (MoveManager.BChase)
            ChaseMove();

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
        if (nextWaypoint)
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        else
        {
            if (--currentWaypoint < 0)
                currentWaypoint = waypoints.Count - 1;
        }
    }

    private void PatrolMove()
    {
        if (isTravelling && agent.remainingDistance <= 0.5f)
        {
            isTravelling = false;

            ChangePatrolPoint();
            SetDestination();
        }
    }

    private void ChaseMove()
    {
        //if(agent.remainingDistance <= 1f)
        agent.SetDestination(RedTank.position);
    }
}
