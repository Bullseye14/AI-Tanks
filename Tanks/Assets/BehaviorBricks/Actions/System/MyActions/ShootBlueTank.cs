using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System;

namespace BBUnity.Actions
{
    [Action("MyActions/ShootBlueTank")]
    [Help("Clone a 'bullet' and shoots it throught the Forward axis with the " +
           "specified velocity.")]
    public class ShootBlueTank : GOAction
    {
        private float m_CurrentLaunchForce;
        private bool canFire = true;
        private float delayTimer;
        [InParam("delay")]
        public float delay;
        private float minDistance = 10;
        private float midDistance = 15;
        private float maxDistance = 20;
        private Vector3 destination;
        private Vector3 origin;

        //public AudioSource m_ShootingAudio;
        //public AudioClip m_ChargingClip;
        //public AudioClip m_FireClip;

        
        //CanShootBlue condition;


        [InParam("shootPoint")]
        public Transform shootPoint;

        [InParam("bullet")]
        public GameObject bullet;

        //[InParam("velocity", DefaultValue = 30f)]
        //public float velocity;

        public override void OnStart()
        {
            if (shootPoint == null)
            {
                shootPoint = gameObject.transform.Find("shootPoint");
                if (shootPoint == null)
                {
                    Debug.LogWarning("Shoot point not specified. ShootOnce will not work " +
                                     "for " + gameObject.name);
                }
            }

            GameObject tank1 = GameObject.Find("Tank1");
            GameObject tank2 = GameObject.Find("Tank2");

            origin = tank1.transform.position;
            destination = tank2.transform.position;

            base.OnStart();
        } 

        public override TaskStatus OnUpdate()
        {
            if (shootPoint == null)
            {
                return TaskStatus.FAILED;
            }

            if (!canFire)
            {
                delayTimer += Time.deltaTime;
                if (delayTimer >= delay)
                    canFire = true;
            }

            if (/*condition.blue_shoot && */canFire)
            {
                Fire(EnemyClose(destination - origin));
            }

            //GameObject newBullet = GameObject.Instantiate(
            //                            bullet, shootPoint.position,
            //                            shootPoint.rotation * bullet.transform.rotation
            //                        ) as GameObject;

            //if (newBullet.GetComponent<Rigidbody>() == null)
            //    newBullet.AddComponent<Rigidbody>();

            //newBullet.GetComponent<Rigidbody>().velocity = velocity * shootPoint.forward;

            return TaskStatus.COMPLETED;
        } 

        public void Fire(int charge)
        {
            if (charge != -1)
            {
                if (charge == 0)
                    m_CurrentLaunchForce = 15f;

                else if (charge == 1)
                    m_CurrentLaunchForce = 17f;

                else if (charge == 2)
                    m_CurrentLaunchForce = 22f;

                GameObject newBullet = GameObject.Instantiate(
                                       bullet, shootPoint.position,
                                       shootPoint.rotation * bullet.transform.rotation
                                   ) as GameObject;

                if (newBullet.GetComponent<Rigidbody>() == null)
                    newBullet.AddComponent<Rigidbody>();

                newBullet.GetComponent<Rigidbody>().velocity = m_CurrentLaunchForce * shootPoint.forward;

                canFire = false;

                delayTimer = 0;

                //m_ShootingAudio.clip = m_FireClip;
                //m_ShootingAudio.Play();

                m_CurrentLaunchForce = 15f;
            }
        }

        private int EnemyClose(Vector3 distance)
        {
            int ret;

            if (Mathf.Sqrt((distance.x * distance.x) + (distance.z * distance.z)) < minDistance) ret = 0;
            else if (Mathf.Sqrt((distance.x * distance.x) + (distance.z * distance.z)) < midDistance) ret = 1;
            else if (Mathf.Sqrt((distance.x * distance.x) + (distance.z * distance.z)) < maxDistance) ret = 2;
            else ret = -1;

            //if ((ret == 2) && this.m_PlayerNumber == 2) ret = -1;

            return ret;
        }

    } 
}

