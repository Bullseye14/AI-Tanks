using UnityEngine;


using Pada1.BBCore.Framework;
using Pada1.BBCore;

using UnityEngine.AI;

[Condition("MyConditions/Tank2/Out Of Ammo Red?")]
[Help("Checks if the blue tank is out of ammo or not")]
public class OutOfAmmoRed: ConditionBase
{
    public GameObject game;
    public override bool Check()
    {
        game = GameObject.Find("GameManager");

        if (game.GetComponent<MyGameManager>().redBullets != 0)
            return false;

        else
            return true;
    }
}
