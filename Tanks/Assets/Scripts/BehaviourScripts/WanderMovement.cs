using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class WanderMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    //public LayerMask allArea;
    //public Transform ChasedTank;
    //public TankShooting tankShooting;

    // Patrolling Movement Variables
    //private int currentWaypoint;
    //private bool isTravelling;
    //private bool nextWaypoint = false;
    //private Vector3 target;

    // Public
    //public List<WayPoints> waypoints; // List of all waypoints in the game

    //Wander Movement Variables
    private float nextCheck;
    private NavMeshHit navHit;
    private Vector3 wanderTarget;
    private float checkRate = 0.3f;

    // Wander2 Vars
    //public Vector3 walkPoint;
    //bool walkPointSet;
    //public float walkPointRange;

    // Public
    public Transform TankWander;
    public Transform TankTarget;
    public float wanderRange = 10f;

    //public float detectedRange, attackRange;
    //public bool tankInDetectingRange, tankInAttackingRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        checkRate = Random.Range(0.3f, 0.4f);
        TankWander = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check is in range or not
        //tankInDetectingRange = Physics.CheckSphere(transform.position, detectedRange);
        //tankInAttackingRange = Physics.CheckSphere(transform.position, attackRange);

        //if (!tankInDetectingRange && !tankInAttackingRange) WanderMove();
        //if (tankInDetectingRange && !tankInAttackingRange) ChaseMove();
        //if (tankInAttackingRange && tankInDetectingRange) tankShooting.Fire(); //----> Function extracted from another script

        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            CheckIfIShouldWander();
        }

        Debug.DrawLine(transform.position, wanderTarget, Color.blue);
    }

    private void CheckIfIShouldWander()
    {
        if (RandomWanderTarget(TankWander.position, wanderRange, out wanderTarget))
        {
            agent.SetDestination(wanderTarget);
        }
    }

    private bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    {
        Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
        if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = centre;
            return false;
        }
    }

    //private void WanderMove()
    //{
    //    if (!walkPointSet)
    //        SearchWalkPoint();

    //    if (walkPointSet)
    //        agent.SetDestination(walkPoint);

    //    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //    if (distanceToWalkPoint.magnitude < 1f)
    //        walkPointSet = false;
    //}

    //private void SearchWalkPoint()
    //{
    //    float rndZ = Random.Range(-walkPointRange, walkPointRange);
    //    float rndX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + rndX, transform.position.y, transform.position.z + rndZ);

    //    if (Physics.Raycast(walkPoint, -transform.up, allArea))
    //        walkPointSet = true;

    //}

    //private void ChaseMove()
    //{
    //    agent.SetDestination(ChasedTank.position);
    //}

}
