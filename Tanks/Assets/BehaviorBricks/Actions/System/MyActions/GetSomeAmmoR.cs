using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

namespace BBUnity.Actions
{
    [Action("MyActions/GetSomeAmmoR")]

    public class GetSomeAmmoR : GOAction
    {
        public UnityEngine.AI.NavMeshAgent navAgent;

        [InParam("Red Tank")]
        public GameObject RedTank;

        [InParam("redBase")]
        public GameObject objective;

        [InParam("audioSource")]
        public AudioSource audioSource;

        [InParam("bulletCharge")]
        public AudioClip chargeSound;

        public GameObject game;

        private float delay = 4f;
        private float delayTimer;
        private bool charging = true;

        private bool arrived = false;

        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            RedTank = GameObject.Find("Tank2");
            objective = GameObject.Find("Red Base");

            arrived = false;

            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            if (RedTank.activeSelf)
            {
                if (!arrived && objective != null)
                {
                    GoToBase();
                }

                else if (arrived)
                {
                    HeyHeyMate_GETSEMEMO();
                }
            }

            return TaskStatus.RUNNING;
        }
        private void GoToBase()
        {
            Vector3 distance = Vector3.zero;

            distance.x = Mathf.Abs(objective.transform.position.x - RedTank.transform.position.x);
            distance.z = Mathf.Abs(objective.transform.position.z - RedTank.transform.position.z);

            if (distance.x > 3 && distance.z > 3)
                navAgent.SetDestination(objective.transform.position);

            else
                arrived = true;
        }

        private void HeyHeyMate_GETSEMEMO()
        {
            if (charging)
            {
                delayTimer += Time.deltaTime;

                if (delayTimer >= delay)
                    charging = false;
            }
            else
            {
                game = GameObject.Find("GameManager");

                game.GetComponent<MyGameManager>().redBullets += 3;

                audioSource.clip = chargeSound;
                audioSource.Play();
            }
        }
    }
}
