using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;

namespace BBUnity.Actions
{
    [Action("MyActions/PatrolTank1")]

    public class PatrolTank1 : GOAction
    {
        public UnityEngine.AI.NavMeshAgent navAgent;

        [InParam("Blue Tank")]
        public GameObject BlueTank;
        private GameObject[] pointChildren;
        private GameObject WayPoints;
        private int destPoint = -1;

        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            WayPoints = GameObject.Find("WayPoints");
            pointChildren = new GameObject[WayPoints.transform.childCount];

            for (int i = 0; i < WayPoints.transform.childCount; i++)
            {
                pointChildren[i] = WayPoints.transform.GetChild(i).gameObject;
            }

            GameObject tank1 = GameObject.Find("Tank1");

            Patrol();

        }

        public override TaskStatus OnUpdate()
        {
            if (BlueTank.activeSelf)
            {
                if (!navAgent.pathPending && navAgent.remainingDistance <= 3f)
                    navAgent.speed -= 0.5f;

                if (!navAgent.pathPending && navAgent.remainingDistance <= 1f)
                {
                    Patrol();
                    navAgent.speed = 4f;
                }
            }

            return TaskStatus.RUNNING;
        }

        private void Patrol()
        {

            if (pointChildren.Length == 0)
                return;

            if (destPoint == -1)
            {
                navAgent.destination = ClosestPatrolPoint();
            }
            else
            {
                navAgent.destination = pointChildren[destPoint].transform.position;
                destPoint = (destPoint + 1) % pointChildren.Length;
            }

            navAgent.angularSpeed = 200f;


            Debug.Log(pointChildren[2].transform.position.y);
            Debug.Log("Points:" + pointChildren.Length);
        }

        private Vector3 ClosestPatrolPoint()
        {
            float dist = -1;
            float mindist = 0;

            Vector3 closest = Vector3.zero;

            for (int i = 0; i < pointChildren.Length; i++)
            {

                //First iteration
                if (dist == -1)
                {
                    mindist = dist = Vector3.Distance(pointChildren[i].transform.position, gameObject.transform.position);
                    closest = pointChildren[i].transform.position;
                    destPoint = i;
                }
                else
                {
                    dist = Vector3.Distance(pointChildren[i].transform.position, gameObject.transform.position);

                    if (dist < mindist)
                    {
                        mindist = dist;
                        closest = pointChildren[i].transform.position;
                        destPoint = i;
                    }
                }
            }

            return closest;
        }

    }
}
