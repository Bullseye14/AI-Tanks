using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public class RedTankMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask layerMask;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    
    //public Transform RedTank;
    //public Transform BlueTank;
    //public float wanderRange = 10f;
    //private float nextCheck;
    //private NavMeshHit navHit;
    //private Vector3 wanderTarget;
    //private float checkRate = 0.3f;

    //public float detectedRange, attackRange;
    //public bool tankInDetectingRange, tankInAttackingRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
        WanderMove();
    }

    private void WanderMove()
    {
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            Debug.DrawLine(transform.position, walkPoint, Color.blue);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float rndZ = Random.Range(-walkPointRange, walkPointRange);
        float rndX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + rndX, transform.position.y, transform.position.z + rndZ);

        if (Physics.Raycast(walkPoint, -transform.up, layerMask))
            walkPointSet = true;

    }

    //private void CheckIfIShouldWander()
    //{
    //    if (RandomWanderTarget(RedTank.position, wanderRange, out wanderTarget))
    //    {
    //        agent.SetDestination(RedTank.transform.position);
    //    }
    //}

    //private bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    //{
    //    Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
    //    if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
    //    {
    //        result = navHit.position;
    //        return true;
    //    }
    //    else
    //    {
    //        result = centre;
    //        return false;
    //    }
    //}
}
