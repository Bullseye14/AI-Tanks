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
        private float launchForce_long = 20f;
        private float launchForce_short = 12f;
        private bool canFire = true;
        private float delayTimer;
        [InParam("delay")]
        public float delay;

        //public AudioSource m_ShootingAudio;
        //public AudioClip m_ChargingClip;
        //public AudioClip m_FireClip;
        
        //CanShootBlue condition;
        private Transform shootPoint;

        [InParam("bullet")]
        public GameObject bullet;

        //[InParam("velocity", DefaultValue = 30f)]
        //public float velocity;

        public override void OnStart()
        {
            if (shootPoint == null)
            {
                Transform fireTransf;

                fireTransf = gameObject.transform.Find("TankRenderers");
                fireTransf = fireTransf.transform.Find("TankTurret");
                shootPoint = fireTransf.transform.Find("FireTransform");
            }

            delayTimer = 0f;
            canFire = true;

            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            if (!canFire)
            {
                delayTimer += Time.deltaTime;

                if (delayTimer >= delay)
                    canFire = true;
            }

            if (canFire)
            {
                Fire();
                canFire = false;
            }

            return TaskStatus.RUNNING;
        }

        public void Fire()
        {
            GameObject tank2 = GameObject.Find("Tank2");

            Vector3 shootPos;
            Quaternion shootRot;

            if (shootPoint == null)
            {
                shootPos = Vector3.zero;
                shootRot = Quaternion.identity;
            }
            else
            { 
                shootPos = shootPoint.position;
                shootRot = shootPoint.rotation;
            }

            GameObject newBullet = GameObject.Instantiate(bullet, shootPos, shootRot * bullet.transform.rotation) as GameObject;

            if (newBullet.GetComponent<Rigidbody>() == null)
                newBullet.AddComponent<Rigidbody>();

            if (CloseEnough(shootPos, tank2))
                newBullet.GetComponent<Rigidbody>().velocity = launchForce_short * shootPoint.forward;

            else
                newBullet.GetComponent<Rigidbody>().velocity = launchForce_long * shootPoint.forward;


            delayTimer = 0;

            //m_ShootingAudio.clip = m_FireClip;
            //m_ShootingAudio.Play();

        }
        public bool CloseEnough(Vector3 origin, GameObject enemy)
        {
            Vector3 enemyPos = enemy.transform.position;

            Vector3 distance;
            distance.x = Mathf.Abs(enemyPos.x - origin.x);
            distance.y = 0;
            distance.z = Mathf.Abs(enemyPos.z - origin.z);

            if (distance.x < 15 && distance.z < 15) return true;
            else return false;
        }
    } 
}

