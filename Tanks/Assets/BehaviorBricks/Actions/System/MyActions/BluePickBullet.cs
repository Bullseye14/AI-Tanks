using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

namespace BBUnity.Actions
{
    [Action("MyActions/BluePickBullet")]

    public class BluePickBullet : GOAction
    {
        public UnityEngine.AI.NavMeshAgent navAgent;

        [InParam("Blue Tank")]
        public GameObject BlueTank;

        public GameObject objective;

        [InParam("audioSource")]
        public AudioSource audioSource;

        [InParam("bulletCharge")]
        public AudioClip chargeSound;

        public GameObject game;

        private bool arrived = false;


        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            BlueTank = GameObject.Find("Tank1");
            objective = GameObject.Find("Bullet(Clone)");

            arrived = false;

            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            if (BlueTank.activeSelf)
            {
                if (!arrived && objective != null)
                {
                    GoToDrop();
                }

                else if (arrived)
                {
                    GetDrop();
                }
            }

            return TaskStatus.RUNNING;
        }
        private void GoToDrop()
        {
            Vector3 distance = Vector3.zero;

            distance.x = Mathf.Abs(objective.transform.position.x - BlueTank.transform.position.x);
            distance.z = Mathf.Abs(objective.transform.position.z - BlueTank.transform.position.z);

            if (distance.x > 1 && distance.z > 1)
                navAgent.SetDestination(objective.transform.position);

            else
                arrived = true;
        }

        private void GetDrop()
        {
            game = GameObject.Find("GameManager");

            game.GetComponent<MyGameManager>().blueBullets += 1;

            audioSource.clip = chargeSound;
            audioSource.Play();

            GameObject.Destroy(objective);
        }
    }
}

