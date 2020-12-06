using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;

namespace BBUnity.Actions
{
    [Action("MyActions/RedPickHealth")]

    public class RedPickHealth : GOAction
    {
        public UnityEngine.AI.NavMeshAgent navAgent;

        [InParam("Red Tank")]
        public GameObject RedTank;

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

            RedTank = GameObject.Find("Tank2");
            objective = GameObject.Find("Heart(Clone)");

            if (tankHealth == null)
            {
                tankHealth = GameObject.Find("Tank2").GetComponent<TankHealth>();
            }

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

            if (distance.x > 1 && distance.z > 1)
                navAgent.SetDestination(objective.transform.position);

            else
                arrived = true;
        }

        private void HeyHeyMate_GETSEMEMO()
        {
            if (objective != null)
            {
                GameObject.Find("Tank2").GetComponent<TankHealth>().m_CurrentHealth += 30f;

                GameObject.Destroy(objective);

                audioSource.clip = healthSound;
                audioSource.Play();

                arrived = false;

            }
        }
    }
}
