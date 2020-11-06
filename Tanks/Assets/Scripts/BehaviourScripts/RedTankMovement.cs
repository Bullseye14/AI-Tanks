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
    public Transform BlueTank;
    public TanksMoveManager MoveManager;
    public GameObject WanderB;
    public GameObject FleeB;

    public float radius = 2f;
    public float offset = 3f;
    private float nextMove;
    public float moveRate = 0.3f;
    private Vector3 debugPoint;
    public LayerMask layer;


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
        if (MoveManager.RWander)
        {
            WanderB.SetActive(true);
            WanderMove();
        }
            
        else if (MoveManager.RFlee)
        {
            FleeB.SetActive(true);
            FleeMove();
        }
            
        Debug.DrawLine(transform.position, debugPoint, Color.blue);

    }

    public void WanderMove()
    {
        if (Time.time > nextMove)
        {
            nextMove = Time.time + moveRate;

            agent.SetDestination(RandomNavmeshLocation(20f));
        }

    }

    public void FleeMove()
    {
        agent.SetDestination(-BlueTank.position);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, layer))
        {
            finalPosition = hit.position;
        }

        debugPoint = finalPosition;

        return finalPosition;
    }

    //private void Wander3()
    //{
    //    if (!walkPointSet)
    //        SearchWalkPoint();

    //    if (walkPointSet)
    //    {
    //        agent.SetDestination(walkPoint);
    //        Debug.DrawLine(transform.position, walkPoint, Color.blue);
    //    }

    //    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //    if (distanceToWalkPoint.magnitude < 1f)
    //        walkPointSet = false;
    //}

    //private void SearchWalkPoint()
    //{
    //    float rndZ = Random.Range(-walkPointRange, walkPointRange);
    //    float rndX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + rndX, transform.position.y, transform.position.z + rndZ);

    //    if (Physics.Raycast(walkPoint, -transform.up, layerMask))
    //        walkPointSet = true;

    //}

    //private void Wander2()
    //{
    //    Vector3 localTarget = new Vector3(Random.Range(-0.5f, 1.0f), 0, Random.Range(-0.5f, 1.0f));
    //    localTarget.Normalize();
    //    localTarget *= radius;
    //    localTarget += new Vector3(0, 0, offset);

    //    Vector3 worldTarget = transform.TransformPoint(localTarget);
    //    worldTarget.y = 0f;

    //    agent.SetDestination(worldTarget);

    //    debugPoint = worldTarget;

    //}

    //private void CheckIfIShouldWander()
    //{
    //    if (RandomWanderTarget(transform.position, wanderRange, out wanderTarget))
    //    {
    //        agent.SetDestination(wanderTarget);
    //    }
    //}

    //private bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    //{
    //    Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
    //    Gizmos.DrawSphere(centre, wanderRange);
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
