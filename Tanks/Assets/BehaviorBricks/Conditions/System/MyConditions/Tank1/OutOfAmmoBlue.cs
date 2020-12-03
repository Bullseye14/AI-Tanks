using UnityEngine;


using Pada1.BBCore.Framework;
using Pada1.BBCore;

using UnityEngine.AI;

[Condition("MyConditions/Tank1/Out Of Ammo?")]
[Help("Checks if the blue tank is out of ammo or not")]
public class OutOfAmmoBlue : ConditionBase
{
    public GameObject game;
    public int blue_bullets;
    public override bool Check()
    {
        game = GameObject.Find("GameManager");
        blue_bullets = game.GetComponent<MyGameManager>().blueBullets;

        if (blue_bullets == 0)
            return true;
        else
            return false;
    }
}
