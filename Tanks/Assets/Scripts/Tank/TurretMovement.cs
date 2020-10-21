using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    public GameObject tankFather;
    public GameObject tankObjective;

    private float distanceTankTurretX, distanceTankTurretY, distanceTankTurretZ;

    // Start is called before the first frame update
    void Start()
    {
        distanceTankTurretX = tankFather.transform.position.x - transform.position.x;
        distanceTankTurretY = 1.08f;
        distanceTankTurretZ = tankFather.transform.position.z - transform.position.z;
    }

    private void FixedUpdate()
    {
        Move(tankObjective);

        // WatchOponent(tankObjective);
    }

    private void Move(GameObject tankObjective)
    {
        Vector3 movement;

        movement.x = tankFather.transform.position.x + distanceTankTurretX;
        movement.y = tankFather.transform.position.y + distanceTankTurretY;
        movement.z = tankFather.transform.position.z + distanceTankTurretZ;

        transform.position = movement;

        Vector3 rotation;

        rotation.x = 0;
        rotation.y = CalculateAngle(tankObjective.transform.position);
        rotation.z = 0;

        transform.rotation = Quaternion.Euler(rotation);
    }

    private float CalculateAngle(Vector3 objectivePos)
    {
        float distanceX = objectivePos.x - transform.position.x;
        float distanceZ = objectivePos.z - transform.position.z;

        float angleDesired = Mathf.Atan2(distanceX, distanceZ) * Mathf.Rad2Deg;

        return angleDesired;
    }
}
