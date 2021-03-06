﻿using UnityEngine;


using Pada1.BBCore.Framework;
using Pada1.BBCore;

using UnityEngine.AI;

[Condition("MyConditions/Tank1/Can blue pick bullet?")]
[Help("Checks if the blue tank can pick bullet drop")]
public class CanPickDropB : ConditionBase
{
    public GameObject game;
    public GameObject bullet = null;

    public GameObject self;
    public GameObject enemy;
    public override bool Check()
    {
        game = GameObject.Find("GameManager");

        if (game.GetComponent<MyGameManager>().blueBullets != 3)
        {
            bullet = GameObject.Find("Bullet(Clone)");

            self = GameObject.Find("Tank1"); enemy = GameObject.Find("Tank2");

            if (bullet != null && enemy != null)
            {
                return GoForAmmo(self.transform.position, enemy.transform.position, bullet.transform.position);
            }
            else return false;
        }

        else
            return false;
    }

    private bool GoForAmmo(Vector3 s, Vector3 e, Vector3 b)
    {
        Vector3 s_e = Vector3.zero;
        Vector3 s_b = Vector3.zero;

        // Distance between Tanks
        s_e.x = Mathf.Abs(e.x - s.x);
        s_e.z = Mathf.Abs(e.z - s.z);

        // Distance between Tank and Bullet
        s_b.x = Mathf.Abs(b.x - s.x);
        s_b.z = Mathf.Abs(b.z - s.z);

        if (s_e.x > s_b.x && s_e.z > s_b.z) // Tank is closest to Bullet than to Enemy
        {
            return true;
        }
        else return false; // Enemy is closest to Tank than to Bullet
    }
}