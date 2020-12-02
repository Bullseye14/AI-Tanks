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
        private bool isTravelling;
        private bool nextWaypoint = false;
        private int destPoint;
        private Vector3 target;

        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            WayPoints = GameObject.Find("WayPoints");
            pointChildren = new GameObject[WayPoints.transform.childCount];

            for (int i = 0; i < WayPoints.transform.childCount; i++)
            {
                pointChildren[i] = WayPoints.transform.GetChild(i).gameObject;
            }

            BlueTank = GameObject.Find("Tank1");

            if(pointChildren.Length >= 2)
            {
                destPoint = 0;
                SetDestination();
            }

        }

        public override TaskStatus OnUpdate()
        {
            if (BlueTank.activeSelf)
            {
                if(isTravelling && navAgent.remainingDistance <= 0.5f)
                {
                    isTravelling = false;
                    ChangePatrolPoint();
                    SetDestination();
                }
            }

            return TaskStatus.RUNNING;
        }

        private void SetDestination()
        {
            target = pointChildren[destPoint].transform.position;
            navAgent.SetDestination(target);
            isTravelling = true;
        }

        private void ChangePatrolPoint()
        {
            if (destPoint == 0)
                navAgent.SetDestination(ClosestPatrolPoint());

            if (nextWaypoint)
                destPoint = (destPoint + 1) % pointChildren.Length;

            else
            {
                if (--destPoint < 0)
                    destPoint = pointChildren.Length - 1;
            }
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
