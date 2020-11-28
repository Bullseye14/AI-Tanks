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
        public List<WayPoints> waypoints;

        private int currentWaypoint;
        private bool isTravelling;
        private bool nextWaypoint = false;
        private Vector3 target;
        public UnityEngine.AI.NavMeshAgent navAgent;

        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            if (waypoints != null && waypoints.Count >= 2)
            {
                currentWaypoint = 0;
                SetDestination();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (isTravelling && navAgent.remainingDistance <= 0.5f)
            {
                isTravelling = false;

                ChangePatrolPoint();
                SetDestination();
            }

            return TaskStatus.RUNNING;
        }

        private void SetDestination()
        {
            if (waypoints != null)
            {
                target = waypoints[currentWaypoint].transform.position;
                navAgent.SetDestination(target);
                isTravelling = true;
            }
        }

        private void ChangePatrolPoint()
        {
            if (nextWaypoint)
                currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
            else
            {
                if (--currentWaypoint < 0)
                    currentWaypoint = waypoints.Count - 1;
            }
        }

    }
}
