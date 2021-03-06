﻿using UnityEngine;


using Pada1.BBCore.Framework;
using Pada1.BBCore;

using UnityEngine.AI;

[Condition("MyConditions/Tank1/Very low health?")]
[Help("Checks if the blue tank has very low health")]

public class VeryLowHealthB : ConditionBase
{
    public GameObject heart;
    public GameObject tank1;

    [InParam("Tank Health")]
    public TankHealth tankHealth;

    public override bool Check()
    {
        tank1 = GameObject.Find("Tank1");

        if (tankHealth.m_CurrentHealth < 40)
        {
            heart = GameObject.Find("Heart(Clone)");

            if (heart != null)
                return true;

            else return false;         
        }

        else return false;
    }
}
