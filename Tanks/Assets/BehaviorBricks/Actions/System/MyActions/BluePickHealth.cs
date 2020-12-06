using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

namespace BBUnity.Actions
{
    [Action("MyActions/BluePickHealth")]

    public class BluePickHealth : GOAction
    {
        public UnityEngine.AI.NavMeshAgent navAgent;

        [InParam("Blue Tank")]
        public GameObject BlueTank;

        [InParam("Tank Health")]
        public TankHealth tankHealth;

        public GameObject objective;

        [InParam("audioSource")]
        public AudioSource audioSource;

        [InParam("healthSound")]
        public AudioClip healthSound;

        private bool arrived = false;

        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            BlueTank = GameObject.Find("Tank1");
            objective = GameObject.Find("Heart(Clone)");

            if(tankHealth == null)
            {
                tankHealth = GameObject.Find("Tank1").GetComponent<TankHealth>();
            }

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
            if (objective != null)
            {
                GameObject.Find("Tank1").GetComponent<TankHealth>().m_CurrentHealth += 30f;

                GameObject.Destroy(objective);

                audioSource.clip = healthSound;
                audioSource.Play();

                arrived = false;

            }
        }
    }
}
