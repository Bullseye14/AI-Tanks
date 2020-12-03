using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

namespace BBUnity.Actions
{
    [Action("MyActions/GetSomeAmmo")]

    public class GetSomeAmmo : GOAction
    {
        public UnityEngine.AI.NavMeshAgent navAgent;

        [InParam("Blue Tank")]
        public GameObject BlueTank;

        [InParam("blueBase")]
        public GameObject objective;

        public GameObject game;

        private float delay = 4f;
        private float delayTimer;
        private bool charging = true;

        private bool arrived = false;


        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            BlueTank = GameObject.Find("Tank1");
            objective = GameObject.Find("Blue Base");

            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            if (BlueTank.activeSelf)
            {
                if (!arrived)
                {
                    GoToBase();
                }

                else
                {
                    HeyHeyMate_GETSEMEMO();
                }
            }

            return TaskStatus.RUNNING;
        }
        private void GoToBase()
        {
            Vector3 distance = Vector3.zero;

            distance.x = Mathf.Abs(objective.transform.position.x - BlueTank.transform.position.x);
            distance.z = Mathf.Abs(objective.transform.position.z - BlueTank.transform.position.z);

            if (distance.x > 1 && distance.z > 1)
                navAgent.SetDestination(objective.transform.position);

            else
                arrived = true;
        }

        private void HeyHeyMate_GETSEMEMO()
        {
            if(charging)
            {
                delayTimer += Time.deltaTime;

                if (delayTimer >= delay)
                    charging = false;
            }
            else
            {
                game = GameObject.Find("GameManager");

                game.GetComponent<MyGameManager>().blueBullets += 2;
            }

        }
    }

    
}
