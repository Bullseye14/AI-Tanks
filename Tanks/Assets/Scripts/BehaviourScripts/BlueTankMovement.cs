using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlueTankMovement : MonoBehaviour
{
    public GameObject RedTank;
    public Vector3 distanceBetweenTanks;
    public bool BluePatrol, BlueChase;
    public TanksMoveManager MoveManager;
    public GameObject PatrolB;
    public GameObject ChaseB;

    // Patrolling Movement Variables
    private NavMeshAgent agent;
    private int currentWaypoint;
    private bool isTravelling;
    private bool nextWaypoint = false;
    private Vector3 target;
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
        //distanceBetweenTanks = RedTank.position - transform.position;
        if (!RedTank.activeSelf) currentWaypoint = 0;

        if (MoveManager.BPatrol)
        {
            PatrolB.SetActive(true);
            PatrolMove();
        }
            
        else if (MoveManager.BChase)
        {
            ChaseB.SetActive(true);
            ChaseMove();
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
        agent.SetDestination(RedTank.transform.position);
    }
}
