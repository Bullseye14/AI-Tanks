using UnityEngine;


using Pada1.BBCore.Framework;
using Pada1.BBCore;

using UnityEngine.AI;

[Condition("MyConditions/Tank1/Out Of Ammo?")]
[Help("Checks if the blue tank is out of ammo or not")]
public class OutOfAmmoBlue : ConditionBase
{
    public override bool Check()
    {
        return false;
    }
}
