using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderMovement : MonoBehaviour
{
    // Wander Movement Variables
    private NavMeshAgent agent;
    private float nextCheck;
    private NavMeshHit navHit;
    private Vector3 wanderTarget;

    // Public
    public Transform TankWander;
    public Transform TankTarget;
    public float checkRate;
    public float wanderRange = 10f;

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
        if(Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            CheckIfIShouldWander();

        }
    }

    private void CheckIfIShouldWander()
    {
        if(RandomWanderTarget(TankWander.position, wanderRange, out wanderTarget))
        {
            agent.SetDestination(wanderTarget);
        }
    }

    private bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    {
        Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
        if(NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
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
}
