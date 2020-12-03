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

    public float radius = 2f;
    public float offset = 3f;
    private float nextMove;
    public float moveRate = 0.3f;
    private Vector3 debugPoint;
    private bool shownBubble1 = true;
    private bool shownBubble2 = false;

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
            shownBubble2 = false;
            if (!shownBubble1)
            {
                AppearSpriteRed.SpriteRedInstance.visible1 = true;
                AppearSpriteRed.SpriteRedInstance.visible2 = false;
                shownBubble1 = true;
            }
            WanderMove();
        }
            
        else if (MoveManager.RFlee)
        {
            shownBubble1 = false;
            if (!shownBubble2)
            {
                AppearSpriteRed.SpriteRedInstance.visible2 = true;
                AppearSpriteRed.SpriteRedInstance.visible1 = false;
                shownBubble2 = true;
            }
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
}
