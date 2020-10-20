using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    public GameObject tankFather;
    public GameObject tankObjective;

    private float distanceTankTurretX, distanceTankTurretZ;
    private float turnSpeed = 20f;

    private Rigidbody m_RigidBody;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        distanceTankTurretX = tankFather.transform.position.x - transform.position.x;
        distanceTankTurretZ = tankFather.transform.position.z - transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();

        WatchOponent(tankObjective);
    }

    private void Move()
    {
        Vector3 movement;

        movement.x = tankFather.transform.position.x + distanceTankTurretX;
        movement.z = tankFather.transform.position.z + distanceTankTurretZ;
        movement.y = 1.08f;

        transform.position = movement;
    }

    private void WatchOponent(GameObject tankObjective)
    {
        Vector3 desiredPosition = tankObjective.transform.position;

        Vector3 angleToLook;
        angleToLook.x = angleToLook.z = 0;
        angleToLook.y = CalculateAngle(desiredPosition);

        Quaternion desiredRotation = Quaternion.Euler(angleToLook);

        m_RigidBody.MoveRotation(desiredRotation);
    }

    private float CalculateAngle(Vector3 objectivePos)
    {
        float distanceX = objectivePos.x - transform.position.x;
        float distanceZ = objectivePos.z - transform.position.z;

        float angleDesired = Mathf.Atan2(distanceX, distanceZ) * Mathf.Rad2Deg;

        return angleDesired;
    }
}
