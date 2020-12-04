using UnityEngine;


using Pada1.BBCore.Framework;
using Pada1.BBCore;

using UnityEngine.AI;

[Condition("MyConditions/Tank1/Out Of Ammo Blue?")]
[Help("Checks if the blue tank is out of ammo or not")]
public class OutOfAmmoBlue : ConditionBase
{
    public GameObject game;
    public override bool Check()
    {
        game = GameObject.Find("GameManager");

        if (game.GetComponent<MyGameManager>().blueBullets != 0) 
            return false;

        else 
            return true;
    }
}
