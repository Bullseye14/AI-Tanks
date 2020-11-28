using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

[Action("MyActions/GoBlueBase")]

public class GoBlueBase : BasePrimitiveAction
{
    public override TaskStatus OnUpdate()
    {
        // code for return base blue tank
        return base.OnUpdate();
    }
}
