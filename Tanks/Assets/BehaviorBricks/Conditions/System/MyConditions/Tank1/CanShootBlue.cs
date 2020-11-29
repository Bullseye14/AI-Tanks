using UnityEngine;


using Pada1.BBCore.Framework;
using Pada1.BBCore;

using UnityEngine.AI;

[Condition("MyConditions/Tank1/Can Shoot Blue Tank?")]
[Help("Checks if the tank can shoot or not")]
public class CanShootBlue : ConditionBase
{
    public bool blue_shoot = false;
    public bool blocked = false;
    public override bool Check()
    {

        GameObject tank1 = GameObject.Find("Tank1");
        GameObject tank2 = GameObject.Find("Tank2");

        NavMeshHit hit;

        Vector3 origin = tank1.transform.position;
        Vector3 destination = tank2.transform.position;

        //Vector3 tanks_distance = AbsoluteValue(destination - origin);

        blocked = NavMesh.Raycast(origin, destination, out hit, NavMesh.AllAreas);
        Debug.DrawLine(tank1.transform.position, tank2.transform.position, blocked ? Color.red : Color.green);

        if (blocked)
        {
            Debug.DrawRay(hit.position, Vector3.up, Color.red);
            blue_shoot = false;
            return false;
        }
        else
        {
            blue_shoot = true;
            return true;
        }
    }

    public Vector3 AbsoluteValue(Vector3 vector)
    {
        if (vector.x < 0) vector.x = -vector.x;

        if (vector.y < 0) vector.y = -vector.y;

        if (vector.z < 0) vector.z = -vector.z;

        return vector;
    }
}
