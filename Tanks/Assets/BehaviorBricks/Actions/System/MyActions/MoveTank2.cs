using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;

namespace BBUnity.Actions
{
    [Action("MyActions/MoveTank2")]

    public class MoveTank2 : GOAction
    {
        public UnityEngine.AI.NavMeshAgent navAgent;

        [InParam("Red Tank")]
        public GameObject RedTank;

        private float nextMove;
        private float moveRate = 0.3f;
        private Vector3 debugPoint;

        private float delay = 2f;
        private float delayTimer;
        private bool waiting = false;

        [InParam("Layer")]
        public LayerMask layer;

        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            RedTank = GameObject.Find("Tank2");

            delayTimer = 0f;
        }

        public override TaskStatus OnUpdate()
        {
            if (RedTank.activeSelf)
            {
                if(waiting)
                {
                    delayTimer += Time.deltaTime;

                    if (delayTimer >= delay)
                        waiting = false;
                }

                else
                {
                    Wander();

                    waiting = true;
                }
                
            }

            return TaskStatus.RUNNING;
        }

        private void Wander()
        {
            if(Time.time > nextMove)
            {
                nextMove = Time.time + moveRate;

                navAgent.SetDestination(RandomLocation(20f));
            }
        }


        private Vector3 RandomLocation(float radius)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;

            randomDirection += RedTank.transform.position;

            NavMeshHit hit;

            Vector3 finalPosition = Vector3.zero;

            if(NavMesh.SamplePosition(randomDirection, out hit, radius, layer))
            {
                finalPosition = hit.position;
            }

            debugPoint = finalPosition;

            delayTimer = 0f;

            return finalPosition;
        }
    }
}
