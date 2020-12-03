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

        if (tank2 != null)
        {
            Vector3 origin = tank1.transform.position;
            Vector3 destination = tank2.transform.position;

            Vector3 tanks_distance = AbsoluteValue(destination - origin);

            blocked = NavMesh.Raycast(origin, destination, out hit, NavMesh.AllAreas);
            Debug.DrawLine(tank1.transform.position, tank2.transform.position, blocked ? Color.red : Color.green);


            // Returns true if it CANNOT shoot, because, if it can shoot, it will not patrol, it will shoot
            if (blocked || !CloseEnough(tanks_distance, 20))
            {
                // Cannot shoot, blocked or too far
                Debug.DrawRay(hit.position, Vector3.up, Color.red);
                blue_shoot = true;
                return true;
            }
            else
            {
                blue_shoot = false;
                return false;
            }
        }
        else return true;
    }

    public Vector3 AbsoluteValue(Vector3 vector)
    {
        if (vector.x < 0) vector.x = -vector.x;

        if (vector.y < 0) vector.y = -vector.y;

        if (vector.z < 0) vector.z = -vector.z;

        return vector;
    }

    private bool CloseEnough(Vector3 distance, int dist)
    {
        if (distance.x < dist && distance.z < dist)
            return true;
        else return false;
    }
}
