using UnityEngine;


using Pada1.BBCore.Framework;
using Pada1.BBCore;

using UnityEngine.AI;

[Condition("MyConditions/CanShoot ?")]
[Help("Checks if the tank can shoot or not")]
public class CanShoot : ConditionBase
{
    public override bool Check()
    {

        GameObject tank1 = GameObject.Find("Tank1");
        GameObject tank2 = GameObject.Find("Tank2");

        NavMeshHit hit;

        Vector3 origin = tank1.transform.position;
        Vector3 destination = tank2.transform.position;

        //Vector3 tanksDistance = AbsoluteValue(destination - origin);

        bool blocked = NavMesh.Raycast(origin, destination, out hit, NavMesh.AllAreas);
        Debug.DrawLine(tank1.transform.position, tank2.transform.position, blocked ? Color.red : Color.green);

        if (blocked)
        {
            Debug.DrawRay(hit.position, Vector3.up, Color.red);
        }
        else
            Debug.Log("Can Shoot");

        return blocked;
    }
}
